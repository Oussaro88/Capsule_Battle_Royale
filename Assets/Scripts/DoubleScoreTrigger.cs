using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Crée par : Oussama Arouch
/// </summary>


public class DoubleScoreTrigger : MonoBehaviour
{
    private GameManager manager; //Mon GameManager

    // Start is called before the first frame update
    void Start()
    {
        manager = GameManager.instance; //référence à mon gamemanager
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Character" && collision.gameObject.name == "CapMan")
        {
            manager.pDoubleScore++; //Incrémentation de la variable double score pour le player
            Debug.Log("Double Score acquired");
            Destroy(gameObject); //Destruction du gameobject après collision
        }
    }

}
