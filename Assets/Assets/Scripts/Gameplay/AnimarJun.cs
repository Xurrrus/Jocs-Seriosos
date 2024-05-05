using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class AnimarJun : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool haParat;
    private float timer;
    public Animator animator;
  
    public GameObject textJun;
    public GameObject textJun1;
    public GameObject textJun2;

    void Start()
    {
        haParat = false;
        rb = gameObject.GetComponent<Rigidbody2D>();

    }

    
    void Update()
    {
        if(!haParat) rb.velocity = new Vector2(1.3f, 0);
        if(haParat)timer += Time.deltaTime;
        if(timer>36)
        {
            
            textJun1.SetActive(false);
            textJun2.SetActive(true);
            Debug.Log(textJun2.activeSelf);
        }
        if (Input.GetKeyDown(KeyCode.Space)) SceneManager.LoadScene(8);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.transform.tag == "Paret")
        {
            haParat = true;
            animator.SetBool("parar",true);
            textJun.SetActive(true);
        }
    }
}
