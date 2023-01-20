using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script créé par Sengsamrach Vong, à part un code de Oussama Arouch
/// </summary>

public class EquipDeploymentCard : MonoBehaviour
{
    //Melee : Knife = 30, Sword = 40, Spear = 40, Hammer = 40
    //Range : Rock = 30, Slinger = 35, Bow = 40, Gun = 50
    //Armor : Cloth = 15, Light = 20, Medium = 25, Heavy = 30

    private GameManager manager;

    [SerializeField] private GameObject player = null;

    [SerializeField] private GameObject panelDeployment = null;
    [SerializeField] private Text textDeployment = null;

    [SerializeField] private int numEquip = 0;
    [SerializeField] private int numWave = 0;
    private float timerEquip = 0f;
    private bool isTriggered = false;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameManager.instance; //Singleton du GameManager
    }

    public void PrepareEquipmentCard()
    {
        numEquip = 0;
        switch (numWave)
        {
            case 0:
                numEquip = UnityEngine.Random.Range(0, 2);
                break;
            case 1:
                numEquip = UnityEngine.Random.Range(2, 7);
                break;
            case 2:
                numEquip = UnityEngine.Random.Range(4, 9);
                break;
            default:
                numEquip = UnityEngine.Random.Range(0, 9);
                break;
        }
        numWave++;
    }

    public void DisplayEquipmentCard()
    {
        panelDeployment.SetActive(true);
        switch (numEquip)
        {
            case 0:
                textDeployment.text = "Slinger Range Card";
                break;
            case 1:
                textDeployment.text = "Light Armor Card";
                break;
            case 2:
                textDeployment.text = "Bow Range Card";
                break;
            case 3:
                textDeployment.text = "Medium Armor Card";
                break;
            case 4:
                textDeployment.text = "Sword Melee Card";
                break;
            case 5:
                textDeployment.text = "Spear Melee Card";
                break;
            case 6:
                textDeployment.text = "Hammer Melee Card";
                break;
            case 7:
                textDeployment.text = "Gun Range Card";
                break;
            case 8:
                textDeployment.text = "Heavy Armor Card";
                break;
            default:
                textDeployment.text = "Empty Card";
                break;
        }
    }

    public void AttachEquipmentPlayer()
    {
        switch (numEquip)
        {
            case 0:
                player.GetComponent<Player>().MyRange = Equipment.typeRange.Slinger;
                break;
            case 1:
                player.GetComponent<Player>().MyArmor = Equipment.typeArmor.Light;
                break;
            case 2:
                player.GetComponent<Player>().MyRange = Equipment.typeRange.Bow;
                break;
            case 3:
                player.GetComponent<Player>().MyArmor = Equipment.typeArmor.Medium;
                break;
            case 4:
                player.GetComponent<Player>().MyMelee = Equipment.typeMelee.Sword;
                break;
            case 5:
                player.GetComponent<Player>().MyMelee = Equipment.typeMelee.Spear;
                break;
            case 6:
                player.GetComponent<Player>().MyMelee = Equipment.typeMelee.Hammer;
                break;
            case 7:
                player.GetComponent<Player>().MyRange = Equipment.typeRange.Gun;
                break;
            case 8:
                player.GetComponent<Player>().MyArmor = Equipment.typeArmor.Heavy;
                break;
            default:
                break;
        }
        player.GetComponent<Player>().SetImgEquipment();
        player.GetComponent<Player>().SetPlayerEquipment();
        SetInactiveEquipmentCard();
    }
    public void AttachEquipmentEnemy(Collider other)
    {
        switch (numEquip)
        {
            case 0:
                other.gameObject.GetComponent<Enemy>().MyRange = Equipment.typeRange.Slinger;
                break;
            case 1:
                other.gameObject.GetComponent<Enemy>().MyArmor = Equipment.typeArmor.Light;
                break;
            case 2:
                other.gameObject.GetComponent<Enemy>().MyRange = Equipment.typeRange.Bow;
                break;
            case 3:
                other.gameObject.GetComponent<Enemy>().MyArmor = Equipment.typeArmor.Medium;
                break;
            case 4:
                other.gameObject.GetComponent<Enemy>().MyMelee = Equipment.typeMelee.Sword;
                break;
            case 5:
                other.gameObject.GetComponent<Enemy>().MyMelee = Equipment.typeMelee.Spear;
                break;
            case 6:
                other.gameObject.GetComponent<Enemy>().MyMelee = Equipment.typeMelee.Hammer;
                break;
            case 7:
                other.gameObject.GetComponent<Enemy>().MyRange = Equipment.typeRange.Gun;
                break;
            case 8:
                other.gameObject.GetComponent<Enemy>().MyArmor = Equipment.typeArmor.Heavy;
                break;
            default:
                break;
        }
        other.gameObject.GetComponent<Enemy>().SetEnemyEquipment();
        SetInactiveEquipmentCard();
    }

    public void SetInactiveEquipmentCard()
    {
        panelDeployment.SetActive(false);
        gameObject.SetActive(false);
        isTriggered = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Character")
        {
            if (other.gameObject == player)
            {
                DisplayEquipmentCard();
                isTriggered = true;
            } 
            else
            {
                timerEquip += Time.deltaTime;
                if (timerEquip >= 2f)
                {
                    AttachEquipmentEnemy(other);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Character" && other.gameObject == player)
        {
            if (other.gameObject == player)
            {
                isTriggered = false;
                panelDeployment.SetActive(false);
            }
            else
            {
                timerEquip = 0;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.SendGetEquipment() && isTriggered) //Ligne modifiée par Oussama Arouch
        {
            AttachEquipmentPlayer();
        }
    }
}
