using UnityEngine;
using UnityEngine.UI;

public class ManagerMissioFeedback : MonoBehaviour
{
    public GameObject planetes;
    public GameObject objetoA;
    public GameObject objetoB;
    public GameObject objetoC;

    private string nombreBoton;

    // Método que se llama cuando se pulsa un botón
    void Update()
    {
        // Obtener el nombre del botón que fue pulsad
        Debug.Log(nombreBoton);
        // Dependiendo del nombre del botón, mostrar el objeto correspondiente
        if (nombreBoton == "Mart")
        {
            MostrarObjeto(objetoC);
        }
        else if (nombreBoton == "Jupiter")
        {
            MostrarObjeto(objetoB);
        }
        else if (nombreBoton == "Saturn")
        {
            MostrarObjeto(objetoB);
        }
        else if (nombreBoton == "Ura")
        {
            MostrarObjeto(objetoB);
        }
        else if (nombreBoton == "Neptu")
        {
            MostrarObjeto(objetoA);
        }
        else if (nombreBoton == "Mercuri")
        {
            MostrarObjeto(objetoC);
        }
        else if (nombreBoton == "Venus")
        {
            MostrarObjeto(objetoC);
        }
        else if (nombreBoton == "Terra")
        {
            MostrarObjeto(objetoC);
        }
        // Agrega más casos según la cantidad de botones y objetos que tengas
    }

    public void botons()
    {
        // Acceder al GameObject del botón que fue pulsado
        GameObject botonPulsado = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;

        // Obtener el nombre del botón pulsado
        nombreBoton = botonPulsado.name;


    }


    void MostrarObjeto(GameObject objetoAMostrar)
    {
        planetes.SetActive(false);
        if(objetoAMostrar == objetoA)objetoA.SetActive(true);
        else if(objetoAMostrar == objetoB)objetoB.SetActive(true);
        else if(objetoAMostrar == objetoC)objetoC.SetActive(true);
      
    }

    public void tornarMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}

