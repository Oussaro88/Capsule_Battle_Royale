using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Cr�e par : Oussama Arouch
/// </summary>


public class InvincibilityTrigger : MonoBehaviour
{
    private GameManager manager; //Mon GameManager

    // Start is called before the first frame update
    void Start()
    {
        manager = GameManager.instance; //r�f�rence � mon gamemanager
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Character")
        {
            if (collision.gameObject.name == "CapMan")
            {
                manager.pInvincibility++; //Incr�mentation de la variable invincibility pour le player
                Debug.Log("Invincibility acquired");
                Destroy(gameObject); //Destruction du gameobject apr�s la collision
            }
            else if (collision.gameObject.name != "CapMan")
            {
                collision.gameObject.GetComponent<Enemy>().eInvincibility++; //Incr�mentation de la variable invincibility pour le opponent
            }
        }
    }
}
