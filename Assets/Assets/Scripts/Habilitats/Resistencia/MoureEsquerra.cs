using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MoureEsquerra : MonoBehaviour
{
    private float velocitat = 12f;
    private resistencia rs;


    private void Start()
    {
        rs = GameObject.FindWithTag("Jugador").GetComponent<resistencia>();
        //gameObject.transform.position = gameObject.transform.position;

    }


    void Update()
    {
        if (rs.comencarNivell)
        {
            transform.Translate(Vector2.left * velocitat * Time.deltaTime);

            if (transform.position.x < -32f)
            {
                Destroy(gameObject);
            }
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if(collision.gameObject.tag == "Jugador")
        {
            if (ValorsGenerals.resistencia > 0)
            {
                ValorsGenerals.resistencia -= 1;
                if (ValorsGenerals.resistencia > 0 && ValorsGenerals.resistencia < 4) JugadorInventari.numeroJaquetes -= 1;
                else if (ValorsGenerals.resistencia == 4) JugadorInventari.numeroJaquetes -= 2;
               
            }
            SceneManager.LoadScene(4);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Jugador")
        {
            SceneManager.LoadScene(4);
            if (ValorsGenerals.resistencia < 4)
            {
                ValorsGenerals.resistencia += 1;
                if (ValorsGenerals.resistencia > 0 && ValorsGenerals.resistencia < 4) JugadorInventari.numeroJaquetes += 1;
                else if (ValorsGenerals.resistencia == 4) JugadorInventari.numeroJaquetes += 2;
                SceneManager.LoadScene(4);
            }

        }
    }
}

