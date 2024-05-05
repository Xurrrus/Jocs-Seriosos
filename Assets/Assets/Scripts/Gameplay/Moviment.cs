using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Moviment : MonoBehaviour
{
    public float velocitatMoviment;
    private float moviment;
    public bool esTerra;
    private Rigidbody2D rb;
    private float abastament = 10f;
    public LayerMask maskTerra;

    public PhysicsMaterial2D rebotMat, normalMat;
    public bool potSaltar = true;
    public float valorSalt = 0f;

    public float multiplicadorForce = 10f;
    
    public mostresManager mm;
    public GameObject spriteGanxo;
    //public GameObject spritePujar;

    private SpriteRenderer sp;

    Vector2 direccion = new Vector2(0,0);
    Vector2 posicioNova = new Vector2(0,0);
    private RaycastHit2D plataforma;
    private Vector2 velocitat = Vector2.zero;

    private bool donat = false;
    private Animator animator;
    private SpriteRenderer personatge;

    private float acceleracio = 10f;
    private float acceleracioMaxima = 20f; // Ajusta segons necessitat


    void Start()
    {

        rb = gameObject.GetComponent<Rigidbody2D>();
        sp = spriteGanxo.GetComponent<SpriteRenderer>();
        

    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) SceneManager.LoadScene(1); //MENU

        moviment = Input.GetAxisRaw("Horizontal");
        Debug.Log("el moviment " + moviment);

        animator = gameObject.GetComponent<Animator>();
        personatge = gameObject.GetComponent<SpriteRenderer>();

       
        if (animator == null)
        {
            Debug.LogError("Animator is not assigned to the player GameObject!");
            return;
        }
        if (moviment != 0)
        {
            // Si hi ha moviment, activem l'animació de "run"
            animator.SetBool("Correr", true);
            if (velocitatMoviment == 8) {animator.speed = 1.6f;}
            animator.SetBool("Quiet", false);
        }
        else
        {
            // Si no hi ha moviment, activem l'animació de "idle"
            animator.SetBool("Correr", false);
            if (velocitatMoviment == 8) { animator.speed = 1f;}
            animator.SetBool("Quiet", true);
        }

        if (spriteGanxo)
        {
            Vector2 localPosition = spriteGanxo.transform.localPosition;
            if (moviment == 1)
            {
                sp.flipX = false;
                personatge.flipX = false;
                direccion = new Vector2(1, 1);
                localPosition.x = 1.3f * Mathf.Sign(transform.localScale.x);
            }
            else if (moviment == -1)
            {
                sp.flipX = true;
                personatge.flipX = true;
                direccion = new Vector2(-1, 1);
                localPosition.x = -1.3f * Mathf.Sign(transform.localScale.x);
            }
            spriteGanxo.transform.localPosition = localPosition;
        }
        

       
        if (valorSalt == 0f && esTerra)
        {
            Debug.Log("es a terra amb salt 0");
            animator.SetBool("salt", false);
            animator.SetBool("PrepararSalt", false);
            animator.SetBool("Terra", true);
            rb.velocity = new Vector2(moviment * velocitatMoviment, rb.velocity.y);

        }

        esTerra = IsGrounded();

        if (!esTerra)
        {
            Debug.Log("saltar");
            animator.SetBool("PrepararSalt", false);
            animator.SetBool("salt", true);
            animator.SetBool("Quiet", false);
            animator.SetBool("Terra", false);
        }


        if (valorSalt > 0)
        {

            rb.sharedMaterial = rebotMat;

        }
        else
        {

            rb.sharedMaterial = normalMat;
        }

        if (Input.GetKey("space") && esTerra && potSaltar)
        {
          
            valorSalt += acceleracio * Time.deltaTime*1.2f;
            valorSalt = Mathf.Min(valorSalt, acceleracioMaxima); // Limita la acceleració màxima
            animator.SetBool("PrepararSalt", true);
            animator.SetBool("Quiet", false);
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }


        if (valorSalt >= 20f && esTerra)
        {
            
            float tempx = moviment * velocitatMoviment;
            float tempy = valorSalt;
            rb.velocity = new Vector2(tempx, tempy);

            Invoke("resetSalt", 0.2f);

        }

        if (Input.GetKeyUp("space"))
        {

            if (esTerra)
            {
                
                rb.velocity = new Vector2(moviment * velocitatMoviment, valorSalt);
                valorSalt = 0f;
            }
            potSaltar = true;

        }

        if (Input.GetKeyUp(KeyCode.X) && spriteGanxo)
        {
            RaycastHit2D hit;
            
            
            Vector2 origen = gameObject.transform.position;

            Debug.DrawRay(origen, direccion *abastament, Color.white, 1.0f);

            Debug.Log("Disparat");

            if (hit = Physics2D.Raycast(origen, direccion , abastament))
            {
                Debug.Log("he tocat: " + hit.transform.name);
                if (hit.collider.gameObject.layer == 3)
                {
                    Debug.Log("Tocat tete: " + plataforma);
                    plataforma = hit;
                    rb.simulated = false;
                    donat = true;
                }
            }
        }

        if (donat)
        {
            float suavizado = 0.3f; // Ajusta el suavizado según tus necesidades
            posicioNova = plataforma.transform.position;
            posicioNova.y += 1.5f;
            transform.position = Vector2.SmoothDamp(transform.position, posicioNova, ref velocitat, suavizado);


            // Si el jugador está lo suficientemente cerca de la posición final, termina el movimiento

            float distancia = MathF.Abs(transform.position.y - posicioNova.y);
            if (distancia < 0.2f)
            {
                donat = false;
                rb.simulated = true;
            }
        }


    }
    void resetSalt(){

        potSaltar = false;
        valorSalt = 0f;

    }
    void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.CompareTag("Paret")){
            // Obtenir la normal de la superfície de la paret
            Vector2 normalParet = collision.contacts[0].normal;

            // Calcular la direcció de rebote (inversa de la normal, amb la y positiva)
            Vector2 direccioRebote = new Vector2(normalParet.x, normalParet.y).normalized;

            // Aplicar una força d'impuls amb la direcció de rebote i el multiplicador
            rb.AddForce(direccioRebote * multiplicadorForce, ForceMode2D.Impulse);

        }
        if (collision.gameObject.CompareTag("Plataforma"))
        {
            gameObject.transform.SetParent(collision.gameObject.transform);
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Plataforma"))
        {
            gameObject.transform.SetParent(null);
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("recollible"))
        {

            Destroy(other.gameObject);
            mm.mostresComptador++;

        }


    }

 

    private bool IsGrounded(){
        Debug.Log("Raycast: " + (bool)Physics2D.Raycast(gameObject.transform.position, -Vector2.up,6f,maskTerra));
        return Physics2D.Raycast(gameObject.transform.position, -Vector2.up,6f,maskTerra);
    }



}
