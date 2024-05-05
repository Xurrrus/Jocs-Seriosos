using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TornarMenu : MonoBehaviour
{
    public mostresManager mm;
    void OnTriggerEnter2D(Collider2D other){

        if(other.gameObject.tag == "Jugador"){
            ValorsGenerals.numeroMostres = mm.retornarMostres();
            SceneManager.LoadScene(7);

        }

    }
}
