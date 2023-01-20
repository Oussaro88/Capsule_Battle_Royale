using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Cr�e par : Oussama Arouch
/// </summary>


public class GameManager : MonoBehaviour
{
    //Inventaire pour les powers
    public int pInvisibility = 0;
    public int pInvincibility = 0;
    public int pDoubleDamage = 0;
    public int pDoubleSpeed = 0;
    public int pDoubleScore = 0;
    public int pInstantHealing = 0;

    //UI des powers
    [SerializeField] private GameObject[] powerIndicators; //tableau des powers
    private bool powerChangedRight = false; //est-ceque j'ai chang� de power ?
    private bool powerChangedLeft = false; //est-ce que j'ai chang� de power ?
    public bool powerIsUsed = false; //est-ce que j'ai utilis� mon power ?
    private bool getEquipement = false; //est-ce que j'ai r�cup�r� un �quipement ?
    public int index = 0;

    //Icone Pour inventaire
    [SerializeField] private GameObject okIcon1 = null;
    [SerializeField] private GameObject okIcon2 = null;
    [SerializeField] private GameObject okIcon3 = null;
    [SerializeField] private GameObject okIcon4 = null;
    [SerializeField] private GameObject okIcon5 = null;
    [SerializeField] private GameObject okIcon6 = null;
    [SerializeField] private GameObject nokIcon1 = null;
    [SerializeField] private GameObject nokIcon2 = null;
    [SerializeField] private GameObject nokIcon3 = null;
    [SerializeField] private GameObject nokIcon4 = null;
    [SerializeField] private GameObject nokIcon5 = null;
    [SerializeField] private GameObject nokIcon6 = null;

    //Bool�ene pour les pouvoirs(Input system)
    public bool isUsingInvisibility = false;
    public bool isUsingInvincibility = false;
    public bool isUsingDoubleDamage = false;
    public bool isUsingDoubleSpeed = false;
    public bool isUsingDoubleScore = false;
    public bool isUsingInstantHealing = false;

    private Power power; //R�f�rence aus Scipt Power
    [SerializeField] private GameObject powerGO; //R�f�rence au gameobject portant le Scipt Power
    public static GameManager instance = null; //mon Gamemanager

    [SerializeField] private GameObject[] pointStart = null; //point de d�part du joueur
    [SerializeField] private GameObject[] characters = new GameObject[8]; //joueurs + IA

    [SerializeField] private CharacterController player; //mon character controller
    [SerializeField] private Characters character = null; //mon joueur

    //panelpause
    [SerializeField] private GameObject pnlPauseMenu; //Menu pause
    [SerializeField] private GameObject pnlGameVictory; //mon �cran de victoire
    [SerializeField] private GameObject pnlGameOver; // mon �cran de gameOver

    public static bool isPaused = false; //est-ce que je suis en pause
    private int opponentGone = 0; //int pour d�signer le nombre de opponent d�truit

    private float timer = 0f; //float qui d�signe le timer

    //Score
    private float score;
    public float finalScore; //mon score final bas� sur le temps pass� dans le jeu
    private float hpScore; //mon hpscore
    private float multiplier = 1.25f; // bonus de score
    [SerializeField] private Text timeScoreTxtGO; //Mon time score pour l'�cran Game Over
    [SerializeField] private Text hpScoreTxtGO; //Mon Hp score pour l'�cran Game Over
    [SerializeField] private Text totalScoreTxtGO; //Mon total score pour l'�cran Game Over
    [SerializeField] private Text timeScoreTxtGV; //Mon time score pour l'�cran Game Victory
    [SerializeField] private Text hpScoreTxtGV; //Mon Hp score pour l'�cran Game Victory
    [SerializeField] private Text totalScoreTxtGV; //Mon total score pour l'�cran Game Victory
    private bool isGameOver = false; //ai-je perdu ?
    private bool hasWon = false; //ai-je gagn� ?

    //Chrono
    [SerializeField] private Text chrono; //Mon chrono text
    private float chronoTime = 0f;
    private float timeElapsed; //variable pour calculer le temps pass� dans le jeu

    //Variables rajout�es par Sengsamrach Vong

    [SerializeField] private GameObject[] mapPoints = new GameObject[8]; //GameObject qui repr�sente la position des personnages dans le mini-map
    [SerializeField] private GameObject[] baseIndicators = new GameObject[8];
    [SerializeField] private Material[] materialsColour = new Material[8]; //Couleur des personnages
    [SerializeField] private Text[] charactersID = new Text[8]; //Text des noms des personnages Besoin de couleur et non material
    [SerializeField] private GameObject[] arrayEquipmentCard = new GameObject[16];

    private int equipCardCount = 0;
    private int equipCardRandom = 0;
    private bool onceTimer1 = false;
    private bool onceTimer2 = false;
    private bool onceTimer3 = false;
    private bool onceTimer4 = false;
    private float timerPenaltyHealth = 0f;

    void Awake()
    {
        if (instance == null) //si aucune isntance
        {
            instance = this; // cr�er une nouvelle instance
        }
        else if (instance != null) //si j'ai d�ja une instance
        {
            Destroy(this); //d�truire l'instance actuelle
        }
        pnlPauseMenu.SetActive(false); //activation du panel pause
    }


    // Start is called before the first frame update
    void Start()
    {
        power = GetComponent<Power>(); //Cache du component Power
        powerIndicators[0].SetActive(true);
        SetColour(); //Aller � la m�thode SetColour pour mettre le couleur au Player et Opponent
        DeployEquipmentCard();
        BlockLocationShown();
    }
    public void OnInvisbility(InputAction.CallbackContext context)
    {
        isUsingInvisibility = context.performed; //Pouvoir utilis�
    }

    public void OnInvincibility(InputAction.CallbackContext context)
    {
        isUsingInvincibility = context.performed; //Pouvoir utilis�
    }

    public void OnDoubleDamage(InputAction.CallbackContext context)
    {
        isUsingDoubleDamage = context.performed; //Pouvoir utilis�
    }

    public void OnDoubleSpeed(InputAction.CallbackContext context)
    {
        isUsingDoubleSpeed = context.performed; //Pouvoir utilis�
    }

    public void OnDoubleScore(InputAction.CallbackContext context)
    {
        isUsingDoubleScore = context.performed; //Pouvoir utilis�
    }

    public void OnInstantHealing(InputAction.CallbackContext context)
    {
        isUsingInstantHealing = context.performed; //Pouvoir utilis�
    }

    public void OnPowerChangeRight(InputAction.CallbackContext context)
    {
        powerChangedRight = context.performed; //Pouvoir chang�
    }
    public void OnPowerChangeLeft(InputAction.CallbackContext context)
    {
        powerChangedLeft = context.performed; //Pouvoir chang�
    }

    public void OnPowerUse(InputAction.CallbackContext context)
    {
        powerIsUsed = context.performed; //Pouvoir utilis�
    }

    public void OnGetEquipment(InputAction.CallbackContext context)
    {
        getEquipement = context.performed; //Equipement r�cup�r�
    }

    public bool SendGetEquipment() //Equipement r�cup�r� //m�thode appel� dans le EquiDeployment Card
    {
        return getEquipement;
    }

    //Fonction rajout�e par Sengsamrach Vong
    public void SetColour() //M�thode pour mettre la couleur
    {
        characters[0].GetComponent<MeshRenderer>().material = materialsColour[PlayerPrefs.GetInt("Player", 0)]; //Donner le material du joueur au couleur dans PlayerPrefs de Player
        mapPoints[0].GetComponent<MeshRenderer>().material = materialsColour[PlayerPrefs.GetInt("Player", 0)]; //Donner le material de l'ic�ne Mini-Map du joueur au couleur dans PlayerPrefs de Player
        charactersID[0].color = materialsColour[PlayerPrefs.GetInt("Player", 0)].color; //Donner la couleur du Text du joueur au couleur dans PlayerPrefs de Player
        baseIndicators[0].GetComponent<MeshRenderer>().material = materialsColour[PlayerPrefs.GetInt("Player", 0)];
        for (int i = 1; i < characters.Length; i++) //Boucle For pour traverser le array characters � partir de 1
        {
            characters[i].GetComponent<MeshRenderer>().material = materialsColour[PlayerPrefs.GetInt("Enemy" + i, 0)]; //Donner le material de l'ennemi au couleur dans PlayerPrefs de Enemy
            mapPoints[i].GetComponent<MeshRenderer>().material = materialsColour[PlayerPrefs.GetInt("Enemy" + i, 0)]; //Donner le material de l'ic�ne Mini-Map de l'ennemi au couleur dans PlayerPrefs de Enemy
            charactersID[i].color = materialsColour[PlayerPrefs.GetInt("Enemy" + i, 0)].color; //Donner la couleur du Text de l'enemmi au couleur dans PlayerPrefs de Enemy
            baseIndicators[i].GetComponent<MeshRenderer>().material = materialsColour[PlayerPrefs.GetInt("Enemy" + i, 0)];
        }
    }
    
    public void SetEquipmentCard() //Fonction rajout�e par Sengsamrach Vong
    {
        for(int i = 0; i < arrayEquipmentCard.Length; i++)
        {
            arrayEquipmentCard[i].GetComponent<EquipDeploymentCard>().PrepareEquipmentCard();
        }
    }

    public void DeployEquipmentCard()     //Fonction rajout�e par Sengsamrach Vong
    {
        equipCardCount = 0;

        DestroyEquipmentCard();
        SetEquipmentCard();

        do
        {
            do
            {
                equipCardRandom = UnityEngine.Random.Range(0, arrayEquipmentCard.Length);
            } while (arrayEquipmentCard[equipCardRandom].activeInHierarchy);
            arrayEquipmentCard[equipCardRandom].SetActive(true);
            equipCardCount++;
        } while (equipCardCount < 8);
    }

    public void DestroyEquipmentCard()     //Fonction rajout�e par Sengsamrach Vong
    {
        for (int i = 0; i < arrayEquipmentCard.Length; i++)
        {   
            if(arrayEquipmentCard[i].activeInHierarchy)
                arrayEquipmentCard[i].GetComponent<EquipDeploymentCard>().SetInactiveEquipmentCard();
        }
    }

    public void BlockLocationShown()     //Fonction rajout�e par Sengsamrach Vong
    {
        for (int i = 1; i < mapPoints.Length; i++)
        {
            mapPoints[i].SetActive(false);
        }
    }

    public void PenaltyBaseLost()     //Fonction rajout�e par Sengsamrach Vong
    {
        characters[0].GetComponent<Player>().SetInactiveBasePlayer();
        for(int i = 1; i < characters.Length; i++)
        {
            if(characters[i] != null)
                characters[i].GetComponent<Enemy>().SetInactiveBaseEnemy();
        }
    }

    public void PenaltyLocationShown()     //Fonction rajout�e par Sengsamrach Vong
    {
        for(int i = 1; i < mapPoints.Length; i++)
        {
            if(mapPoints[i] != null)
                mapPoints[i].SetActive(true);
        }
    }

    public void PenaltyHealthLost()     //Fonction rajout�e par Sengsamrach Vong
    {
        characters[0].GetComponent<Player>().Hp -= 10;
        for (int i = 1; i < characters.Length; i++)
        {
            if(characters[i] != null)
                characters[i].GetComponent<Enemy>().Hp -= 10;
        }
    }

    public void VerifyOpponentPresent() //M�thode pour v�rifier la pr�sence des opponents dans la sc�ne
    {
        opponentGone = 0; //initialise le int � 0

        for (int i = 1; i < characters.Length; i++) //Boucle For pour traverser le array characters
        {
            if (characters[i] == null) //Condition If pour si le personnage � cette �l�ment est d�truit
            {
                opponentGone++; //Incr�mente le int
            }
        }

        if (opponentGone >= 7) //Condition If pour si le int est � 7
        {
            GameVictory(); //Aller � la m�thode GameVictory() pour d�clarer la victoire du joueur
        }
    }
    public void OnDeployment() //M�thode pour d�ployer les characters
    {
        for (int i = 0; i < pointStart.Length; i++) //Boucle For pour traverser le array pointStart
        {
            characters[i].transform.position = pointStart[i].transform.position; //D�ploie chaque joueur � un start point dans l'ar�ne 
        }
    }

    public void GameVictory() //M�thode pour la victoire du joueur
    {
        hasWon = true; 
        Time.timeScale = 0f;
        pnlGameVictory.SetActive(true); //active l'�cran de victoire 
        player.GetComponent<CharacterController>().enabled = false; //D�sactive le character controller
    }

    public void GameOver() //M�thode pour la d�faite du joueur
    {
        isGameOver = true;
        Time.timeScale = 0f;
        pnlGameOver.SetActive(true); //active l'�cran gameover 
        player.GetComponent<CharacterController>().enabled = false; //D�sactive le character controller
    }

    public void ResumeGame() //M�thode pour r�sumer le jeu
    {
        pnlPauseMenu.SetActive(false); //D�sactive le menu pause
        Time.timeScale = 1f; //remet le temps de jeu � son cours normal
        isPaused = false; //est-ce qie ;e jeu est en pause ?
        player.GetComponent<CharacterController>().enabled = true; //R�active le character controller
    }

    public void PauseGame() //M�thode pour mettre le jeu en pause
    {
        pnlPauseMenu.SetActive(true); //active le menu pause
        Time.timeScale = 0f; //freeze le temps dans le jeu
        isPaused = true; //est-ce qie ;e jeu est en pause ?
        player.GetComponent<CharacterController>().enabled = false; //R�active le character controller
    }

    public void ReturnToMainMenu() //M�thode pour retourner au menu principal
    {
        SceneManager.LoadScene(0);
    }

    public void ChangePowerRight() //fonction pour parcourir l'inventaire des powers
    {
        if (index < powerIndicators.Length)
        {
            index++;
        }
        else if (index >= powerIndicators.Length)
        {
            index = powerIndicators.Length;
        }
    }
    
    public void ChangePowerLeft() //fonction pour parcourir l'inventaire des powers
    {
        if (index > 0)
        {
            index--;
        }
        else if (index <= 0)
        {
            index = 0;
        }
    }

    public void PowerStatus() //Fonction pour indiquer le power choisi
    {
        ////CETTE BOUCLE DONNAIT UNE ERREUR AU NIVEAU DE L'INTERFACE, DONC J'AI DU PARCOURIR CHAQUE INDEX 
        ///
        //for (int i = 0; i < powerIndicators.Length; i++)
        //{
        //    if (index == i)
        //    {
        //        powerIndicators[i].SetActive(true);
        //    }
        //    else
        //    {
        //        powerIndicators[i].SetActive(false);
        //    }
        //}


        if (index == 0)
        {
            powerIndicators[0].SetActive(true);
            powerIndicators[1].SetActive(false);
            powerIndicators[2].SetActive(false);
            powerIndicators[3].SetActive(false);
            powerIndicators[4].SetActive(false);
            powerIndicators[5].SetActive(false);
        }
        else if (index == 1)
        {
            powerIndicators[0].SetActive(false);
            powerIndicators[1].SetActive(true);
            powerIndicators[2].SetActive(false);
            powerIndicators[3].SetActive(false);
            powerIndicators[4].SetActive(false);
            powerIndicators[5].SetActive(false);
        }
        else if (index == 2)
        {
            powerIndicators[0].SetActive(false);
            powerIndicators[1].SetActive(false);
            powerIndicators[2].SetActive(true);
            powerIndicators[3].SetActive(false);
            powerIndicators[4].SetActive(false);
            powerIndicators[5].SetActive(false);
        }
        else if (index == 3)
        {
            powerIndicators[0].SetActive(false);
            powerIndicators[1].SetActive(false);
            powerIndicators[2].SetActive(false);
            powerIndicators[3].SetActive(true);
            powerIndicators[4].SetActive(false);
            powerIndicators[5].SetActive(false);
        }
        else if (index == 4)
        {
            powerIndicators[0].SetActive(false);
            powerIndicators[1].SetActive(false);
            powerIndicators[2].SetActive(false);
            powerIndicators[3].SetActive(false);
            powerIndicators[4].SetActive(true);
            powerIndicators[5].SetActive(false);
        }
        else if (index == 5)
        {
            powerIndicators[0].SetActive(false);
            powerIndicators[1].SetActive(false);
            powerIndicators[2].SetActive(false);
            powerIndicators[3].SetActive(false);
            powerIndicators[4].SetActive(false);
            powerIndicators[5].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(powerChangedRight) //Est-ce que j'ai chang� de power ?
        {
            ChangePowerRight(); //Parcourir l'inventaire en incr�mentant l'index
            PowerStatus(); //modifie emplacement de l'indicateur
            powerChangedRight = false;
        }
        else if(powerChangedLeft) //Est-ce que j'ai chang� de power ?
        {
            ChangePowerLeft(); //Parcourir l'inventaire en d�cr�mentant l'index
            PowerStatus(); //modifie emplacement de l'indicateur
            powerChangedLeft = false;
        }

        timer += Time.deltaTime;
        //Characters deployment on arena
        if (timer < 3f)
        {
            OnDeployment(); //D�ploie les characters dans leurs bases
        }
        else
        {
            CancelInvoke("OnDeployment"); //le d�ploiment est annul� apr�s 3 secondes pour permettre au joueurs de patrouiller dans l'ar�ne
        }

        //V�rifier les Opponents dans la sc�ne.
        VerifyOpponentPresent();

        //Pause
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame(); //r�sume le jeu
            }
            else
            {
                PauseGame(); //mets le jeu en pause
            }
        }


        //TimeScore
            score = timer * multiplier; //mon score dnas le temps
        hpScore = character.GetComponent<Characters>().Hp * multiplier; //mes points HP * bonus
        finalScore = score + hpScore; //score final
        //Debug.Log(score);
        if (isGameOver || hasWon)
        {
            if (character.GetComponent<Characters>().Hp < 0)
            {
                //Affichage du score
                hpScore = 0;
                timeScoreTxtGO.text = "Time Score: " + finalScore.ToString("F4");
                hpScoreTxtGO.text = "HP Score: " + hpScore.ToString("F4");
                totalScoreTxtGO.text = "Total Score: " + finalScore.ToString("F4");
                timeScoreTxtGV.text = "Time Score: " + finalScore.ToString("F4");
                hpScoreTxtGV.text = "HP Score: " + hpScore.ToString("F4");
                totalScoreTxtGV.text = "Total Score: " + finalScore.ToString("F4");
                if(powerGO.GetComponent<Power>().isDoubleScore) //A revoir
                {
                    finalScore = finalScore * 2;
                    totalScoreTxtGV.text = "Total Score: " + finalScore.ToString("F4");
                }
            }
            else
            {
                //Affichage du score
                timeScoreTxtGO.text = "Time Score: " + finalScore.ToString("F4");
                hpScoreTxtGO.text = "HP Score: " + hpScore.ToString("F4");
                totalScoreTxtGO.text = "Total Score: " + finalScore.ToString("F4");
                timeScoreTxtGV.text = "Time Score: " + finalScore.ToString("F4");
                hpScoreTxtGV.text = "HP Score: " + hpScore.ToString("F4");
                totalScoreTxtGV.text = "Total Score: " + finalScore.ToString("F4");
            }
        }


        //Chrono
        chronoTime += Time.deltaTime;
        int minutes = (int) chronoTime / 60; // minutes �coul�s
        int seconds = (int) chronoTime - (minutes * 60); //secondes �coul�es
        chrono.text = "Time : " + minutes.ToString("D2") + ":" + seconds.ToString("D2"); //convertit le temps pass� en string pour l'afficher sur le canvas txt_timer

        
        //Activation d�sactivation des Ui qui indiquent si on poss�de un power ou pas (OK / NOK)
        if(pInvisibility > 0) 
        { 
            okIcon1.SetActive(true);
            nokIcon1.SetActive(false);
        } 
        else 
        {
            okIcon1.SetActive(false);
            nokIcon1.SetActive(true);
        }

        //invincibility
        if (pInvincibility > 0)
        {
            okIcon2.SetActive(true);
            nokIcon2.SetActive(false);
        }
        else
        {
            okIcon2.SetActive(false);
            nokIcon2.SetActive(true);
        }

        //instant healing
        if (pInstantHealing > 0)
        {
            okIcon3.SetActive(true);
            nokIcon3.SetActive(false);
        }
        else
        {
            okIcon3.SetActive(false);
            nokIcon3.SetActive(true);
        }

        //double speed
        if (pDoubleSpeed > 0)
        {
            okIcon4.SetActive(true);
            nokIcon4.SetActive(false);
        }
        else
        {
            okIcon4.SetActive(false);
            nokIcon4.SetActive(true);
        }

        //double damage
        if (pDoubleDamage > 0)
        {
            okIcon5.SetActive(true);
            nokIcon5.SetActive(false);
        }
        else
        {
            okIcon5.SetActive(false);
            nokIcon5.SetActive(true);
        }

        //double score
        if (pDoubleScore > 0)
        {
            okIcon6.SetActive(true);
            nokIcon6.SetActive(false);
        }
        else
        {
            okIcon6.SetActive(false);
            nokIcon6.SetActive(true);
        }


        //Partie faite par Sengsamrach Vong
        //Condition Timer pour le d�ploiement des cartes d'�quipements et des p�nalit�s.
        if (timer >= 45f && !onceTimer1) //0:45
        {
            onceTimer1 = true;
            DeployEquipmentCard();
        }
        if (timer >= 90f && !onceTimer2) //1:30
        {
            onceTimer2 = true;
            DeployEquipmentCard();
        }
        if (timer >= 135f && !onceTimer3) //2:15
        {
            onceTimer3 = true;
            PenaltyBaseLost();
        }
        if (timer >= 180f && !onceTimer4) //3:00
        {
            onceTimer4 = true;
            PenaltyLocationShown();
        }
        if (timer >= 225f) //3:45
            timerPenaltyHealth += Time.deltaTime;

        if (timerPenaltyHealth >= 5f)
        {
            timerPenaltyHealth = 0f;
            PenaltyHealthLost();
        }


    }
}
