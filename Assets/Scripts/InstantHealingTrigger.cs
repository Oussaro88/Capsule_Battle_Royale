using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Crée par : Oussama Arouch
/// </summary>


public class InstantHealingTrigger : MonoBehaviour
{
    private GameManager manager; //Mon GameManager

    // Start is called before the first frame update
    void Start()
    {
        manager = GameManager.instance; //référence à mon gamemanager
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Character")
        {
            if (collision.gameObject.name == "CapMan")
            {
                manager.pInstantHealing++; //Incrémentation de la variable instant healing pour le player
                Debug.Log("Instant Healing acquired");
                Destroy(gameObject); //Destruction du gameobject après la collision
            }
            else if (collision.gameObject.name != "CapMan")
            {
                collision.gameObject.GetComponent<Enemy>().eInstantHealing++; //Incrémentation de la variable instant healing pour le opponent
            }
        }
    }
}
