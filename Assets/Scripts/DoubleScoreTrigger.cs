using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Cr�e par : Oussama Arouch
/// </summary>


public class DoubleScoreTrigger : MonoBehaviour
{
    private GameManager manager; //Mon GameManager

    // Start is called before the first frame update
    void Start()
    {
        manager = GameManager.instance; //r�f�rence � mon gamemanager
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Character" && collision.gameObject.name == "CapMan")
        {
            manager.pDoubleScore++; //Incr�mentation de la variable double score pour le player
            Debug.Log("Double Score acquired");
            Destroy(gameObject); //Destruction du gameobject apr�s collision
        }
    }

}
