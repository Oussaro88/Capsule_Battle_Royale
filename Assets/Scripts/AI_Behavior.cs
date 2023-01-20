using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Crée par : Oussama Arouch
/// </summary>

public class AI_Behavior : MonoBehaviour
{
    //Faire référence au gaemobject portant le script power
    [SerializeField] private GameObject powerGO; //mon gameobject
    private Power power; //référence au script power

    private Enemy enemy;
    private Enemy enemyGO;
    [SerializeField] private Transform startPointPosition; //Référence à la base du joueur

    [SerializeField] private NavMeshAgent agent; //Mon NavMeshAgent
    [SerializeField] public List<GameObject> targetList = null; //Liste des cibles
    private float timer = 0f; //timer
    private float timer2 = 10f; //timer pour le countdown
    private bool timer2OFF = false; //le timer est-il actif??

    //Patrouille de l'IA
    [SerializeField] private Transform[] patrolPoints; //Liste de points de patrouille
    private int pointIndex; //index

    //Attaque de l'IA

    private IState state; //Référence vers l'interface Istate


    //Variables rajoutés par Sengsamrach Vong
    private BaseArea baseArea;
    [SerializeField] private GameObject playerBase;

    private GameObject rangedWeapon = null;
    [SerializeField] private Equipment.typeMelee myMelee;
    [SerializeField] private Equipment.typeRange myRange;
    [SerializeField] private GameObject projectileRock = null;
    [SerializeField] private GameObject projectileSlinger = null;
    [SerializeField] private GameObject projectileBow = null;
    [SerializeField] private GameObject projectileGun = null;
    [SerializeField] private GameObject knifeObject;
    [SerializeField] private GameObject swordObject;
    [SerializeField] private GameObject spearObject;
    [SerializeField] private GameObject hammerObject;
    [SerializeField] private Animator knifeAnim;
    [SerializeField] private Animator swordAnim;
    [SerializeField] private Animator spearAnim;
    [SerializeField] private Animator hammerAnim;
    private bool timerMeleeOn = false;
    private float timerMelee = 0;

    void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); //Cache du navmesh
        pointIndex = Random.Range(0, patrolPoints.Length); //retourne un index alátoire de la liste des points de patrouille
        state = NormalState.GetState(); //référence au state pattern
        enemy = GetComponent<Enemy>();
        enemyGO = GetComponent<Enemy>();
        baseArea = playerBase.GetComponent<BaseArea>(); //Cache de cu component BaseArea

        knifeObject.GetComponent<Weapon>().SetOrigin(gameObject);
        swordObject.GetComponent<Weapon>().SetOrigin(gameObject);
        spearObject.GetComponent<Weapon>().SetOrigin(gameObject);
        hammerObject.GetComponent<Weapon>().SetOrigin(gameObject);

        power = powerGO.GetComponent<Power>(); //Cache du gameObject portant le script power
    }

    // Update is called once per frame
    void Update()
    {
        //patrouille

        if (!this.state.CanAttackEnemy() || !this.state.GoToBase() || !this.state.DefendBase()) //Si lÍA est dans son Normal State
        {
            Invoke("OnPatrolling", 3f); //Invoque la méthode qui permet au navMesh de faire sa patrouille
        }
        else
        {
            CancelInvoke("OnPatrolling"); //Annule l'état de patrouille
        }

        //Invisibility Power for player
        if (power.isInvisible && power.isActivePower && !timer2OFF)
        {
            timer2 -= Time.deltaTime;
            if (powerGO.name == "CapMan")
            {
                targetList.Remove(powerGO);
            }
            timer2OFF = true;
        }

        if (timer2OFF && !power.isInvisible)
        {
            timer2OFF = false;
            if (powerGO.name == "CapMan")
            {
                targetList.Add(powerGO);
            }
        }

        foreach (GameObject target in targetList)
        {
            if (target != null)

            {
                Vector3 distanceFromEnemy = agent.transform.position - target.transform.position; //distance entre le joueur et l'IA
                if (distanceFromEnemy.magnitude < 70)
                {
                    this.state = AttackState.GetState(); //Fait appel au pattern Attack State
                    if (this.state.CanAttackEnemy()) //vérfie si l'agent peut attaquer
                    {
                        transform.LookAt(target.transform.position); //le forward de l'agent fait face à sa cible
                        agent.destination = target.transform.position; //l'agent s'arrete à sa position actuelle

                        if (distanceFromEnemy.magnitude < 40)
                        {
                            agent.destination = agent.transform.position; // l'IA s'arrete à sa position actuelle

                            transform.LookAt(target.transform.position); //L'IA s''oriente vers sa cible

                            timer += Time.deltaTime; //active le chrono
                            
                            //Activation d'un power si disponible dans l'inventaire
                            if(enemyGO.GetComponent<Enemy>().eInvisibility > 0)
                            {
                                enemyGO.GetComponent<Enemy>().activeInvisibility = true; //Pouvoir d'invisibilité
                            }
                            if (enemyGO.GetComponent<Enemy>().eInvincibility > 0)
                            {
                                enemyGO.GetComponent<Enemy>().activeInvincibility = true; //Pouvoir d'invincibilité
                            }
                            if (enemyGO.GetComponent<Enemy>().eDoubleDamage> 0)
                            {
                                enemyGO.GetComponent<Enemy>().activeDoubleDamage = true; //Pouvoir Double Damage
                            }

                            /////Partie rajoutée par SengSamrach Vong////
                            if (distanceFromEnemy.magnitude < 20)
                            {
                                StrikeMeleeWeapon();
                            }
                            else if (timer > 1f)
                            {
                                SetRangedWeapon();//Testing Purpose
                                ShootRangedWeapon();

                                timer = 0f;
                            }
                            //////////
                        }
                    }
                }

                if (enemy.Hp <= (enemy.HpMax / 2))
                {
                    if (enemy.GetBoolActiveBaseArea()) //Ligne rajoutée par SengSamrach Vong
                    {
                        this.state = RunState.GetState(); // L'IA passe en mode fuite

                        CancelInvoke("OnPatrolling"); //Annule l'état de patrouille
                        agent.destination = startPointPosition.position; //L'IA se dirige vers sa base
                        if (enemy.Hp >= (enemy.HpMax * 0.75)) // si le joueur récupère 75% de ses points HP, il retourne à l'arène
                        {
                            this.state = NormalState.GetState();
                        }
                    }
                }

                if (baseArea.isBeingCaptured) //Si la base de l'IA est attaquée   //Ligne rajoutée par SengSamrach Vong
                {
                    this.state = DefendState.GetState(); //passage au mode défense
                    CancelInvoke("OnPatrolling"); //Annule l'état de patrouille
                    Debug.Log(state);
                    if (this.state.DefendBase()) //un ennemi est dans la base de l'IA
                    {
                        agent.destination = startPointPosition.position; //L'IA se dirige vers sa base
                        Vector3 distanceFromStartPoint = agent.transform.position - startPointPosition.position;
                        if (distanceFromStartPoint.magnitude < 30) 
                        {
                            this.state = AttackState.GetState(); //si l'IA  est en mode et qu'un adversaire est à proximité, l'IA passe en mode attaque
                        }
                    }
                }
                else if (!baseArea.isBeingCaptured) //Boucle rajoutée par SengSamrach Vong
                {
                    this.state = NormalState.GetState(); //fait appel au pattern Normal State
                }


                else
                {
                    this.state = NormalState.GetState(); //fait appel au pattern Normal State
                }
            }
        }

        if (timerMeleeOn)
        {
            timerMelee -= Time.deltaTime;
            if (timerMelee <= 0f)
            {
                timerMelee = 0f;
                timerMeleeOn = false;
            }
        }
    }

    private void OnPatrolling()
    {
        if (pointIndex < patrolPoints.Length) //vérfie la position de l'index par rapport à la taille de la liste des points de patrouille
        {
            agent.destination = Vector3.MoveTowards(transform.position, patrolPoints[pointIndex].position, 500f * Time.deltaTime); //Dirige l'agent vers le point indéxé de la liste de patrouille

            if (Vector3.Distance(agent.transform.position, patrolPoints[pointIndex].position) < 10f) //vérifie la distance en tre le joueur et sa destination
            {
                pointIndex++; //incrémente l'index pour permettre au joueur d'aller à un nouveau point de patrouille
                agent.SetDestination(patrolPoints[pointIndex].position);
            }
            if (pointIndex >= patrolPoints.Length - 1)
            {
                pointIndex = pointIndex = Random.Range(0, patrolPoints.Length); //réintialise le tableau une fois la taille final tdu tableau atteinte
            }
        }
    }

    //Tout le reste a été rajouté par SengSamrach Vong

    public void SetMelee(Equipment.typeMelee enemyMelee) //Fonction rajoutée par SengSamrach Vong
    {
        myMelee = enemyMelee;
    }

    public void SetRange(Equipment.typeRange enemyRange) //Fonction rajoutée par SengSamrach Vong
    {
        myRange = enemyRange;
        SetRangedWeapon();
    }

    public void SetRangedWeapon() //Fonction rajoutée par SengSamrach Vong
    {
        if (myRange == Equipment.typeRange.Rock)
        {
            rangedWeapon = projectileRock;
        }
        else if (myRange == Equipment.typeRange.Slinger)
        {
            rangedWeapon = projectileSlinger;
        }
        else if (myRange == Equipment.typeRange.Bow)
        {
            rangedWeapon = projectileBow;
        }
        else if (myRange == Equipment.typeRange.Gun)
        {
            rangedWeapon = projectileGun;
        }
    }

    public void ShootRangedWeapon() //Fonction rajoutée par SengSamrach Vong
    {
        if (rangedWeapon == projectileRock)
        {
            GameObject rangedAttack = Instantiate(rangedWeapon, transform.position + (transform.forward * 3f), transform.rotation);
            rangedAttack.GetComponent<Weapon>().SetOrigin(gameObject);
            rangedAttack.GetComponent<Rigidbody>().AddForce(transform.forward * 80, ForceMode.Impulse);
        }
        else if (rangedWeapon == projectileSlinger)
        {
            GameObject rangedAttack = Instantiate(rangedWeapon, transform.position + (transform.forward * 3f), transform.rotation);
            rangedAttack.GetComponent<Weapon>().SetOrigin(gameObject);
            rangedAttack.GetComponent<Rigidbody>().AddForce(transform.forward * 100, ForceMode.Impulse);
        }
        else if (rangedWeapon == projectileBow)
        {
            GameObject rangedAttack = Instantiate(rangedWeapon, transform.position + (transform.forward * 5f), transform.rotation);
            rangedAttack.GetComponent<Weapon>().SetOrigin(gameObject);
            rangedAttack.GetComponent<Rigidbody>().AddForce(transform.forward * 125, ForceMode.Impulse);
        }
        else if (rangedWeapon == projectileGun)
        {
            GameObject rangedAttack = Instantiate(rangedWeapon, transform.position + (transform.forward * 3f), transform.rotation);
            rangedAttack.GetComponent<Weapon>().SetOrigin(gameObject);
            rangedAttack.GetComponent<Rigidbody>().AddForce(transform.forward * 150, ForceMode.Impulse);
        }

    }

    public void StrikeMeleeWeapon() //Fonction rajoutée par SengSamrach Vong
    {
        if (!timerMeleeOn)
        {
            timerMeleeOn = true;

            if (myMelee == Equipment.typeMelee.Knife)
            {
                timerMelee = 0.5f;
                knifeObject.SetActive(true);
                if (knifeAnim) knifeAnim.SetTrigger("Attack");
                Invoke("SetInactiveKnife", timerMelee);
            }
            else if (myMelee == Equipment.typeMelee.Sword)
            {
                timerMelee = 0.5f;
                swordObject.SetActive(true);
                if (swordAnim) swordAnim.SetTrigger("Attack");
                Invoke("SetInactiveSword", timerMelee);
            }
            else if (myMelee == Equipment.typeMelee.Spear)
            {
                timerMelee = 0.75f;
                spearObject.SetActive(true);
                if (spearAnim) spearAnim.SetTrigger("Attack");
                Invoke("SetInactiveSpear", timerMelee);
            }
            else if (myMelee == Equipment.typeMelee.Hammer)
            {
                timerMelee = 1f;
                hammerObject.SetActive(true);
                if (hammerAnim) hammerAnim.SetTrigger("Attack");
                Invoke("SetInactiveHammer", timerMelee);
            }
        }
    }

    public void SetInactiveKnife() //Fonction rajoutée par SengSamrach Vong
    {
        knifeObject.SetActive(false);
    }

    public void SetInactiveSword() //Fonction rajoutée par SengSamrach Vong
    {
        swordObject.SetActive(false);
    }

    public void SetInactiveSpear() //Fonction rajoutée par SengSamrach Vong
    {
        spearObject.SetActive(false);
    }

    public void SetInactiveHammer() //Fonction rajoutée par SengSamrach Vong
    {
        hammerObject.SetActive(false);
    }

}
