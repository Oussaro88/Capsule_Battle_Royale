using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script créé par Sengsamrach Vong
/// </summary>

public class ColourOpponent : MonoBehaviour
{
    [SerializeField] private GameObject panelArena = null; //GameObject qui représente le panel de l'arène
    [SerializeField] private GameObject panelColour = null; //GameObject qui représente le panel des couleurs
    [SerializeField] private GameObject panelMain = null; //GameObject qui représente le panel du menu principal
    private int[] arrayColour = new int[8]; //Array de int pour les couleurs
    private int colourTemp = 0; //int pour garder temporairement une couleur
    private int colourNumber = 0; //int pour garder le numéro de la couleur

    // Start is called before the first frame update
    void Start()
    {
        //initialiser les éléments de arrayColour par un chiffre
        arrayColour[0] = 0; //Couleur Rouge
        arrayColour[1] = 1; //Couleur Bleu
        arrayColour[2] = 2; //Couleur Jaune
        arrayColour[3] = 3; //Couleur Vert
        arrayColour[4] = 4; //Couleur Orange
        arrayColour[5] = 5; //Couleur Violet
        arrayColour[6] = 6; //Couleur Vert Pâle
        arrayColour[7] = 7; //Couleur Bleu Pâle

        PlayerPrefs.DeleteKey("Player"); //Supprimer les data de couleur de Player
        PlayerPrefs.DeleteKey("Enemy"); //Supprimer les data de couleur de Enemy

    }

    public void ColourRestart() //Méthode pour remettre le array à la valeur initiale
    {
        colourTemp = arrayColour[0]; 
        arrayColour[0] = arrayColour[colourNumber];
        arrayColour[colourNumber] = colourTemp;
    }
    public void ColourCheck() //Méthode pour vérifier si le array
    {
        if (arrayColour[0] != 1) //Condition If pour si la valeur de l'élément 0 dans arrayColour n'est pas 1
        {
            ColourRestart(); //Aller à la méthode ColourRestart
        }
    }
    public void ColourSave() //Méthode pour sauvegarder la couleur choisie
    {
        PlayerPrefs.SetInt("Player", arrayColour[0]); //Garder dans PlayerPrefs la couleur du joueur qui se retrouve à l'élément 0
        for(int i = 1; i < arrayColour.Length; i++) //Boucle For pour traverser le arrayColour à partir de 1
        {
            PlayerPrefs.SetInt("Enemy" + i, arrayColour[i]); //Garder dans PlayerPrefs la couleur des Opponents
        }

        PlayerPrefs.Save(); //Sauvegarder les données
    }
    public void ChangePanel() //Méthode pour changer le panel de couleur au panel de l'arène
    {
        panelColour.SetActive(false);
        panelArena.SetActive(true);
    }
    public void ColourExit() //Méthode pour changer le panel de couleur au panel du menu principal
    {
        panelColour.SetActive(false);
        panelMain.SetActive(true);
    }
    public void ColourRed() //Méthode pour placer la couleur rouge à l'élément 0
    {
        ColourCheck();

        colourNumber = 0;

        ColourSave();

        ChangePanel();
    }
    public void ColourBlue() //Méthode pour placer la couleur bleu à l'élément 0
    {
        ColourCheck();

        colourTemp = arrayColour[0];
        arrayColour[0] = arrayColour[1];
        arrayColour[1] = colourTemp;
        colourNumber = 1;

        ColourSave();

        ChangePanel();
    }
    public void ColourYellow() //Méthode pour placer la couleur jaune à l'élément 0
    {
        ColourCheck();

        colourTemp = arrayColour[0];
        arrayColour[0] = arrayColour[2];
        arrayColour[2] = colourTemp;
        colourNumber = 2;

        ColourSave();

        ChangePanel();
    }
    public void ColourGreen() //Méthode pour placer la couleur vert à l'élément 0
    {
        ColourCheck();

        colourTemp = arrayColour[0];
        arrayColour[0] = arrayColour[3];
        arrayColour[3] = colourTemp;
        colourNumber = 3;

        ColourSave();

        ChangePanel();
    }
    public void ColourOrange() //Méthode pour placer la couleur orange à l'élément 0
    {
        ColourCheck();

        colourTemp = arrayColour[0];
        arrayColour[0] = arrayColour[4];
        arrayColour[4] = colourTemp;
        colourNumber = 4;

        ColourSave();

        ChangePanel();
    }
    public void ColourPurple() //Méthode pour placer la couleur violet à l'élément 0
    {
        ColourCheck();

        colourTemp = arrayColour[0];
        arrayColour[0] = arrayColour[5];
        arrayColour[5] = colourTemp;
        colourNumber = 5;

        ColourSave();

        ChangePanel();
    }
    public void ColourLightGreen() //Méthode pour placer la couleur vert pâle à l'élément 0
    {
        ColourCheck();

        colourTemp = arrayColour[0];
        arrayColour[0] = arrayColour[6];
        arrayColour[6] = colourTemp;
        colourNumber = 6;

        ColourSave();

        ChangePanel();
    }
    public void ColourLightBlue() //Méthode pour placer la couleur bleu pâle à l'élément 0
    {
        ColourCheck();

        colourTemp = arrayColour[0];
        arrayColour[0] = arrayColour[7];
        arrayColour[7] = colourTemp;
        colourNumber = 7;

        ColourSave();

        ChangePanel();
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
