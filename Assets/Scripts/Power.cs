using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Crée par : Oussama Arouch
/// </summary>

public class Power : MonoBehaviour
{

    private GameManager manager;//mon gamemanager

    //référence à mon player ou opponent
    [SerializeField] private CharacterController capman; //mon character controller
    [SerializeField] private NavMeshAgent agentCapman; //mon navmesh agent
    [SerializeField] private Renderer myRenderer; // mon renderer
    [SerializeField] private GameObject playerGO; //mon gameobject(player)

    //Référence aux powers
    public bool isActivePower = false;
    public bool isInvisible = false;
    public bool isInvincible = false;
    public bool isDoubleSpeed = false;
    public bool isDoubleDamage = false;
    public bool isDoubleScore = false;
    public bool isInstantHealing = false;

    private float timer = 10f;

    void Start()
    {
        capman = GetComponent<CharacterController>(); //Cache du character controller
        myRenderer = GetComponent<Renderer>(); //cache du renderer
        agentCapman = GetComponent<NavMeshAgent>(); //cache du navmeshagent
        manager = GameManager.instance; //référence au gamemanager
    }

    void Update()
    {
        //Pourvoir d'invisibilité
        if (isInvisible)
        {
            isActivePower = true;
            if (isActivePower)
            {
                timer -= Time.deltaTime; //countdown
                myRenderer.enabled = false; //renderer désactivé
                if (timer <= 0f)
                {
                    myRenderer.enabled = true; //renderer réactivé
                    isActivePower = false;
                    isInvisible = false;
                    timer = 10f; //timer réinitialisé
                }
            }

        }

        //Pouvoir d'invincibilité
        if (isInvincible)
        {
            isActivePower = true;
            if (isActivePower)
            {
                timer -= Time.deltaTime; //countdown
                if (timer > 0f)
                {
                    playerGO.GetComponent<Player>().IsInvincibilityOn(true); //le player active l'invincibilité
                }
                else
                {
                    playerGO.GetComponent<Player>().IsInvincibilityOn(false); //le player perd l'invincibilité
                    isActivePower = false;
                    isInvincible = false;
                    timer = 10f; //timer réinitialisé
                }
            }
        }

        //Pouvoir DoubleSpeed (opoonent seulement) // pour le player voir le script LocomotionV2
        if (isDoubleSpeed)
        {
            isActivePower = true;
            if (isActivePower)
            {
                timer -= Time.deltaTime; //countdown
                if (timer > 0f)
                {
                    float newSpeed = agentCapman.speed * 1.5f; //vitesse doublée (opponent)
                    agentCapman.speed = newSpeed;
                }
                else
                {
                    agentCapman.speed = agentCapman.speed; //vitesse revenue à la normale (opponent)
                    isActivePower = false;
                    isDoubleSpeed = false;
                    timer = 10f; //timer réinitialisé
                }
            }
        }

        //Pouvoir DoubleDamage //Pouvoir modifié par Sengsamrach Vong
        if (isDoubleDamage)
        {
            isActivePower = true;
            if (isActivePower)
            {
                timer -= Time.deltaTime; //countdown
                if (timer > 0f)
                {
                    playerGO.GetComponent<Player>().SetDoubleDamageOn(2f); //double damage activée pour le player
                }
                else
                {
                    playerGO.GetComponent<Player>().SetDoubleDamageOn(1f); //double damage désactivée pour le player
                    isActivePower = false;
                    isDoubleDamage = false;
                    timer = 10f; //timer réinitialisé
                }
            }
        }

        //Pouvoir Instant Healing
        if (isInstantHealing)
        {
            isActivePower = true;
            if (isActivePower)
            {
                playerGO.GetComponent<Player>().Hp = playerGO.GetComponent<Player>().HpMax; //Le player récupère tous ses points de vie
                isActivePower = false;
                isInstantHealing = false;
            }
        }

    }
}
