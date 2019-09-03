using System;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    public Sound[] sounds;
    public Sound[] music;

    public short type;
    public float masterVolume;
    public float musicVolume;
    public float sfxVolume;
    public float uiVolume;

    private void Awake() {
        LoadSoundData();
        foreach (Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            if (name == "ButtonClick")
                s.soundType = Constants.Audio.soundTypes.UI_SOUNDS;
            else if (name == "ShootingSound" || name == "WinningSound") 
                s.soundType = Constants.Audio.soundTypes.SFX;
            s.source.clip = s.clip;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            UpdateSoundVolume(s);
        }
        foreach (Sound s in music) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.soundType = Constants.Audio.soundTypes.MUSIC;
            s.source.clip = s.clip;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            UpdateSoundVolume(s);
        }
        if (!RuntimeSpecs.startedBackgroundMusic) {
            RuntimeSpecs.startedBackgroundMusic = true;
            PickRandomMusic().source.Play();
        }
    }
    public void FixedUpdate() {
        if (!music[music.Length - 1].source.isPlaying)
            PickRandomMusic().source.Play();
    }

    public void Play(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

    public void UpdateSoundVolume(Sound s) {
        switch (s.soundType) {
            case Constants.Audio.soundTypes.MUSIC:
                s.source.volume = musicVolume * masterVolume;
                break;
            case Constants.Audio.soundTypes.SFX:
                s.source.volume = sfxVolume * masterVolume;
                break;
            case Constants.Audio.soundTypes.UI_SOUNDS:
                s.source.volume = uiVolume * masterVolume;
                break;
        }
    }

    public void UpdateVolume() {
        foreach (Sound s in sounds)
            UpdateSoundVolume(s);
        foreach (Sound s in music)
            UpdateSoundVolume(s);
    }
    private void LoadSoundData() {
        SavingSystem.getInstance().Load(Constants.Data.dataTypes.AUDIO_DATA);
        masterVolume = RuntimeSpecs.masterVolume;
        musicVolume = RuntimeSpecs.musicVolume;
        sfxVolume = RuntimeSpecs.sfxVolume;
        uiVolume = RuntimeSpecs.uiVolume;
    }
    public Sound PickRandomMusic() {
        int i = 0;
        i = UnityEngine.Random.Range(0, music.Length - 2);
        Sound temp = music[music.Length-1];
        music[music.Length-1] = music[i];
        music[music.Length - 1].source = music[i].source;
        music[i] = temp;
        music[i].source = temp.source;
        return music[music.Length-1];
    }
}
