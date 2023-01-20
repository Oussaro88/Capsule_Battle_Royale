using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Crée par : Oussama Arouch
/// </summary>


public class InvincibilityTrigger : MonoBehaviour
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
                manager.pInvincibility++; //Incrémentation de la variable invincibility pour le player
                Debug.Log("Invincibility acquired");
                Destroy(gameObject); //Destruction du gameobject après la collision
            }
            else if (collision.gameObject.name != "CapMan")
            {
                collision.gameObject.GetComponent<Enemy>().eInvincibility++; //Incrémentation de la variable invincibility pour le opponent
            }
        }
    }
}
