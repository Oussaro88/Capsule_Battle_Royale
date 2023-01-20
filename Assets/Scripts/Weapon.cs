using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script cr�� par Sengsamrach Vong
/// </summary>

public class Weapon : MonoBehaviour
{
    private GameObject origin = null;
    private float timerDestroy = 3f;
    private float timerHarmless = 0.5f;
    private bool once = false;

    public void SetOrigin(GameObject character) //M�thode pour garder l'origine du Shoot
    {
        origin = character; //Garde le personnage qui a attaqu�
    }

    public GameObject GetOrigin() //M�thode pour envoyer l'origine du Shoot
    {
        return origin; //Retourne
    }

    private void DestroyWeapon() //M�thode pour d�truire le Weapon
    {
        Destroy(gameObject); //D�truire le gameObject de ce script
    }

    private void SetWeaponHarmless() //M�thode pour rendre l'arme inoffensif
    {
        gameObject.tag = "Damage"; //Mettre le tag du gameObject "Damage", et Non "Melee" ou "Range"
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (gameObject.tag == "Range")
        {
            Invoke("SetWeaponHarmless", timerHarmless); //Invoquer la m�thode SetWeaponHarmless() apr�s le timerHarmless pour faire en sorte que le projectile ne fait aucune d�g�ts apr�s une collision
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        once = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeInHierarchy && !once) //Condition If pour si le gameObject de ce script est actif dans la sc�ne et que le bool once est false
        {
            once = true; //Bool once est mis � true
            if (gameObject.tag == "Range")
            {
                Invoke("DestroyWeapon", timerDestroy); //Invoquer la m�thode DestroyWeapon() apr�s le timerDestroy pour d�truire la balle.
            }
        }
    }
}
