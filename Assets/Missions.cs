using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Missions : MonoBehaviour
{
    public List<string> llistaMissions = new List<string>();
    public List<GameObject> objectePregunta = new List<GameObject>();
    public int preguntaActual;
    public Text textPreguntes;
    public Text textFinal;

    private string[] respostesCorrectes;

    private List<string> respostesJugador = new List<string>();
    private int puntatge = 0;
    private bool final;

    void Start()
    {
        final = false;
        resultatPreguntes();
        preguntaActual = 0;
        objectePregunta[preguntaActual].SetActive(true);


    }

    // Update is called once per frame
    void Update()
    {
        if(!final)textPreguntes.text = llistaMissions[preguntaActual];

        if(final && Input.GetKeyDown(KeyCode.Mouse0)) SceneManager.LoadScene(1);

    }


    public void evaluarPregunta(string nomObj)
    {
        // Desactivar la pregunta actual antes de incrementar el índice
        objectePregunta[preguntaActual].SetActive(false);

        // Añadir la respuesta del jugador a la lista
        respostesJugador.Add(nomObj);

        // Incrementar el índice solo si no es la última pregunta
        if (preguntaActual < 9)
        {
            preguntaActual++;
            // Activar la siguiente pregunta
            objectePregunta[preguntaActual].SetActive(true);
        }
        else
        {
            // Si es la última pregunta, evaluar las respuestas del jugador
            evaluarJugador();
        }
    }


    private void resultatPreguntes()
    {
        respostesCorrectes = new string[]{ "a", "a", "c", "c", "b", "a", "a", "a", "a", "b" };

    }

    private void evaluarJugador()
    {
        objectePregunta[8].SetActive(false);
        objectePregunta[9].SetActive(false);

        for (int i = 0; i<10; i++)
        {
            if (respostesCorrectes[i] == respostesJugador[i]) puntatge++;
        }
        respostesFinals();
    }


    private void respostesFinals()
    {
        final = true;
        textFinal.text = "El teu puntatge ha estat de: " + puntatge;
        textPreguntes.text = "Clic esquerre per sortir al menu";
    }


}
