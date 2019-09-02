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
        audioManager.UpdateVolume();
    }
    public void ChangeMusicVolume() {
        audioManager.musicVolume = GetComponent<Slider>().value;
        audioManager.UpdateVolume();
    }
    public void ChangeSFXVolume() {
        audioManager.sfxVolume = GetComponent<Slider>().value;
        audioManager.UpdateVolume();
    }
}
