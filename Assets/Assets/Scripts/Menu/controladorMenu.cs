using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;


public class controladorMenu : MonoBehaviour
{

    public GameObject menuPrincipal;
    public GameObject menuMissions;
    public GameObject menuPlanetes;
    public GameObject menuOpcions;
    public GameObject menuRanking;
    public GameObject menuHabilitats;

    public GameObject[] menus;

    public Text llistamisssions;
    public Text missioActual;
    public Text mostres;
    public InputField numeroMissio;

    private List<string> missions; 
    
    void Start()
    {
        menus = new GameObject[ ]{ menuOpcions, menuMissions, menuPlanetes, menuRanking,menuHabilitats };

        missions = new List<string>();
        missions.Add("1 - Fa poc la sonda Kaelis – 4, el mes lluny de la terra, ha detectat una forta pluja acida, investiga d'on prove aquesta \n");
        missions.Add("2 - Des del laboratori d'R+D necessiten mostres de gas eta per a fer un nou experiment amb vestimenta espacial, busca 5 mostres d'aquest gas per ells.");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CarregarNeptu(){

        SceneManager.LoadScene(2);

    }
    public void CarregarForce()
    {

        SceneManager.LoadScene(6);

    }
    public void CarregarControl()
    {

        SceneManager.LoadScene(5);

    }
    public void CarregarResistencia()
    {

        SceneManager.LoadScene(4);

    }
    public void CarregarAgilitat()
    {

        SceneManager.LoadScene(3);

    }

    public void carregarTriarMissio(){

        menuPrincipal.SetActive(false);
        menuMissions.SetActive(true);

        string result = "Llista de missions disponibles: " + "\n";
        foreach (var item in missions)
        {
            result += item.ToString() + "\n";
            
        }

        llistamisssions.text = result;


    }

    public void triarMissio(string missioTriada){

        int numero = int.Parse(numeroMissio.text);
        
        string missioSeleccionada = missions[numero-1];

        Debug.Log(missioSeleccionada);

        ValorsGenerals.MissioActual = missioSeleccionada;

        
        menuMissions.SetActive(false);
        menuPlanetes.SetActive(true);

        missioActual.text = "Missio: " + missioSeleccionada;

    }


    public void ExitJoc(){

        Application.Quit();

    }

    public void opcions(){

        menuPrincipal.SetActive(false);
        menuOpcions.SetActive(true);


    }

    public void tornarMenu(){

        menuPrincipal.SetActive(true);
        Debug.Log("no va");
        foreach (GameObject menu in menus){
            if (menu != null){
                menu.SetActive(false);
            }
        }

    }

    public void MenuRanking()
    {
        menuPrincipal.SetActive(false);
        menuRanking.SetActive(true); 
        
    }
    public void Habilitats()
    {
        menuPlanetes.SetActive(false);
        menuHabilitats.SetActive(true);

    }

}