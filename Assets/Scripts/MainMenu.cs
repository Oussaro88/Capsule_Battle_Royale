using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Crée par : Oussama Arouch
/// </summary>


public class MainMenu : MonoBehaviour
{

    //Panels
    [SerializeField] private GameObject pnl_MainMenu; //Mon main menu 
    [SerializeField] private GameObject pnl_Options; //menu options
    [SerializeField] private GameObject pnl_HowToPlay; //menu how to play
    [SerializeField] private GameObject pnl_ChooseColor; //séléction de couleur

    [SerializeField] private GameObject[] texts; //UI des bouton keyboard + gamepad + tips
    private int index = 0;

    void Awake()
    {
        pnl_MainMenu.SetActive(true);
        texts[0].SetActive(true);
    }

    public void OnStartGame()
    {
        pnl_MainMenu.SetActive(false);
        pnl_ChooseColor.SetActive(true);
    }

    public void OnOptions()
    {
        pnl_MainMenu.SetActive(false);
        pnl_Options.SetActive(true);
    }

    public void OnReturnToMenu()
    {
        pnl_MainMenu.SetActive(true);
        pnl_Options.SetActive(false);
        pnl_HowToPlay.SetActive(false);
    }

    public void OnHowToPlay()
    {
        pnl_MainMenu.SetActive(false);
        pnl_HowToPlay.SetActive(true);
    }

    public void OnKeyboard() // bouton keyboard
    {
        index = 0;
        for (int i = 0; i < texts.Length; i++)
        {
            if(i == index)
            {
                texts[i].SetActive(true);
            }
            else
            {
                texts[i].SetActive(false);
            }
        }
    }

    public void OnGamepad() // bouton gamepad
    {
        index = 1;
        for (int i = 0; i < texts.Length; i++)
        {
            if (i == index)
            {
                texts[i].SetActive(true);
            }
            else
            {
                texts[i].SetActive(false);
            }
        }
    }

    public void OnTips() // bouton tips
    {
        index = 2;
        for (int i = 0; i < texts.Length; i++)
        {
            if (i == index)
            {
                texts[i].SetActive(true);
            }
            else
            {
                texts[i].SetActive(false);
            }
        }
    }

    public void OnQuitGame()
    {
        Application.Quit(); //quitte l'application
    }
}
