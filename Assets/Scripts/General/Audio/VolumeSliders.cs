using UnityEngine.UI;
using UnityEngine;

public class VolumeSliders : MonoBehaviour
{

    private GameObject amObject;
    private AudioManager audioManager;
    private float volume;


    private void Start() {
        audioManager = GameObject.FindGameObjectWithTag(Utility.Tags.AUDIO_MANAGER_TAG).GetComponent<AudioManager>();
        UpdateSliders();
    }
    public void ChangeMasterVolume() {
        if (audioManager == null) audioManager = GameObject.FindGameObjectWithTag(Utility.Tags.AUDIO_MANAGER_TAG).GetComponent<AudioManager>();
        //Debug.Log(audioManager + "||" + GetComponent<Slider>());
        GameSettings.masterVolume= 0.1f * GetComponent<Slider>().value;
        audioManager.UpdateVolume(Utility.Audio.soundTypes.UI_SOUNDS);
        audioManager.UpdateVolume(Utility.Audio.soundTypes.SFX);
        audioManager.UpdateVolume(Utility.Audio.soundTypes.MUSIC);
    }
    public void ChangeUIVolume() {
        //Debug.Log(audioManager + "||" + GetComponent<Slider>());
        GameSettings.uiVolume = 0.1f * GetComponent<Slider>().value;
        audioManager.UpdateVolume(Utility.Audio.soundTypes.UI_SOUNDS);
    }
    public void ChangeSFXVolume() {
        //Debug.Log(audioManager + "||" + GetComponent<Slider>());
        GameSettings.sfxVolume = 0.1f * GetComponent<Slider>().value;
        audioManager.UpdateVolume(Utility.Audio.soundTypes.SFX);
    }
    public void ChangeMusicVolume() {
        //Debug.Log(audioManager + "||" + GetComponent<Slider>());
        GameSettings.musicVolume = 0.1f * GetComponent<Slider>().value;
        audioManager.UpdateVolume(Utility.Audio.soundTypes.MUSIC);
    }

    public void UpdateSliders() {
        switch (gameObject.name) {
            case "MasterVolumeSlider":
                GetComponent<Slider>().value = GetComponent<Slider>().maxValue * GameSettings.masterVolume;
                break;
            case "MusicVolumeSlider":
                GetComponent<Slider>().value = GetComponent<Slider>().maxValue * GameSettings.musicVolume;
                break;
            case "SFXVolumeSlider":
                GetComponent<Slider>().value = GetComponent<Slider>().maxValue * GameSettings.sfxVolume;
                break;
            case "UIVolumeSlider":
                GetComponent<Slider>().value = GetComponent<Slider>().maxValue * GameSettings.uiVolume;
                break;
        }
        
    }
}
