using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script créé par Sengsamrach Vong
/// </summary>

public class Weapon : MonoBehaviour
{
    private GameObject origin = null;
    private float timerDestroy = 3f;
    private float timerHarmless = 0.5f;
    private bool once = false;

    public void SetOrigin(GameObject character) //Méthode pour garder l'origine du Shoot
    {
        origin = character; //Garde le personnage qui a attaqué
    }

    public GameObject GetOrigin() //Méthode pour envoyer l'origine du Shoot
    {
        return origin; //Retourne
    }

    private void DestroyWeapon() //Méthode pour détruire le Weapon
    {
        Destroy(gameObject); //Détruire le gameObject de ce script
    }

    private void SetWeaponHarmless() //Méthode pour rendre l'arme inoffensif
    {
        gameObject.tag = "Damage"; //Mettre le tag du gameObject "Damage", et Non "Melee" ou "Range"
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (gameObject.tag == "Range")
        {
            Invoke("SetWeaponHarmless", timerHarmless); //Invoquer la méthode SetWeaponHarmless() après le timerHarmless pour faire en sorte que le projectile ne fait aucune dégâts après une collision
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
        if (gameObject.activeInHierarchy && !once) //Condition If pour si le gameObject de ce script est actif dans la scène et que le bool once est false
        {
            once = true; //Bool once est mis à true
            if (gameObject.tag == "Range")
            {
                Invoke("DestroyWeapon", timerDestroy); //Invoquer la méthode DestroyWeapon() après le timerDestroy pour détruire la balle.
            }
        }
    }
}
