using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Cr�e par : Oussama Arouch
/// </summary>

public class Power : MonoBehaviour
{

    private GameManager manager;//mon gamemanager

    //r�f�rence � mon player ou opponent
    [SerializeField] private CharacterController capman; //mon character controller
    [SerializeField] private NavMeshAgent agentCapman; //mon navmesh agent
    [SerializeField] private Renderer myRenderer; // mon renderer
    [SerializeField] private GameObject playerGO; //mon gameobject(player)

    //R�f�rence aux powers
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
        manager = GameManager.instance; //r�f�rence au gamemanager
    }

    void Update()
    {
        //Pourvoir d'invisibilit�
        if (isInvisible)
        {
            isActivePower = true;
            if (isActivePower)
            {
                timer -= Time.deltaTime; //countdown
                myRenderer.enabled = false; //renderer d�sactiv�
                if (timer <= 0f)
                {
                    myRenderer.enabled = true; //renderer r�activ�
                    isActivePower = false;
                    isInvisible = false;
                    timer = 10f; //timer r�initialis�
                }
            }

        }

        //Pouvoir d'invincibilit�
        if (isInvincible)
        {
            isActivePower = true;
            if (isActivePower)
            {
                timer -= Time.deltaTime; //countdown
                if (timer > 0f)
                {
                    playerGO.GetComponent<Player>().IsInvincibilityOn(true); //le player active l'invincibilit�
                }
                else
                {
                    playerGO.GetComponent<Player>().IsInvincibilityOn(false); //le player perd l'invincibilit�
                    isActivePower = false;
                    isInvincible = false;
                    timer = 10f; //timer r�initialis�
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
                    float newSpeed = agentCapman.speed * 1.5f; //vitesse doubl�e (opponent)
                    agentCapman.speed = newSpeed;
                }
                else
                {
                    agentCapman.speed = agentCapman.speed; //vitesse revenue � la normale (opponent)
                    isActivePower = false;
                    isDoubleSpeed = false;
                    timer = 10f; //timer r�initialis�
                }
            }
        }

        //Pouvoir DoubleDamage //Pouvoir modifi� par Sengsamrach Vong
        if (isDoubleDamage)
        {
            isActivePower = true;
            if (isActivePower)
            {
                timer -= Time.deltaTime; //countdown
                if (timer > 0f)
                {
                    playerGO.GetComponent<Player>().SetDoubleDamageOn(2f); //double damage activ�e pour le player
                }
                else
                {
                    playerGO.GetComponent<Player>().SetDoubleDamageOn(1f); //double damage d�sactiv�e pour le player
                    isActivePower = false;
                    isDoubleDamage = false;
                    timer = 10f; //timer r�initialis�
                }
            }
        }

        //Pouvoir Instant Healing
        if (isInstantHealing)
        {
            isActivePower = true;
            if (isActivePower)
            {
                playerGO.GetComponent<Player>().Hp = playerGO.GetComponent<Player>().HpMax; //Le player r�cup�re tous ses points de vie
                isActivePower = false;
                isInstantHealing = false;
            }
        }

    }
}
