using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.UI;

/// <summary>
/// Crée par : Oussama Arouch
/// </summary>


public class Loading : MonoBehaviour
{

    private AsyncOperation async; //toutes les valeurs de chargements
    [SerializeField] private Image loadProgress;
    [SerializeField] private Text txtProgress;

    void Init()
    {
        Time.timeScale = 1.0f;
        Input.ResetInputAxes(); //éviter une doubel séléction des boutons
        System.GC.Collect(); //Vider la RAM des variables inutiles
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
        Scene currentScene = SceneManager.GetActiveScene(); //récupère la scène actuelle
        async = SceneManager.LoadSceneAsync(currentScene.buildIndex + 1);
        async.allowSceneActivation = false; //attend confirmation pour passer à la scène suivante
    }

    // Update is called once per frame
    void Update()
    {
        if (loadProgress)
        {
            loadProgress.fillAmount = async.progress + 0.1f; //remplit la barre au dela de 90%
        }

        if (txtProgress)
        {
            txtProgress.text = ((async.progress + 0.1f) * 100).ToString("F2") + " %"; //rend une valeur decimale avec deux chiffres apres la virgule
        }

        if (async.progress >= 0.9f && SplashScreen.isFinished)
        {
            async.allowSceneActivation = true;
        }
    }

}
