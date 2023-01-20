using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Cr�e par : Oussama Arouch
/// </summary>


public class DoubleSpeedTrigger : MonoBehaviour
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
                manager.pDoubleSpeed++; //Incr�mentation de la variable double speed pour le player
                Debug.Log("Double Speed acquired");
                Destroy(gameObject); //Destruction du gameobject apr�s la collision
            }
            else if (collision.gameObject.name != "CapMan")
            {
                collision.gameObject.GetComponent<Enemy>().eDoubleSpeed++; //Incr�mentation de la variable double speed pour le opponent
            }
        }
    }
}
