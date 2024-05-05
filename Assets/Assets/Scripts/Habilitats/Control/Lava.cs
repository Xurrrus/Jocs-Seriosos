using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Lava : MonoBehaviour
{

    public Text textControl;
    public Text textLava;

    private bool comencarNivell;
    private float velocitatLava;
    private float temps = 5f;
    void Start()
    {
        comencarNivell = false;
        configuradorDificultat();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (comencarNivell)
        {
            textControl.text = "Nivell Control: " + ValorsGenerals.Control.ToString();
            temps -= Time.deltaTime;
            if (temps <= 0) textLava.text = "";
            else tempsRestant(temps);
        }
        else
        {
            textControl.text = "CLICA EL BOTO ESQUERRA DEL RATOLI PER JUGAR O DRET PER SORTIR AL MENU \nEL NIVELL DE CONTROl ES: " + ValorsGenerals.Control.ToString();
            textLava.text = "";
        }
        if (Input.GetKeyDown(KeyCode.Mouse0)) 
        {
            comencarNivell = true;
            InvokeRepeating("PujarLava", 5.0f, 0.3f);

        }
        if (Input.GetKeyDown(KeyCode.Mouse1) && comencarNivell == false) SceneManager.LoadScene(1); //MENU

    }

    private void PujarLava()
    {
        Vector2 escala = gameObject.transform.localScale;
        escala.y += velocitatLava;
        gameObject.transform.localScale = escala; 

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.transform.tag == "Jugador")
        {
            Debug.Log("La Lava ha acabat amb tu");
            if (ValorsGenerals.Control > 0)
            {
                ValorsGenerals.Control -= 1;
                if (ValorsGenerals.Control > 0 && ValorsGenerals.Control < 4) JugadorInventari.numeroGanxos -= 1;
                else if (ValorsGenerals.Control == 4) JugadorInventari.numeroGanxos -= 2;
                
            }
            SceneManager.LoadScene(5);

        }
    }
    void tempsRestant(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        textLava.text = string.Format(" El gas surt en: {0:00}:{1:00}", minutes, seconds);
    }
    private void configuradorDificultat()
    {
        switch (ValorsGenerals.Control)
        {
            case 0:
                velocitatLava = 0.3f;
                break;
            case 1:
                velocitatLava = 0.5f;
                break;
            case 2:
                velocitatLava = 0.7f;
                break;
            case 3:
                velocitatLava = 0.9f;
                break;
        }
    }
}
