using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeAudio : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer; //Référence à mon audioMixer
    [SerializeField] private string nameVol; //Nom du parametre dans mon audioMixer
    [SerializeField] private Slider slider; //Référence au slider

    public void SetVolume(float volume)
    {
        volume = slider.value;
        audioMixer.SetFloat(nameVol, volume); //Attribue a la valeur nameParam la valeur de la variable volume
        PlayerPrefs.SetFloat(nameVol, volume); //crée des paramètres pour mon playerprefs
        PlayerPrefs.Save(); //sauvegarde les paramètres de mon playerprefs
    }

    void Start()
    {
        slider.value = PlayerPrefs.GetFloat(nameVol); //charge les paramètres de mon playerprefs
    }
}
