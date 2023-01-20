using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Cr�e par : Oussama Arouch
/// </summary>


public class DoubleDamageTrigger : MonoBehaviour
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
                manager.pDoubleDamage++; //Incr�mentation de la variable double damage pour le player
                Debug.Log("Double Damage acquired");
                Destroy(gameObject); //Destruction du gameobject apr�s collision
            }
            else if (collision.gameObject.name != "CapMan")
            {
                collision.gameObject.GetComponent<Enemy>().eDoubleDamage++; //Incr�mentation de la variable double damage pour le opponent
            }
        }
    }
}
