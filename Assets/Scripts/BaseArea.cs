using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script créé par Sengsamrach Vong
/// </summary>

public class BaseArea : MonoBehaviour
{
    [SerializeField] private GameObject character = null; //GameObject représentant l'occupant de la base
    [SerializeField] private GameObject player = null; //GameObject représentant le joueur
    [SerializeField] private bool baseActive = true; //Bool pour désigner si la base est actif ou non
    [SerializeField] private Material defaultMaterial = null;

    [SerializeField] private bool isPresentCharacter = false; //Bool pour désigner si l'occupant est présent dans la base
    private bool isPresentPlayer = false; //Bool pour désigner si le joueur est dans la base
    private bool isPresentCapture = false; //Bool pour désigner si quelqu'un autres que l'occupant et le joueur est dans la base

    [SerializeField] private GameObject panelBaseCapture = null; //GameObject qui désigne le panel de la capture de base
    [SerializeField] private Image imageFillCaptureProgress = null; //Image qui désigne la barre de progression de la capture
    [SerializeField] private float floatCaptureProgress = 0; //float qui désigne la valeur de la capture

    private float timeRegen = 0; //float qui désigne le temps de régénération
    public bool isBeingCaptured = false; //bool qui désigne si la base est en cours de capture
    [SerializeField] private float baseRegen = 10f; //float qui désigne la valeur de régénération dans la base
    private float baseMultiplierDamage = 1.0f; //float qui désigne le multiplicateur de base du dégât
    [SerializeField] private float multiplierDamage = 1.2f; //float qui désigne le multiplicateur du dégât
    private float baseMultiplierDefense = 1.0f; //float qui désigne le multiplicateur de base de défense
    [SerializeField] private float multiplierDefense = 1.2f; //float qui désigne le multiplicateur de défense

    // Start is called before the first frame update
    void Start()
    {
        baseActive = true; //Initialise à true
        isBeingCaptured = false; //Initialise à false
        panelBaseCapture.SetActive(false); //Cache le panel de la capture de base
        imageFillCaptureProgress.fillAmount = 0; //Initialise le fillAmount à 0
        floatCaptureProgress = 0; //Initialise la valeur à 0
    }

    private void OnTriggerEnter(Collider other) //Méthode pour si un collider entre
    {
        if (other.gameObject.tag == "Character") //Condition If pour voir si le tag du gameObject du collider est "Character"
        {
            if (baseActive) //Condition If pour voir si la base est actif
            {
                if (other.gameObject == character) //Condition If pour voir si le gameObject du collider est l'occupant
                {
                    isPresentCharacter = true; //Bool à true
                }
                if (other.gameObject != character && other.gameObject == player && character != player) //Condition If pour voir si le gameObject du collider est le joueur et que c'est une base autre que celui du joueur
                {
                    panelBaseCapture.SetActive(true); //Panel actif dans la scène
                    isPresentPlayer = true; //Bool à true
                }
                if (other.gameObject != character && other.gameObject != player) //Condition If pour voir si le gameObject du collider est autre que l'occupant et le joueur
                {
                    isPresentCapture = true; //Bool à true
                }
            }
        }
    }

    private void OnTriggerExit(Collider other) //Méthode pour si le collider sort
    {
        if (other.gameObject.tag == "Character") //Condition If pour voir si le tag du gameObject du collider est "Character"
        {
            if (other.gameObject == character) //Condition If pour voir si le gameObject du collider est l'occupant
            {
                isPresentCharacter = false; //Bool à false
            }
            if (other.gameObject != character && other.gameObject == player && character != player) //Condition If pour voir si le gameObject du collider est le joueur et que c'est une base autre que celui du joueur
            {
                panelBaseCapture.SetActive(false); //Panel non actif dans la scène
                isPresentPlayer = false; //Bool à false
            }
            if (other.gameObject != character && other.gameObject != player) //Condition If pour voir si le gameObject du collider est autre que l'occupant et le joueur
            {
                isPresentCapture = false; //Bool à false
            }
        }
    }

    public void BaseRegen() //Méthode pour régénérer la vie 
    {
        if (character != player) //Condition If pour si l'occupant n'est pas le joueur
        {
            if (character.GetComponent<Enemy>().Hp < character.GetComponent<Enemy>().HpMax) //Condition If pour si le HP de l'occupant est inférieur à son HpMax
            {
                character.GetComponent<Enemy>().Hp += baseRegen; //Augmente la vie de l'occupant par le baseRegen

                if (character.GetComponent<Enemy>().Hp > character.GetComponent<Enemy>().HpMax) //Condition If pour si le Hp de l'occupant est supérieur à son HpMax
                {
                    character.GetComponent<Enemy>().Hp = character.GetComponent<Enemy>().HpMax; //Mettre son Hp au HpMax.
                }
            }
        }
        else if (character == player) //Condition If pour si l'occupant est celui du joueur
        {
            if (character.GetComponent<Player>().Hp < character.GetComponent<Player>().HpMax) //Condition If pour si le HP de l'occupant est inférieur à son HpMax
            {
                character.GetComponent<Player>().Hp += baseRegen; //Augmente la vie de l'occupant par le baseRegen

                if (character.GetComponent<Player>().Hp > character.GetComponent<Player>().HpMax) //Condition If pour si le Hp de l'occupant est supérieur à son HpMax
                {
                    character.GetComponent<Player>().Hp = character.GetComponent<Player>().HpMax; //Mettre son Hp au HpMax.
                }
            }
        }
    }

    public float BaseDamageBenefit() //Méthode pour envoyer le multiplicateur de dégâts
    {
        if (baseActive) //Condition If pour si la base est actif 
        {
            return multiplierDamage; //Retourne le multiplicateur de dégâts
        } 
        else //Si la base est inactif
        {
            return baseMultiplierDamage; //Retourne le multiplicateur de base de dégâts
        }
    }

    public float BaseDefenseBenefit() //Méthode pour envoyer le multiplicateur de défense
    {
        if (baseActive) //Condition If pour si la base est actif 
        {
            return multiplierDefense; //Retourne le multiplicateur de défense
        }
        else //Si la base est inactif
        {
            return baseMultiplierDefense; //Retourne le multiplicateur de base de défense
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
            if (character != null) //Si l'occupant n'est pas détruit
            {
                timeRegen += Time.deltaTime; //Augment le timeRegen par le temps du jeu
                if (timeRegen >= 0.5f) //Si le timeRegen est supérieur ou égal à 0.5 secondes.
                {
                    timeRegen = 0f; //Remet le timeRegen à 0
                    BaseRegen(); //Aller à la méthode BaseRegen() pour régénérer sa vie.
                }
            }
        }

        if (baseActive && imageFillCaptureProgress && isPresentPlayer && !isPresentCharacter) //Si la base est actif, il y a un image pour la progression, le joueur est présent et l'occupant est absent
        {
            imageFillCaptureProgress.fillAmount += Time.deltaTime * 0.025f; //Augmente le fillAmount selon le temps multiplié par 0.025f
            isBeingCaptured = true; //Bool de capture à true
        } 
        else //Sinon
        {
            isBeingCaptured = false; //Bool de capture à false
        }

        if (baseActive && isPresentCapture && !isPresentCharacter) //Si la base est actif, quelqu'un d'autre est dans la base et l'oocupant est absent
        {
            floatCaptureProgress += Time.deltaTime * 0.025f; //Augmente le float selon le temps multiplié par 0.025
        }

        if (baseActive && (imageFillCaptureProgress.fillAmount == 1f || floatCaptureProgress > 1f)) //Si la base est actif et que soit l'image ou le float est au-dessus de 1 (rempli)
        {
            baseActive = false; //Bool de la base est false (inactif)
            gameObject.GetComponentInChildren<MeshRenderer>().material = defaultMaterial;
        }
    }
}
