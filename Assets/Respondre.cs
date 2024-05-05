using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respondre : MonoBehaviour
{
    public Missions ms;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Resposta"))
        {
            string nomObj = collision.gameObject.transform.name;
            ms.evaluarPregunta(nomObj);
            


        }
    }
}
