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
        audioManager.masterVolume= 0.1f * GetComponent<Slider>().value;
        RuntimeSpecs.masterVolume = audioManager.masterVolume;
        audioManager.UpdateVolume();
    }
    public void ChangeMusicVolume() {
        audioManager.musicVolume = 0.1f * GetComponent<Slider>().value;
        RuntimeSpecs.musicVolume = audioManager.musicVolume;
        audioManager.UpdateVolume();
    }
    public void ChangeSFXVolume() {
        audioManager.sfxVolume = 0.1f * GetComponent<Slider>().value;
        RuntimeSpecs.sfxVolume = audioManager.sfxVolume;
        audioManager.UpdateVolume();
    }
    public void ChangeUIVolume() {
        audioManager.uiVolume = 0.1f * GetComponent<Slider>().value;
        RuntimeSpecs.uiVolume = audioManager.uiVolume;
        audioManager.UpdateVolume();
    }

    public void UpdateSliders() {
        switch (gameObject.name) {
            case "MasterVolumeSlider":
                GetComponent<Slider>().value = 10f * RuntimeSpecs.masterVolume;
                break;
            case "MusicVolumeSlider":
                GetComponent<Slider>().value = 10f * RuntimeSpecs.musicVolume;
                break;
            case "SFXVolumeSlider":
                GetComponent<Slider>().value = 10f * RuntimeSpecs.sfxVolume;
                break;
            case "UIVolumeSlider":
                GetComponent<Slider>().value = 10f * RuntimeSpecs.uiVolume;
                break;
        }
        
    }
}
