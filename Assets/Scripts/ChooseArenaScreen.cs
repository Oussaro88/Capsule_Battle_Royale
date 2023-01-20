using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Cr�e par : Oussama Arouch
/// </summary>


public class ChooseArenaScreen : MonoBehaviour
{

    //Selection de l'ar�ne
    [SerializeField] private GameObject pnlChooseArena; //panel choose arena
    private GameObject btn_Forest; //bouton arene forest
    private GameObject btn_Ruins; //bouton arene ruins

    private void Awake()
    {
        pnlChooseArena.SetActive(true); //active le panel de choix d'ar�ne
    }

    public void OnForestClick()
    {
        pnlChooseArena.SetActive(false); //D�sactive le panel de choix d'ar�ne
        SceneManager.LoadScene(2); //Charge la sc�ne ind�x�e
    }

    public void OnRuinsClick()
    {
        pnlChooseArena.SetActive(false); //D�sactive le panel de choix d'ar�ne
        SceneManager.LoadScene(3); //Charge la sc�ne ind�x�e
    }
}
