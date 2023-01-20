using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Crée par : Oussama Arouch
/// </summary>


public class ChooseArenaScreen : MonoBehaviour
{

    //Selection de l'arène
    [SerializeField] private GameObject pnlChooseArena; //panel choose arena
    private GameObject btn_Forest; //bouton arene forest
    private GameObject btn_Ruins; //bouton arene ruins

    private void Awake()
    {
        pnlChooseArena.SetActive(true); //active le panel de choix d'arène
    }

    public void OnForestClick()
    {
        pnlChooseArena.SetActive(false); //Désactive le panel de choix d'arène
        SceneManager.LoadScene(2); //Charge la scène indéxée
    }

    public void OnRuinsClick()
    {
        pnlChooseArena.SetActive(false); //Désactive le panel de choix d'arène
        SceneManager.LoadScene(3); //Charge la scène indéxée
    }
}
