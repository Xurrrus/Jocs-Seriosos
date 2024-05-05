using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class terrenyNivell : MonoBehaviour
{
    [SerializeField]
    float forceVent;
    
    
    private GameObject vent;

    // void start(){

    //     vent = GameObject.Find("VentPantalla");

    // }

    private void OnTriggerStay2D(Collider2D other){

        if(other.gameObject.tag == "Jugador"){
            vent = GameObject.Find("VentPantalla");
            if(!vent.activeSelf)vent.SetActive(true);
            var rb = other.gameObject.GetComponent<Rigidbody2D>();
            var mv = other.gameObject.GetComponent<Moviment>();
            if (!mv.esTerra) { rb.AddRelativeForce(forceVent * -rb.transform.right); Debug.Log("vent"); }

            
        }
        
    }

    private void OnTriggerExit2D(Collider2D other){
        if(other.gameObject.tag == "Jugador"){
            vent = GameObject.Find("VentPantalla");
            vent.SetActive(false);
        }
    }


}
