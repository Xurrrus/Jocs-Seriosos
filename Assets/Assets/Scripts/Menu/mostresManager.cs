using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class mostresManager : MonoBehaviour
{
    
    public int mostresComptador;
    public Text mostresText;
    public Text missioText;


    // Start is called before the first frame update
    void Start()
    {
        missioText.text = "Missio actual: " + ValorsGenerals.MissioActual;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        mostresText.text = "Comptador de mostres: " + mostresComptador.ToString();

    }

    public int retornarMostres(){
        return mostresComptador;
    }
}
