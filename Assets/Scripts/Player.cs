using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script cr�� par Sengsamrach Vong, � part au code en lien avec Power dans Update() par Oussama Arouch
/// </summary>

public class Player : Characters
{
    private Power power;

    public Player() { } //Constructeur d�faut
    public Player(float hp, float defense) : base(hp, defense) { }  //Constructeur avec les variables deu script Characters

    private GameManager manager; //Script de manager
    private BaseArea baseAreaBenefit; //Script de BaseArea

    private GameObject damageOrigin = null;
    private string dmgType = " ";
    private bool iPowerOn = false;
    private float dmgPowerOn = 1f;

    [SerializeField] private GameObject baseArea = null; //GameObject pour le baseArea du joueur
    [SerializeField] private Slider sliderHealthBar = null; //Slider pour la barre de vie du joueur
    [SerializeField] private Image fillHealthBar = null; //Image pour jouer avec le UI de la barre de vie.
    [SerializeField] private Text txtHealthBar = null; //Text pour afficher la valeur de la barre de vie.

    [SerializeField] private Image[] imgEquipInventory = new Image[3];
    [SerializeField] private Sprite[] imgEquipment = new Sprite[12];

    private float dmgReceived = 0; //float pour garder la valeur de damage recu.

    public override void ReceiveDamage() //M�thode pour calculer la vie du joueur selon le d�g�t re�u
    {
        if (!iPowerOn)
        {
            if (damageOrigin != null)
            {
                if (dmgType == "Melee")
                {
                    dmgReceived = damageOrigin.GetComponent<Equipment>().DamageMelee(damageOrigin.GetComponent<Enemy>().MyMelee) * damageOrigin.GetComponent<Enemy>().GetEnemyBaseAreaBenefit().BaseDamageBenefit() * damageOrigin.GetComponent<Enemy>().SendDoubleDamage() - gameObject.GetComponent<Equipment>().DefenseArmor(MyArmor) * baseAreaBenefit.BaseDefenseBenefit();
                }
                else if (dmgType == "Range")
                {
                    dmgReceived = damageOrigin.GetComponent<Equipment>().DamageRange(damageOrigin.GetComponent<Enemy>().MyRange) * damageOrigin.GetComponent<Enemy>().GetEnemyBaseAreaBenefit().BaseDamageBenefit() * damageOrigin.GetComponent<Enemy>().SendDoubleDamage() - gameObject.GetComponent<Equipment>().DefenseArmor(MyArmor) * baseAreaBenefit.BaseDefenseBenefit();
                }

                this.Hp -= dmgReceived; //Soustrait le Hp par le dmgReceived.
            }
        }
    }

    public void SetInactiveBasePlayer()
    {
        baseAreaBenefit.LoseBaseArea();
    }

    public void SetPlayerEquipment()
    {
        gameObject.GetComponent<Strike>().SetMelee(MyMelee); //Need to call it when changing melee
        gameObject.GetComponent<Shoot>().SetRange(MyRange); //Need to call it when changing range
    }

    public void SetHealthBar() //M�thode pour ajuster la barre de vie
    {
        SetFillHealthBar(); //Aller � la m�thode SetFillHealthBar() pour bouger la barre de vie
        SetColorHealthBar(); //Aller � la m�thode SetColorHealthBar() pour changer la couleur de la barre de vie selon la valeur.
        SetTextHealthBar(); //Aller � la m�thode SetTextHealthBar() pour changer la valeur affich� de la barre de vie.
    }

    public void SetFillHealthBar() //M�thode pour bouger la barre de vie
    {
        sliderHealthBar.value = this.Hp; //Donner la valeur du slider la vie du joueur.
    }

    public void SetColorHealthBar() //M�thode pour changer la couleur de la barre de vie selon la valeur. n
    {
        if (this.Hp >= 0.6 * this.HpMax) //Condition If pour si la vie du joueur est sup�rieure ou �gale � 60% de sa vie maximum.
        {
            fillHealthBar.color = Color.green; //Changer la couleur du fillHealthBar par Vert
        }
        else if (this.Hp >= 0.3 * this.HpMax && this.Hp < 0.6 * this.HpMax) //Condition If pour si la vie du joueur est sup�rieure ou �gale � 30% et inf�rieur � 60% de sa vie maximum.
        {
            fillHealthBar.color = Color.yellow; //Changer la couleur du fillHealthBar par Jaune
        }
        else if (this.Hp >= 0 * this.HpMax && this.Hp < 0.3 * this.HpMax) //Condition If pour si la vie du joueur est sup�rieure ou �gale � 0% et inf�rieur � 30% de sa vie maximum.
        {
            fillHealthBar.color = Color.red; //Changer la couleur du fillHealthBar par Rouge
        }
    }

    public void SetTextHealthBar() //M�thode pour changer la valeur affich� de la barre de vie.
    {
        if (this.Hp >= 100) //Condition If pour si la valeur de Hp est sup�rieure ou �gale � 100.
        {
            txtHealthBar.text = Convert.ToInt32(this.Hp).ToString("D3"); //Afficher la valeur de Hp � 3 chiffres
        }
        else if (this.Hp < 100) //Condition If pour si la valeur de Hp est inf�rieure � 100.
        {
            txtHealthBar.text = Convert.ToInt32(this.Hp).ToString("D2"); //Afficher la valeur de Hp � 2 chiffres
        }
        if (this.Hp <= 0) //Condition If pour si la valeur de Hp est inf�rieure ou �gale � 0.
        {
            txtHealthBar.text = Convert.ToInt32(this.Hp * 0).ToString("D1"); //Afficher 0
        }
    }

    public void SetImgEquipment()
    {
        switch (MyArmor)
        {
            case Equipment.typeArmor.Cloth:
                imgEquipInventory[0].sprite = imgEquipment[0];
                break;
            case Equipment.typeArmor.Light:
                imgEquipInventory[0].sprite = imgEquipment[1];
                break;
            case Equipment.typeArmor.Medium:
                imgEquipInventory[0].sprite = imgEquipment[2];
                break;
            case Equipment.typeArmor.Heavy:
                imgEquipInventory[0].sprite = imgEquipment[3];
                break;
        }
        
        switch (MyMelee)
        {
            case Equipment.typeMelee.Knife:
                imgEquipInventory[1].sprite = imgEquipment[4];
                break;
            case Equipment.typeMelee.Sword:
                imgEquipInventory[1].sprite = imgEquipment[5];
                break;
            case Equipment.typeMelee.Spear:
                imgEquipInventory[1].sprite = imgEquipment[6];
                break;
            case Equipment.typeMelee.Hammer:
                imgEquipInventory[1].sprite = imgEquipment[7];
                break;
        }
        
        switch (MyRange)
        {
            case Equipment.typeRange.Rock:
                imgEquipInventory[2].sprite = imgEquipment[8];
                break;
            case Equipment.typeRange.Slinger:
                imgEquipInventory[2].sprite = imgEquipment[9];
                break;
            case Equipment.typeRange.Bow:
                imgEquipInventory[2].sprite = imgEquipment[10];
                break;
            case Equipment.typeRange.Gun:
                imgEquipInventory[2].sprite = imgEquipment[11];
                break;
        }
    }

    public void IsInvincibilityOn(bool on)
    {
        iPowerOn = on;
    }

    public void SetDoubleDamageOn(float mul)
    {
        dmgPowerOn = mul;
    }

    public float SendDoubleDamageOn()
    {
        return dmgPowerOn;
    }

    public BaseArea GetPlayerBaseAreaBenefit()
    {
        return baseAreaBenefit;
    }

    public override void IsDead() //M�thode pour si le joueur est mort
    {
        manager.GameOver(); //Aller � la m�thode GameOver() du script GameManager
    }

    public bool ReturnDead() //M�thode pour retourner un false.
    {
        return false; //Retourne False
    }

    private void OnCollisionEnter(Collision collision) //M�thode pour si il y a une collision.
    {
        if (collision.gameObject.tag == "Melee")
        {
            damageOrigin = collision.gameObject.GetComponent<Weapon>().GetOrigin();
            dmgType = "Melee";
            ReceiveDamage();
        }
        if (collision.gameObject.tag == "Range")
        {
            damageOrigin = collision.gameObject.GetComponent<Weapon>().GetOrigin();
            dmgType = "Range";
            ReceiveDamage();
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        manager = GameManager.instance; //Singleton du GameManager
        baseAreaBenefit = baseArea.GetComponent<BaseArea>(); //Cache le baseAreaBenefit
        sliderHealthBar.value = this.Hp; //Initialise la valeur du slider au Hp du joueur.
        sliderHealthBar.maxValue = this.HpMax; //Initialise la valeur maximale du slider au HpMax du joueur.
        SetImgEquipment();
        SetPlayerEquipment();
        power = GetComponent<Power>();
    }

    // Update is called once per frame
    void Update()
    {
        SetHealthBar(); //Aller � la m�thode SetHealthBar() pour ajuster la barre de vie du joueur.

        if (this.Hp <= 0f) //Condition If pour si le Hp est inf�rieur ou �gal � 0
        {
            SetHealthBar(); //Aller � la m�thode SetHealthBar() pour ajuster la barre de vie avant de mourir
            SetInactiveBasePlayer();
            IsDead(); //Aller � la m�thode IsDead() pour dire que le joueur est mort
        }


        //Partie rajout�e par Oussama Arouch
        //Invisibilit�
        if (manager.isUsingInvisibility || (manager.powerIsUsed && manager.index == 0)) //v�rifie l'input
        {
            if (manager.pInvisibility > 0) //v�rifie l'inventaire
            {
                power.GetComponent<Power>().isActivePower = true; //active le pouvoir
                power.GetComponent<Power>().isInvisible = true; //active le pouvoir cibl�
                manager.pInvisibility--;
                manager.isUsingInvisibility = false;
            }
        }


        //Invincibilit�
        if (manager.isUsingInvincibility || (manager.powerIsUsed && manager.index == 1)) //v�rifie l'input
        {
            if (manager.pInvincibility > 0) //v�rifie l'inventaire
            {
                power.GetComponent<Power>().isActivePower = true; //active le pouvoir
                power.GetComponent<Power>().isInvincible = true; //active le pouvoir cibl�
                manager.pInvincibility--;
                manager.isUsingInvincibility = false;
            }
        }

        //Instant Healing
        if (manager.isUsingInstantHealing || (manager.powerIsUsed && manager.index == 2)) //v�rifie l'input
        {
            if (manager.pInstantHealing > 0) //v�rifie l'inventaire
            {
                power.GetComponent<Power>().isActivePower = true; //active le pouvoir
                power.GetComponent<Power>().isInstantHealing = true; //active le pouvoir cibl�
                manager.pInstantHealing--;
                manager.isUsingInstantHealing = false;
            }
        }

        //Double Damage
        if (manager.isUsingDoubleDamage || (manager.powerIsUsed && manager.index == 4)) //v�rifie l'input
        {
            if (manager.pDoubleDamage > 0) //v�rifie l'inventaire
            {
                power.GetComponent<Power>().isActivePower = true; //active le pouvoir
                power.GetComponent<Power>().isDoubleDamage = true; //active le pouvoir cibl�
                manager.pDoubleDamage--;
                manager.isUsingDoubleDamage = false;
            }
        }


        //Double Score
        if (manager.isUsingDoubleScore || (manager.powerIsUsed && manager.index == 5)) //v�rifie l'input
        {
            if (manager.pDoubleScore > 0) //v�rifie l'inventaire
            {
                power.GetComponent<Power>().isDoubleScore = true; //active le pouvoir cibl�
                manager.pDoubleScore--;
                manager.isUsingDoubleScore = false;
            }
        }



    }
}
