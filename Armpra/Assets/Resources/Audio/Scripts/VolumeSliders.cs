using UnityEngine.UI;
using UnityEngine;

public class VolumeSliders : MonoBehaviour
{
    private GameObject amObject;
    private AudioManager audioManager;
    private float volume;
    private void Start() {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        UpdateSliders();
    }
    public void ChangeMasterVolume() {
        audioManager.masterVolume= GetComponent<Slider>().value;
        RuntimeSpecs.masterVolume = audioManager.masterVolume;
        audioManager.UpdateVolume();
    }
    public void ChangeMusicVolume() {
        audioManager.musicVolume = GetComponent<Slider>().value;
        RuntimeSpecs.musicVolume = audioManager.musicVolume;
        audioManager.UpdateVolume();
    }
    public void ChangeSFXVolume() {
        audioManager.sfxVolume = GetComponent<Slider>().value;
        RuntimeSpecs.sfxVolume = audioManager.sfxVolume;
        audioManager.UpdateVolume();
    }
    public void ChangeUIVolume() {
        audioManager.uiVolume = GetComponent<Slider>().value;
        RuntimeSpecs.uiVolume = audioManager.uiVolume;
        audioManager.UpdateVolume();
    }

    public void UpdateSliders() {
        switch (gameObject.name) {
            case "MasterVolumeSlider":
                GetComponent<Slider>().value = RuntimeSpecs.masterVolume;
                break;
            case "MusicVolumeSlider":
                GetComponent<Slider>().value = RuntimeSpecs.musicVolume;
                break;
            case "SFXVolumeSlider":
                GetComponent<Slider>().value = RuntimeSpecs.sfxVolume;
                break;
            case "UIVolumeSlider":
                GetComponent<Slider>().value = RuntimeSpecs.uiVolume;
                break;
        }
        
    }
}
