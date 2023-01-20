using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

/// <summary>
/// Script cr�� par Sengsamrach Vong, � part les codes en lien avec Power par Oussama Arouch
/// </summary>

public class Enemy : Characters
{

    public Enemy() { } //Constructeur par d�faut
    public Enemy(float hp, float defense) : base(hp, defense) { } //Constructeur avec les valeurs du script Characters

    private BaseArea baseAreaBenefit; //Script BaseArea
    [SerializeField] private GameObject player = null;
    private GameObject damageOrigin = null;
    private string dmgType = " ";

    [SerializeField] private GameObject baseArea = null; //GameObject pour associer le baseArea au opponent
    [SerializeField] private Slider sliderHealthBar = null; //Silder pour ajuster la valeur de la barre de vie.
    [SerializeField] private Image fillHealthBar = null; //Image pour ajuster le UI de la barre de vie.
    [SerializeField] private Text txtHealthBar = null; //Text pour afficher la valeur de la barre de vie

    private float dmgReceived = 0; //float pour garder la valeur de damage recu.


    //Variables rajout�es par Oussama Arouch
    private GameManager manager;
    [SerializeField] private Renderer myRenderer;
    [SerializeField] private NavMeshAgent agent;
    private float timer = 10f;
    //Variables d'incr�mentation de pouvoirs
    public int eInvisibility = 0;
    public int eInvincibility = 0;
    public int eDoubleDamage = 0;
    public int eDoubleSpeed = 0;
    public int eInstantHealing = 0;
    //Activation des pouvoirs
    public bool activeInvisibility = false;
    public bool activeInvincibility = false;
    public bool activeDoubleDamage = false;
    private bool timerON = false;
    private bool isInvincible = false;
    private float dmgMultiplier = 1f;

    public override void ReceiveDamage()
    {
        if(!isInvincible)
        {

            if (damageOrigin == player)
            {
                if (dmgType == "Melee")
                {
                    dmgReceived = damageOrigin.GetComponent<Equipment>().DamageMelee(damageOrigin.GetComponent<Player>().MyMelee) * damageOrigin.GetComponent<Player>().GetPlayerBaseAreaBenefit().BaseDamageBenefit() * damageOrigin.GetComponent<Player>().SendDoubleDamageOn() - gameObject.GetComponent<Equipment>().DefenseArmor(MyArmor) * baseAreaBenefit.BaseDefenseBenefit();
                }
                else if (dmgType == "Range")
                {
                    dmgReceived = damageOrigin.GetComponent<Equipment>().DamageRange(damageOrigin.GetComponent<Player>().MyRange) * damageOrigin.GetComponent<Player>().GetPlayerBaseAreaBenefit().BaseDamageBenefit() * damageOrigin.GetComponent<Player>().SendDoubleDamageOn() - gameObject.GetComponent<Equipment>().DefenseArmor(MyArmor) * baseAreaBenefit.BaseDefenseBenefit();
                }
            }
            else
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
                }
            }

            this.Hp -= dmgReceived; //Soustrait le Hp par le dmgReceived.
        }
    }
    public void SetInactiveBaseEnemy()
    {
        baseAreaBenefit.LoseBaseArea();
    }

    public bool GetBoolActiveBaseArea()
    {
        return baseAreaBenefit.GetBoolActiveBaseArea();
    }

    public void SetEnemyEquipment()
    {
        gameObject.GetComponent<AI_Behavior>().SetMelee(MyMelee); //Need to call it when changing melee
        gameObject.GetComponent<AI_Behavior>().SetRange(MyRange); //Need to call it when changing range
    }

    public override void IsDead() //M�thode pour si le opponent est mort
    {
        Destroy(gameObject); //D�truire le gameObject de ce script
    }

    public bool ReturnDead() //M�thode pour retourner un bool
    {
        return false; //Retourne false
    }

    private void OnCollisionEnter(Collision collision) //M�thode pour si il y a une collision.
    {
        if (gameObject != null)
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
    }

    // Start is called before the first frame update
    void Start()
    {
        baseAreaBenefit = baseArea.GetComponent<BaseArea>(); //Cache le baseAreaBenefit
        SetEnemyEquipment();
        manager = GameManager.instance; //R�f�rence au GameMnager
        myRenderer = GetComponent<Renderer>();
        agent = GetComponent<NavMeshAgent>();
    }

    public void SetHealthBar() //M�thode pour ajuster la barre de vie
    {
        SetFillHealthBar(); //Aller � la m�thode SetFillHealthBar() pour ajuster la valeur de la barre de vie
        SetColorHealthBar(); //Aller � la m�thode SetColorHealthBar() pour changer la couleur de la barre de vie selon la valeur.
        SetTextHealthBar(); //Aller � la m�thode SetTextHealthBar() pour changer la valeur affich� de la barre de vie.
    }

    public void SetFillHealthBar() //M�thode pour bouger la barre de vie
    {
        sliderHealthBar.value = this.Hp; //Donner la valeur du slider la vie du joueur.
    }

    public void SetColorHealthBar() //M�thode pour changer la couleur de la barre de vie selon la valeur.
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
        if(this.Hp < 0) //Condition If pour si la valeur de Hp est inf�rieure ou �gale � 0.
        {
            txtHealthBar.text = Convert.ToInt32(this.Hp * 0).ToString("D1"); //Afficher 0
        }
    }

    public BaseArea GetEnemyBaseAreaBenefit()
    {
        return baseAreaBenefit;
    }

    public float SendDoubleDamage()
    {
        return dmgMultiplier;
    }

    private void InstantHealing() //fonction rajout�e par Oussama Arouch
    {
        if (this.eInstantHealing > 0) //v�rifie l'inventaire de l'opponent
        {
            this.Hp = this.HpMax; //l'opponent r�cup�re tous ses point de vie
            eInstantHealing--;
        }
    }

    private void DoubleDamage() //fonction rajout�e par Oussama Arouch et modifi�e par Sengsamrach Vong
    {
        if (this.eDoubleDamage > 0) //v�rifie l'inventaire de l'opponent
        {
            timerON = true;
            dmgMultiplier = 2f; //l'opponent douvble son damage
        }
        if(timer <= 0f)
        {
            timerON = false;
            timer = 10f;
            activeDoubleDamage = false;
            dmgMultiplier = 1f; //l'opponent r�cup�re son damage initial
        }
    }

    private void DoubleSpeed() //fonction rajout�e par Oussama Arouch
    {
        if (this.eDoubleSpeed > 0) //v�rifie l'inventaire de l'opponent
        {
            timerON = true;
            agent.speed = 35f; //l'opponent augemnte sa vitesse
            eDoubleSpeed--;
        }
        if (timer <= 0f)
        {
            agent.speed = 25f; //l'opponent r�cup�re sa vitesse initiale
            timer = 10f; //timer r�initialis�
            timerON = false;
        }
    }

    private void Invincibility() //fonction rajout�e par Oussama Arouch
    {

        if (this.eInvincibility > 0) //v�rifie l'inventaire de l'opponent
        {
            isInvincible = true; //l'opponent devient invincible
            timerON = true;
            eInvincibility--;
        }
        if (timer <= 0f)
        {
            timer = 10f; //timer r�initialis�
            isInvincible = false; //l'opponent perd son invincibilit�
            activeInvincibility = false;
            timerON = false;
        }
    }

    private void Invisibility() //fonction rajout�e par Oussama Arouch
    {
        if (this.eInvisibility > 0) //v�rifie l'inventaire de l'opponent
        {
            timerON = true;
            myRenderer.enabled = false; //d�sactive le renderer de l'opponent
            eInvisibility--;
        }
        
        if (timer <= 0f)
        {
            myRenderer.enabled = true; //R�active le renderer de l'opponent
            timer = 10f; //timer r�initialis�
            activeInvisibility = false;
            timerON = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        SetHealthBar(); //Aller � la m�thode SetHealthBar() pour ajuster la barre de vie du joueur.

        if (this.Hp <= 0f) //Condition If pour si la valeur Hp est inf�rieure ou �gale � 0
        {
            SetHealthBar(); //Aller � la m�thode SetHealthBar() pour ajuster la barre de vie avant de mourir
            SetInactiveBaseEnemy();
            IsDead(); //Aller � la m�thode IsDead() pour aller mourir
        }


        //Partie rajout�e par Oussama Arouch
        if (timerON)
        {
            timer -= Time.deltaTime;
        }

        if (activeInvisibility) //Est-ce que le pouvoir a �t� activ� ?
        {
                Invisibility();
        }
            
        if (activeInvincibility) //Est-ce que le pouvoir a �t� activ� ?
        {
                Invincibility();
        }

        if (activeDoubleDamage) //Est-ce que le pouvoir a �t� activ� ?
        {
                DoubleDamage();
        }

        DoubleSpeed();

        InstantHealing();

    }

}
