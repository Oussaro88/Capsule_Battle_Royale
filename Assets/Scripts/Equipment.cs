using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script créé par Sengsamrach Vong
/// </summary>

public class Equipment : MonoBehaviour
{
    //Melee : Knife = 30, Sword = 40, Spear = 40, Hammer = 40
    //Range : Rock = 30, Slinger = 35, Bow = 40, Gun = 50
    //Armor : Cloth = 15, Light = 20, Medium = 25, Heavy = 30

    public enum typeMelee { Knife, Sword, Spear, Hammer }
    public enum typeRange { Rock, Slinger, Bow, Gun }
    public enum typeArmor { Cloth, Light, Medium, Heavy }

    private float damage = 0;
    private float defense = 0;

    public float DamageMelee(typeMelee myMelee)
    {
        if(myMelee == typeMelee.Knife)
        {
            damage = 30;
            return damage;
        } 
        else if (myMelee == typeMelee.Sword)
        {
            damage = 40;
            return damage;
        } 
        else if (myMelee == typeMelee.Spear)
        {
            damage = 40;
            return damage;
        } 
        else if (myMelee == typeMelee.Hammer)
        {
            damage = 40;
            return damage;
        }
        damage = 30;
        return damage;
    }

    public float DamageRange(typeRange myRange)
    {
        if (myRange == typeRange.Rock)
        {
            damage = 30;
            return damage;
        }
        else if (myRange == typeRange.Slinger)
        {
            damage = 35;
            return damage;
        }
        else if (myRange == typeRange.Bow)
        {
            damage = 40;
            return damage;
        }
        else if (myRange == typeRange.Gun)
        {
            damage = 50;
            return damage;
        }
        damage = 30;
        return damage;
    }
    public float DefenseArmor(typeArmor myArmor)
    {
        if (myArmor == typeArmor.Cloth)
        {
            defense = 15;
            return defense;
        }
        else if (myArmor == typeArmor.Light)
        {
            defense = 20;
            return defense;
        }
        else if (myArmor == typeArmor.Medium)
        {
            defense = 25;
            return defense;
        }
        else if (myArmor == typeArmor.Heavy)
        {
            defense = 30;
            return defense;
        }
        defense = 15;
        return defense;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}