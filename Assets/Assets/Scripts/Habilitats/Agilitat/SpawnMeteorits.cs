using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpawnMeteorits : MonoBehaviour
{
    [Header("Public")]
    public Text nivellControl;
    public GameObject meteorit; 
    public Collider2D colliderSpawn;
    public LayerMask terraLayer;
    public List<Vector2> llistaMeteorits;

    public int NivellPassat;
    public bool haPerdut;

    [Header("Privat")]
    private float tempsSpawn;
    private float Comptador;
    private float timerHabilitar;
    private int passarNivell;

    private bool comencarNivell;
    private bool sumarNivell;

    private void Start()
    {
        Comptador = 0f;
        timerHabilitar = 0f;
        NivellPassat = 0;
        llistaMeteorits = new List<Vector2>();

        comencarNivell = false;
        haPerdut = false;
        sumarNivell = true;

        configuradorDificultat();

    }



    // Update is called once per frame
    void Update()
    {
        if (comencarNivell)
        {
            nivellControl.text = "Nivell Agilitat: " + ValorsGenerals.agilitat.ToString();
            Comptador += Time.deltaTime;

            if (Comptador >= tempsSpawn)
            {
                for (int i = 0; i < 5; i++) SpawnObjeto(); //5 meteors aleatoris               
                Comptador = 0;
            }
            haGuanyat();
            if (haPerdut) perdut();

        }
        else nivellControl.text = "CLICA EL BOTO ESQUERRA DEL RATOLI PER JUGAR O DRET PER SORTIR AL MENU \nEL NIVELL DE AGILITAT ES: " + ValorsGenerals.agilitat.ToString();


        if (Input.GetKeyDown(KeyCode.Mouse0)) comencarNivell = true;
        if (Input.GetKeyDown(KeyCode.Mouse1) && comencarNivell == false) SceneManager.LoadScene(1); //MENU


        if (!sumarNivell) timerHabilitar += Time.deltaTime;


    }
    private void SpawnObjeto()
    {
        float x = Random.Range(colliderSpawn.bounds.min.x, colliderSpawn.bounds.max.x);
        float y = Random.Range(colliderSpawn.bounds.min.y, colliderSpawn.bounds.min.y - 1);

        Vector2 posSpawn = new Vector2(x, y);

        while (ExisteixSolapament(posSpawn)) // per no solapar els meteorits
        {
            x = Random.Range(colliderSpawn.bounds.min.x, colliderSpawn.bounds.max.x);
            y = Random.Range(colliderSpawn.bounds.min.y, colliderSpawn.bounds.min.y - 1);
            posSpawn = new Vector2(x, y);
        }

        GameObject mt = Instantiate(meteorit, posSpawn, Quaternion.identity);
        llistaMeteorits.Add(mt.transform.position);
        Rigidbody2D rbMeteorit = mt.GetComponent<Rigidbody2D>();
        rbMeteorit.velocity = Vector2.down * 10; //apliquem la força just quan s'ha instanciat el meteorit
    }
    private void configuradorDificultat()
    {


        switch (ValorsGenerals.Control)
        {
            case 0:
                tempsSpawn = 3f;
                passarNivell = 10;
                break;
            case 1:
                tempsSpawn = 2.5f;
                passarNivell = 16;
                break;
            case 2:
                tempsSpawn = 2f;
                passarNivell = 23;
                break;
            case 3:
                tempsSpawn = 1f;
                passarNivell = 30;
                break;
        }

    }
    private void haGuanyat()
    {

        if (NivellPassat >= passarNivell)
        {
            ValorsGenerals.agilitat += 1;
            if (ValorsGenerals.agilitat > 0 && ValorsGenerals.agilitat < 4) JugadorInventari.numeroBotes += 1;
            else if (ValorsGenerals.agilitat == 4) JugadorInventari.numeroBotes += 2;
            Debug.Log(ValorsGenerals.agilitat);
            SceneManager.LoadScene(3);

        }




    }
    private void perdut()
    {
        if (ValorsGenerals.agilitat > 0)
        {
            ValorsGenerals.agilitat -= 1;
            if (ValorsGenerals.agilitat > 0 && ValorsGenerals.agilitat < 4) JugadorInventari.numeroBotes -= 1;
            else if (ValorsGenerals.agilitat == 4) JugadorInventari.numeroBotes -= 2;

        }
        SceneManager.LoadScene(3);

    }
    private bool ExisteixSolapament(Vector2 novaPosicio)
    {
        // Comprovem si la nova posició està prou a prop de qualsevol posició de meteorit existent
        foreach (Vector2 pos in llistaMeteorits)
        {
            if (Vector2.Distance(novaPosicio, pos) < 4)
            {
                return true; // Hi ha solapament
            }
        }

        return false; // No hi ha solapament
    }

    public void terraMeteorit()
    {
        if (sumarNivell)
        {
            NivellPassat += 1;
            sumarNivell = false;
        }
        else
        {
            if (timerHabilitar > 0.5f)
            {
                sumarNivell = true;
                timerHabilitar = 0;
            }
        }
          
    }

}
