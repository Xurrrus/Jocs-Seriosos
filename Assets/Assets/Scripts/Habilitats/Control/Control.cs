using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Control : MonoBehaviour
{
    private float velocitat;
    private bool comencarNivell;
    private string orientacio = "esquerra";

    void Start()
    {
        comencarNivell = false;
        configuradorDificultat();
    }

    // Update is called once per frame
    void Update()
    {

            MovimentPlataforma();
            transform.Translate((orientacio == "esquerra") ? Vector2.right * velocitat * Time.deltaTime : Vector2.left * velocitat * Time.deltaTime);


    }

    private void MovimentPlataforma()
    {
        float xNoise = Mathf.PerlinNoise(Time.time * velocitat, 0f) * 2f - 1f;
        float yNoise = Mathf.PerlinNoise(0f, Time.time * velocitat) * 2f - 1f;

        Vector3 movement = new Vector3(xNoise, yNoise, 0f);
        transform.Translate(movement * Time.deltaTime);
    }

    private void configuradorDificultat()
    {
        switch (ValorsGenerals.Control)
        {
            case 0:
                velocitat = 5f;

                break;
            case 1:
                velocitat = 7f;
                break;
            case 2:
                velocitat = 10f;
                break;
            case 3:
                velocitat = 12f;
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Paret"))
        {
            orientacio = (orientacio == "esquerra") ? "dreta" : "esquerra";
        }
        Debug.Log("Es el jugador: " + collision.gameObject.CompareTag("Jugador"));
        Debug.Log("La plataforma: "  + gameObject.transform.tag == "Guanyat");
        if (collision.gameObject.CompareTag("Jugador") && gameObject.transform.tag == "Guanyat")
        {
            ValorsGenerals.Control += 1;
            if (ValorsGenerals.Control > 0 && ValorsGenerals.Control < 4) JugadorInventari.numeroGanxos += 1;
            else if (ValorsGenerals.Control == 4) JugadorInventari.numeroGanxos += 2;
            SceneManager.LoadScene(5);
        }
    }
}

