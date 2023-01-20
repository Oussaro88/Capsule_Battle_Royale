using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script cr�� par Sengsamrach Vong
/// </summary>

public class BaseArea : MonoBehaviour
{
    [SerializeField] private GameObject character = null; //GameObject repr�sentant l'occupant de la base
    [SerializeField] private GameObject player = null; //GameObject repr�sentant le joueur
    [SerializeField] private bool baseActive = true; //Bool pour d�signer si la base est actif ou non
    [SerializeField] private Material defaultMaterial = null;

    [SerializeField] private bool isPresentCharacter = false; //Bool pour d�signer si l'occupant est pr�sent dans la base
    private bool isPresentPlayer = false; //Bool pour d�signer si le joueur est dans la base
    private bool isPresentCapture = false; //Bool pour d�signer si quelqu'un autres que l'occupant et le joueur est dans la base

    [SerializeField] private GameObject panelBaseCapture = null; //GameObject qui d�signe le panel de la capture de base
    [SerializeField] private Image imageFillCaptureProgress = null; //Image qui d�signe la barre de progression de la capture
    [SerializeField] private float floatCaptureProgress = 0; //float qui d�signe la valeur de la capture

    private float timeRegen = 0; //float qui d�signe le temps de r�g�n�ration
    public bool isBeingCaptured = false; //bool qui d�signe si la base est en cours de capture
    [SerializeField] private float baseRegen = 10f; //float qui d�signe la valeur de r�g�n�ration dans la base
    private float baseMultiplierDamage = 1.0f; //float qui d�signe le multiplicateur de base du d�g�t
    [SerializeField] private float multiplierDamage = 1.2f; //float qui d�signe le multiplicateur du d�g�t
    private float baseMultiplierDefense = 1.0f; //float qui d�signe le multiplicateur de base de d�fense
    [SerializeField] private float multiplierDefense = 1.2f; //float qui d�signe le multiplicateur de d�fense

    // Start is called before the first frame update
    void Start()
    {
        baseActive = true; //Initialise � true
        isBeingCaptured = false; //Initialise � false
        panelBaseCapture.SetActive(false); //Cache le panel de la capture de base
        imageFillCaptureProgress.fillAmount = 0; //Initialise le fillAmount � 0
        floatCaptureProgress = 0; //Initialise la valeur � 0
    }

    private void OnTriggerEnter(Collider other) //M�thode pour si un collider entre
    {
        if (other.gameObject.tag == "Character") //Condition If pour voir si le tag du gameObject du collider est "Character"
        {
            if (baseActive) //Condition If pour voir si la base est actif
            {
                if (other.gameObject == character) //Condition If pour voir si le gameObject du collider est l'occupant
                {
                    isPresentCharacter = true; //Bool � true
                }
                if (other.gameObject != character && other.gameObject == player && character != player) //Condition If pour voir si le gameObject du collider est le joueur et que c'est une base autre que celui du joueur
                {
                    panelBaseCapture.SetActive(true); //Panel actif dans la sc�ne
                    isPresentPlayer = true; //Bool � true
                }
                if (other.gameObject != character && other.gameObject != player) //Condition If pour voir si le gameObject du collider est autre que l'occupant et le joueur
                {
                    isPresentCapture = true; //Bool � true
                }
            }
        }
    }

    private void OnTriggerExit(Collider other) //M�thode pour si le collider sort
    {
        if (other.gameObject.tag == "Character") //Condition If pour voir si le tag du gameObject du collider est "Character"
        {
            if (other.gameObject == character) //Condition If pour voir si le gameObject du collider est l'occupant
            {
                isPresentCharacter = false; //Bool � false
            }
            if (other.gameObject != character && other.gameObject == player && character != player) //Condition If pour voir si le gameObject du collider est le joueur et que c'est une base autre que celui du joueur
            {
                panelBaseCapture.SetActive(false); //Panel non actif dans la sc�ne
                isPresentPlayer = false; //Bool � false
            }
            if (other.gameObject != character && other.gameObject != player) //Condition If pour voir si le gameObject du collider est autre que l'occupant et le joueur
            {
                isPresentCapture = false; //Bool � false
            }
        }
    }

    public void BaseRegen() //M�thode pour r�g�n�rer la vie 
    {
        if (character != player) //Condition If pour si l'occupant n'est pas le joueur
        {
            if (character.GetComponent<Enemy>().Hp < character.GetComponent<Enemy>().HpMax) //Condition If pour si le HP de l'occupant est inf�rieur � son HpMax
            {
                character.GetComponent<Enemy>().Hp += baseRegen; //Augmente la vie de l'occupant par le baseRegen

                if (character.GetComponent<Enemy>().Hp > character.GetComponent<Enemy>().HpMax) //Condition If pour si le Hp de l'occupant est sup�rieur � son HpMax
                {
                    character.GetComponent<Enemy>().Hp = character.GetComponent<Enemy>().HpMax; //Mettre son Hp au HpMax.
                }
            }
        }
        else if (character == player) //Condition If pour si l'occupant est celui du joueur
        {
            if (character.GetComponent<Player>().Hp < character.GetComponent<Player>().HpMax) //Condition If pour si le HP de l'occupant est inf�rieur � son HpMax
            {
                character.GetComponent<Player>().Hp += baseRegen; //Augmente la vie de l'occupant par le baseRegen

                if (character.GetComponent<Player>().Hp > character.GetComponent<Player>().HpMax) //Condition If pour si le Hp de l'occupant est sup�rieur � son HpMax
                {
                    character.GetComponent<Player>().Hp = character.GetComponent<Player>().HpMax; //Mettre son Hp au HpMax.
                }
            }
        }
    }

    public float BaseDamageBenefit() //M�thode pour envoyer le multiplicateur de d�g�ts
    {
        if (baseActive) //Condition If pour si la base est actif 
        {
            return multiplierDamage; //Retourne le multiplicateur de d�g�ts
        } 
        else //Si la base est inactif
        {
            return baseMultiplierDamage; //Retourne le multiplicateur de base de d�g�ts
        }
    }

    public float BaseDefenseBenefit() //M�thode pour envoyer le multiplicateur de d�fense
    {
        if (baseActive) //Condition If pour si la base est actif 
        {
            return multiplierDefense; //Retourne le multiplicateur de d�fense
        }
        else //Si la base est inactif
        {
            return baseMultiplierDefense; //Retourne le multiplicateur de base de d�fense
        }
    }

    public void LoseBaseArea()
    {
        baseActive = false;
        gameObject.GetComponentInChildren<MeshRenderer>().material = defaultMaterial;
    }

    public bool GetBoolActiveBaseArea()
    {
        return baseActive;
    }

    // Update is called once per frame
    void Update()
    {
        if (baseActive && isPresentCharacter) //Si la base est actif et l'occupant est dans sa base
        {
            if (character != null) //Si l'occupant n'est pas d�truit
            {
                timeRegen += Time.deltaTime; //Augment le timeRegen par le temps du jeu
                if (timeRegen >= 0.5f) //Si le timeRegen est sup�rieur ou �gal � 0.5 secondes.
                {
                    timeRegen = 0f; //Remet le timeRegen � 0
                    BaseRegen(); //Aller � la m�thode BaseRegen() pour r�g�n�rer sa vie.
                }
            }
        }

        if (baseActive && imageFillCaptureProgress && isPresentPlayer && !isPresentCharacter) //Si la base est actif, il y a un image pour la progression, le joueur est pr�sent et l'occupant est absent
        {
            imageFillCaptureProgress.fillAmount += Time.deltaTime * 0.025f; //Augmente le fillAmount selon le temps multipli� par 0.025f
            isBeingCaptured = true; //Bool de capture � true
        } 
        else //Sinon
        {
            isBeingCaptured = false; //Bool de capture � false
        }

        if (baseActive && isPresentCapture && !isPresentCharacter) //Si la base est actif, quelqu'un d'autre est dans la base et l'oocupant est absent
        {
            floatCaptureProgress += Time.deltaTime * 0.025f; //Augmente le float selon le temps multipli� par 0.025
        }

        if (baseActive && (imageFillCaptureProgress.fillAmount == 1f || floatCaptureProgress > 1f)) //Si la base est actif et que soit l'image ou le float est au-dessus de 1 (rempli)
        {
            baseActive = false; //Bool de la base est false (inactif)
            gameObject.GetComponentInChildren<MeshRenderer>().material = defaultMaterial;
        }
    }
}
