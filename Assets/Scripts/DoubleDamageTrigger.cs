using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Crée par : Oussama Arouch
/// </summary>


public class DoubleDamageTrigger : MonoBehaviour
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
                manager.pDoubleDamage++; //Incrémentation de la variable double damage pour le player
                Debug.Log("Double Damage acquired");
                Destroy(gameObject); //Destruction du gameobject après collision
            }
            else if (collision.gameObject.name != "CapMan")
            {
                collision.gameObject.GetComponent<Enemy>().eDoubleDamage++; //Incrémentation de la variable double damage pour le opponent
            }
        }
    }
}
