using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class resistencia : MonoBehaviour
{
    private float moviment;
    private Rigidbody2D rb;
    public Text resist;

    public bool comencarNivell;
    private float velocitatMoviment = 5f;

    private Animator animation;

    void Start()
    {
        comencarNivell = false;
        rb = gameObject.GetComponent<Rigidbody2D>();
        animation = GetComponent<Animator>();
    }

    void Update()
    {
        if (comencarNivell)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            moviment = Input.GetAxisRaw("Horizontal");

            resist.text = "Nivell Resistencia: " + ValorsGenerals.resistencia.ToString();

            if (Input.GetKeyDown("space"))
            {
                // Reprodueix l'animació de salt des del principi
                animation.Play("Jump", -1, 0f);

                // Aplica la nova velocitat
                rb.velocity = new Vector2(moviment * velocitatMoviment, 10f);
            }
        }
        else
        {
            resist.text = "CLICA EL BOTO ESQUERRA DEL RATOLI PER JUGAR O DRET PER SORTIR AL MENU \nEL NIVELL DE RESISTENCIA ES: " + ValorsGenerals.resistencia.ToString();
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0)) comencarNivell = true;
        if (Input.GetKeyDown(KeyCode.Mouse1) && comencarNivell == false) SceneManager.LoadScene(1); //MENU
    }





    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Guanyat")
        {
            ValorsGenerals.resistencia += 1;
            if (ValorsGenerals.resistencia > 0 && ValorsGenerals.resistencia < 4) JugadorInventari.numeroJaquetes += 1;
            else if (ValorsGenerals.resistencia == 4) JugadorInventari.numeroJaquetes += 2;
            SceneManager.LoadScene(4);
        }
    }


}
