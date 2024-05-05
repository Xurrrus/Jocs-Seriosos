using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnParets : MonoBehaviour
{

    private float contador;
    public resistencia rs;

    public GameObject[] parets;
    public GameObject objecteFondo;
    public GameObject finalNivell;
    private float tempsAparicio;

    public Transform llocSpawn;
    private List<Vector2> spawnParets = new List<Vector2>();

    void Start()
    {
        StartCoroutine(ApareixerGuanyar());
        ConfiguradorDificultat();
        PosarSpawnParets();
    }

    private void Update()
    {
        if (rs.comencarNivell)
        {
            contador += Time.deltaTime;
            if (contador > tempsAparicio)
            {
                SpawnPar();
                contador = 0;
            }
        }

        Vector2 movimiento = new Vector2(-1, 0);
        objecteFondo.transform.Translate(movimiento * 0.3f * Time.fixedDeltaTime);
    }

    void SpawnPar()
    {
        int ParetInst = Random.Range(0, parets.Length);
        Vector2 posicioSpawn = spawnParets[ParetInst];
        Instantiate(parets[ParetInst], posicioSpawn, Quaternion.identity);
    }

    private void ConfiguradorDificultat()
    {
        switch (ValorsGenerals.resistencia)
        {
            case 0:
                tempsAparicio = 2f;
                break;
            case 1:
                tempsAparicio = 1.8f;
                break;
            case 2:
                tempsAparicio = 1.4f;
                break;
            case 3:
                tempsAparicio = 1f;
                break;
        }
    }

    IEnumerator ApareixerGuanyar()
    {
        yield return new WaitForSeconds(30f);
        Instantiate(finalNivell, llocSpawn.position, Quaternion.identity);
    }

    private void PosarSpawnParets()
    {
        spawnParets.Add(new Vector2(26f, 0f));
        spawnParets.Add(new Vector2(26f, 2.1f));
        spawnParets.Add(new Vector2(26f, -3.6f));
        spawnParets.Add(new Vector2(26f, 0.7f));
        spawnParets.Add(new Vector2(26f, 3.5f));
    }


}

