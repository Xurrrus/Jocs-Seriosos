using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColMeteorit : MonoBehaviour
{
    private SpawnMeteorits spMeteor;



    private void Start()
    {
        spMeteor = GameObject.FindWithTag("Jugador").GetComponent<SpawnMeteorits>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null)
        {
            if(collision.gameObject.tag == "abaix")
            {
                
                spMeteor.llistaMeteorits.RemoveAt(0);
                spMeteor.terraMeteorit();
                Destroy(gameObject);
                
            }
            if (collision.gameObject.tag == "Jugador")
            {
                spMeteor.haPerdut = true;
               
            }
        }
    }
}
