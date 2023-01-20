using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Cr�e par : Oussama Arouch
/// </summary>


public class InvisibilityTrigger : MonoBehaviour
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
                manager.pInvisibility++; //Incr�mentation de la variable invisibility pour le player
                Debug.Log("Invisibility acquired");
                Destroy(gameObject); //Destruction du gameobject apr�s la collision
            }
            else if (collision.gameObject.name != "CapMan")
            {
                collision.gameObject.GetComponent<Enemy>().eInvisibility++; //Incr�mentation de la variable invisibility pour le opponent
            }
        }
    }
}
