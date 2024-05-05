using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class seguimentCamera : MonoBehaviour
{
    public GameObject camera;
    public GameObject fons;

    private bool canMove = true;  // Variable para controlar si la cámara puede moverse

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log(other);

        if (canMove && other.gameObject.CompareTag("Jugador"))
        {
            if (gameObject.tag == "adalt")
            {
                MoureCamera(13.62f);
            }
            else if (gameObject.tag == "abaix")
            {
                MoureCamera(-13.62f);
            }
        }
    }

    // Método para mover la cámara con un retraso
    void MoureCamera(float offsetY)
    {
        canMove = false;  // Desactivar la capacidad de movimiento temporalmente
        Vector2 novaPos = new Vector2(camera.transform.position.x, camera.transform.position.y + offsetY);
        camera.transform.position = novaPos;
        fons.transform.position = novaPos;

        Invoke("RestaurarMoviment", 0.5f);
    }

    
    void RestaurarMoviment()
    {
        canMove = true;
    }
}
