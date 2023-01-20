using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script cr�� par Sengsamrach Vong
/// </summary>

public class ColourOpponent : MonoBehaviour
{
    [SerializeField] private GameObject panelArena = null; //GameObject qui repr�sente le panel de l'ar�ne
    [SerializeField] private GameObject panelColour = null; //GameObject qui repr�sente le panel des couleurs
    [SerializeField] private GameObject panelMain = null; //GameObject qui repr�sente le panel du menu principal
    private int[] arrayColour = new int[8]; //Array de int pour les couleurs
    private int colourTemp = 0; //int pour garder temporairement une couleur
    private int colourNumber = 0; //int pour garder le num�ro de la couleur

    // Start is called before the first frame update
    void Start()
    {
        //initialiser les �l�ments de arrayColour par un chiffre
        arrayColour[0] = 0; //Couleur Rouge
        arrayColour[1] = 1; //Couleur Bleu
        arrayColour[2] = 2; //Couleur Jaune
        arrayColour[3] = 3; //Couleur Vert
        arrayColour[4] = 4; //Couleur Orange
        arrayColour[5] = 5; //Couleur Violet
        arrayColour[6] = 6; //Couleur Vert P�le
        arrayColour[7] = 7; //Couleur Bleu P�le

        PlayerPrefs.DeleteKey("Player"); //Supprimer les data de couleur de Player
        PlayerPrefs.DeleteKey("Enemy"); //Supprimer les data de couleur de Enemy

    }

    public void ColourRestart() //M�thode pour remettre le array � la valeur initiale
    {
        colourTemp = arrayColour[0]; 
        arrayColour[0] = arrayColour[colourNumber];
        arrayColour[colourNumber] = colourTemp;
    }
    public void ColourCheck() //M�thode pour v�rifier si le array
    {
        if (arrayColour[0] != 1) //Condition If pour si la valeur de l'�l�ment 0 dans arrayColour n'est pas 1
        {
            ColourRestart(); //Aller � la m�thode ColourRestart
        }
    }
    public void ColourSave() //M�thode pour sauvegarder la couleur choisie
    {
        PlayerPrefs.SetInt("Player", arrayColour[0]); //Garder dans PlayerPrefs la couleur du joueur qui se retrouve � l'�l�ment 0
        for(int i = 1; i < arrayColour.Length; i++) //Boucle For pour traverser le arrayColour � partir de 1
        {
            PlayerPrefs.SetInt("Enemy" + i, arrayColour[i]); //Garder dans PlayerPrefs la couleur des Opponents
        }

        PlayerPrefs.Save(); //Sauvegarder les donn�es
    }
    public void ChangePanel() //M�thode pour changer le panel de couleur au panel de l'ar�ne
    {
        panelColour.SetActive(false);
        panelArena.SetActive(true);
    }
    public void ColourExit() //M�thode pour changer le panel de couleur au panel du menu principal
    {
        panelColour.SetActive(false);
        panelMain.SetActive(true);
    }
    public void ColourRed() //M�thode pour placer la couleur rouge � l'�l�ment 0
    {
        ColourCheck();

        colourNumber = 0;

        ColourSave();

        ChangePanel();
    }
    public void ColourBlue() //M�thode pour placer la couleur bleu � l'�l�ment 0
    {
        ColourCheck();

        colourTemp = arrayColour[0];
        arrayColour[0] = arrayColour[1];
        arrayColour[1] = colourTemp;
        colourNumber = 1;

        ColourSave();

        ChangePanel();
    }
    public void ColourYellow() //M�thode pour placer la couleur jaune � l'�l�ment 0
    {
        ColourCheck();

        colourTemp = arrayColour[0];
        arrayColour[0] = arrayColour[2];
        arrayColour[2] = colourTemp;
        colourNumber = 2;

        ColourSave();

        ChangePanel();
    }
    public void ColourGreen() //M�thode pour placer la couleur vert � l'�l�ment 0
    {
        ColourCheck();

        colourTemp = arrayColour[0];
        arrayColour[0] = arrayColour[3];
        arrayColour[3] = colourTemp;
        colourNumber = 3;

        ColourSave();

        ChangePanel();
    }
    public void ColourOrange() //M�thode pour placer la couleur orange � l'�l�ment 0
    {
        ColourCheck();

        colourTemp = arrayColour[0];
        arrayColour[0] = arrayColour[4];
        arrayColour[4] = colourTemp;
        colourNumber = 4;

        ColourSave();

        ChangePanel();
    }
    public void ColourPurple() //M�thode pour placer la couleur violet � l'�l�ment 0
    {
        ColourCheck();

        colourTemp = arrayColour[0];
        arrayColour[0] = arrayColour[5];
        arrayColour[5] = colourTemp;
        colourNumber = 5;

        ColourSave();

        ChangePanel();
    }
    public void ColourLightGreen() //M�thode pour placer la couleur vert p�le � l'�l�ment 0
    {
        ColourCheck();

        colourTemp = arrayColour[0];
        arrayColour[0] = arrayColour[6];
        arrayColour[6] = colourTemp;
        colourNumber = 6;

        ColourSave();

        ChangePanel();
    }
    public void ColourLightBlue() //M�thode pour placer la couleur bleu p�le � l'�l�ment 0
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
