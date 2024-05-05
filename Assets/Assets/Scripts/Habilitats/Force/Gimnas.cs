using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Gimnas : MonoBehaviour
{
    public Slider force;
    public GameObject pesa;
    public Text nivellForce;
    public GameObject[] spritesForce;

    private bool pesaAmunt;

    private float configuracioFactor;
    private float decrementBase;

    private bool comencarNivell;

    // Factor de suavizado para los cambios en configuracioFactor y decrementBase
    private float suavizado = 0.05f;

    void Start()
    {
        pesaAmunt = false;
        decrementBase = 0f;
        configuracioFactor = 0f;
        force.value = 0.2f;
        comencarNivell = false;
    }

    void Update()
    {
        

        if (comencarNivell)
        {
            nivellForce.text = "Nivell force: " + ValorsGenerals.Force.ToString();
            if (Input.GetKeyDown("space"))
            {
                IncrementarForce();
                AjustarPosPesa();
            }

            DecrementarForce();

            haGuanyat();

            haPerdut();

        }
        else nivellForce.text = "CLICA EL BOTO ESQUERRA DEL RATOLI PER JUGAR O DRET PER SORTIR AL MENU \nEL NIVELL DE FORCE ES: " + ValorsGenerals.Force.ToString();
       
        if (Input.GetKeyDown(KeyCode.Mouse0)) comencarNivell = true;
        if (Input.GetKeyDown(KeyCode.Mouse1) && comencarNivell == false) SceneManager.LoadScene(1); //MENU



    }

    private void IncrementarForce()
    {
        force.value += 0.02f;
        
    }

    private void DecrementarForce()
    {
        float decrement = calcularDecrement();
        force.value -= decrement * Time.deltaTime * 0.5f; 
        
    }

    private void AjustarPosPesa()
    {
        Vector2 posicio = pesa.transform.position;
        if (!pesaAmunt)
        {         
            posicio.y = -3.756f;
            spritesForce[0].SetActive(true);
            spritesForce[1].SetActive(false);
            pesaAmunt = true;
        }
        else
        {
            posicio.y = -4.381f;
            spritesForce[0].SetActive(false);
            spritesForce[1].SetActive(true);
            pesaAmunt = false;
        }
        pesa.transform.position = posicio;
    }

    private float calcularDecrement()
    {
        configuradorDificultat();

        float decremento = decrementBase + (force.value * configuracioFactor);

        configuracioFactor = Mathf.Lerp(configuracioFactor, decremento, suavizado);
        decrementBase = Mathf.Lerp(decrementBase, decremento, suavizado);

        return decremento;
    }

    private void configuradorDificultat()
    {
        switch (ValorsGenerals.Force)
        {
            case 0:
                configuracioFactor = 0.04f; 
                decrementBase = 0.06f;
                break;
            case 1:
                configuracioFactor = 0.08f;
                decrementBase = 0.08f;
                break;
            case 2:
                configuracioFactor = 0.12f;
                decrementBase = 0.12f;
                break;
            case 3:
                configuracioFactor = 0.16f;
                decrementBase = 0.1f;
                break;
        }
    }

    private void haGuanyat()
    {
        if (force.value >= 0.96)
        {
            ValorsGenerals.Force += 1;
            if (ValorsGenerals.Force > 0 && ValorsGenerals.Force < 4) JugadorInventari.numeroPeses += 1;
            else if (ValorsGenerals.Force == 4) JugadorInventari.numeroPeses += 2;
            force.value = 0.2f;
            comencarNivell = false;
            Debug.Log(JugadorInventari.numeroPeses);
        }
    }


    private void haPerdut()
    {
        if (force.value <= 0.1)
        {
            if(ValorsGenerals.Force > 0)
            {
                ValorsGenerals.Force -= 1;
                if (ValorsGenerals.Force > 0 && ValorsGenerals.Force < 4) JugadorInventari.numeroPeses -= 1;
                else if (ValorsGenerals.Force == 4) JugadorInventari.numeroPeses -= 2;
            }      
            force.value = 0.2f;
            comencarNivell = false;
            Debug.Log(JugadorInventari.numeroPeses);
        }
    }
}

