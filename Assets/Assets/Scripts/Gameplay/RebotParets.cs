using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RebotParets : MonoBehaviour
{
    
    public GameObject jugador;

    public float multiplicadorForce;

    void Start()
    {

        
    }

    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other){

        if(other.gameObject.tag == "Jugador"){
            Debug.Log("HOLAA");
            Vector2 velocitatRebot = new Vector2((gameObject.transform.position.x - jugador.transform.position.x) * multiplicadorForce,(gameObject.transform.position.y - jugador.transform.position.y) * multiplicadorForce);
            jugador.GetComponent<Rigidbody2D>().velocity -= velocitatRebot;
        }
    }
}
