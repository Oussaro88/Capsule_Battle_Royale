using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Crée par : Oussama Arouch
/// </summary>


public class InvisibilityTrigger : MonoBehaviour
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
                manager.pInvisibility++; //Incrémentation de la variable invisibility pour le player
                Debug.Log("Invisibility acquired");
                Destroy(gameObject); //Destruction du gameobject après la collision
            }
            else if (collision.gameObject.name != "CapMan")
            {
                collision.gameObject.GetComponent<Enemy>().eInvisibility++; //Incrémentation de la variable invisibility pour le opponent
            }
        }
    }
}
