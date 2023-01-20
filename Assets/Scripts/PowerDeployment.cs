using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Crée par : Oussama Arouch
/// </summary>

public class PowerDeployment : MonoBehaviour
{
    //Tableaux des pouvoirs
    [SerializeField] private GameObject[] powerRank1 = null; //Rang 1
    [SerializeField] private GameObject[] powerRank2 = null; //Rang 2
    [SerializeField] private GameObject[] powerRank3 = null; //Rang 3
    int randomPower = 0;
    private bool instantiateOnce1 = false; //est-ce que le pouvoir a été instancié ?
    private bool instantiateOnce2 = false; //est-ce que le pouvoir a été instancié ?
    private bool instantiateOnce3 = false; //est-ce que le pouvoir a été instancié ?

    //tableau pour les spawn points
    [SerializeField] private Transform[] spawnPositions = null;
    int randomPositionSpawn = 0;

    private float timer = 0f; //timer

    public void SetRandomPosition()
    {
        do
        {
            randomPositionSpawn = Random.Range(0, spawnPositions.Length); //récupère une position random dans le tableau des spawn points
        } while (spawnPositions[randomPositionSpawn].gameObject.activeInHierarchy); //si dans uebn position un gameobject a déjà été instancié
        spawnPositions[randomPositionSpawn].gameObject.SetActive(true); //active le gameobject à la position choisie
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime; //timer

        //Pouvoir de RANG 1
        if(timer >= 5f && timer < 20f)
        {
            if (!instantiateOnce1)
            {
                SetRandomPosition(); //Choisit une position random
                randomPower = Random.Range(0, powerRank1.Length); //Choisit un power random
                GameObject power1 = Instantiate(powerRank1[randomPower], spawnPositions[randomPositionSpawn].position, spawnPositions[randomPositionSpawn].rotation, spawnPositions[randomPositionSpawn]); //instancie le préfab du power
                instantiateOnce1 = true; //est-ce que le pouvoir a été instancié ?
            }
        }

        //Pouvoir de RANG 2
        else if (timer >= 20f && timer < 50f)
        {
            if (!instantiateOnce2)
            {
                SetRandomPosition(); //Choisit une position random
                randomPower = Random.Range(0, powerRank2.Length); //Choisit un power random
                GameObject power2 = Instantiate(powerRank2[randomPower], spawnPositions[randomPositionSpawn].position, spawnPositions[randomPositionSpawn].rotation, spawnPositions[randomPositionSpawn]); //instancie le préfab du power
                instantiateOnce2 = true; //est-ce que le pouvoir a été instancié ?
            }
        }

        //Pouvoir de RANG 3
        else if (timer >= 50f && timer < 80f)
        {
            if (!instantiateOnce3)
            {
                SetRandomPosition(); //Choisit une position random
                randomPower = Random.Range(0, powerRank3.Length); //Choisit un power random
                GameObject power3 = Instantiate(powerRank3[randomPower], spawnPositions[randomPositionSpawn].position, spawnPositions[randomPositionSpawn].rotation, spawnPositions[randomPositionSpawn]); //instancie le préfab du power
                instantiateOnce3 = true; //est-ce que le pouvoir a été instancié ?
            }
        }


    }
}
