using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenuManager : MonoBehaviour
{
    [SerializeField]
    private AudioMixer audioMixer;

    [SerializeField]
    private Slider masterSlider;
    [SerializeField]
    private Slider enviromentSlider;
    [SerializeField]
    private Slider musicSlider;
    [SerializeField]
    private Slider UIMenuSlider;
    [SerializeField]
    private Slider SfxSlider;

    public void SetMasterVolume() => SetVolume("Master", masterSlider.value);
    public void SetEnviromentVolume() => SetVolume("Enviroment", enviromentSlider.value);
    public void SetUIMENUVolume() => SetVolume("UI/MENU", UIMenuSlider.value);
    public void SetMusicVolume() => SetVolume("Music", musicSlider.value);
    public void SetSFXVolume() => SetVolume("SFX", SfxSlider.value);


    private void SetVolume(string parameter, float value)
    {
        audioMixer.SetFloat(parameter, Mathf.Log10(value) * 20);
    }
}
