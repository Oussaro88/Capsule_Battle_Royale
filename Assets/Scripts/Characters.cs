using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script cr�� par Sengsamrach Vong
/// </summary>

public abstract class Characters : MonoBehaviour
{
    [SerializeField] private float hp; //float pour d�signer le hp 
    [SerializeField] private float hpMax; //float pour d�signer le hpmax
    private Equipment.typeMelee myMelee;
    private Equipment.typeRange myRange;
    private Equipment.typeArmor myArmor;

    public Characters() //Constructeur par d�faut
    {
        this.hp = 200f;
        this.hpMax = 200f;
        this.myMelee = Equipment.typeMelee.Knife;
        this.myRange = Equipment.typeRange.Rock;
        this.myArmor = Equipment.typeArmor.Cloth;
    }

    public Characters(float hp, float defense)  
    {
        this.hp = hp;
        this.hpMax = hp;
        this.myMelee = Equipment.typeMelee.Knife;
        this.myRange = Equipment.typeRange.Rock;
        this.myArmor = Equipment.typeArmor.Cloth;
    }

    //Encapsulation
    public float Hp { get => hp; set => hp = value; }
    public float HpMax { get => hpMax; set => hpMax = value; }
    public Equipment.typeMelee MyMelee { get => myMelee; set => myMelee = value; }
    public Equipment.typeRange MyRange { get => myRange; set => myRange = value; }
    public Equipment.typeArmor MyArmor { get => myArmor; set => myArmor = value; }

    public abstract void ReceiveDamage(); //M�thode Abstract ReceiveDamage

    public abstract void IsDead(); //M�thode Abstract IsDead()

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
