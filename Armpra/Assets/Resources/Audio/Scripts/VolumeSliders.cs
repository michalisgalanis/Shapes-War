using UnityEngine.UI;
using UnityEngine;

public class VolumeSliders : MonoBehaviour
{
    private GameObject amObject;
    private AudioManager audioManager;
    private float volume;
    private void Start() {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
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
}
