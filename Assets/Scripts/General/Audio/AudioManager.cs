using System;
using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    public Sound[] uiSounds;
    public Sound[] musicSounds;
    public Sound[] sfxSounds;
    public Sound[] bossMusic;
    [HideInInspector] public Sound nextSong;

    private IEnumerator ChangeS;

    private bool alreadyPlaying = false;

    public void Start() {
        SavingSystem.getInstance().Load(Utility.Data.dataTypes.SETTINGS_DATA);
        foreach (Sound s in uiSounds)
            CreateSoundSources(s);
        foreach (Sound s in musicSounds)
            CreateSoundSources(s);
        foreach (Sound s in sfxSounds)
            CreateSoundSources(s);
        foreach (Sound s in bossMusic)
            CreateSoundSources(s);
        UpdateAllVolumes();
        PlayRandomMusic();
    }

    public void UpdateSoundVolume(Sound s) {
        switch (s.soundType) {
            case Utility.Audio.soundTypes.MUSIC:
                s.source.volume = GameSettings.musicVolume * GameSettings.masterVolume * s.volume;
                break;
            case Utility.Audio.soundTypes.SFX:
                s.source.volume = GameSettings.sfxVolume * GameSettings.masterVolume * s.volume;
                break;
            case Utility.Audio.soundTypes.UI_SOUNDS:
                s.source.volume = GameSettings.uiVolume * GameSettings.masterVolume * s.volume;
                break;
            case Utility.Audio.soundTypes.BOSS_MUSIC:
                s.source.volume = GameSettings.musicVolume * GameSettings.masterVolume * s.volume;
                break;
        }
    }

    public void UpdateVolume(Utility.Audio.soundTypes soundType) {
        switch (soundType) {
            case Utility.Audio.soundTypes.UI_SOUNDS:
                foreach (Sound s in uiSounds)
                    UpdateSoundVolume(s);
                break;
            case Utility.Audio.soundTypes.SFX:
                foreach (Sound s in sfxSounds)
                    UpdateSoundVolume(s);
                break;
            case Utility.Audio.soundTypes.MUSIC:
                foreach (Sound s in musicSounds)
                    UpdateSoundVolume(s);
                break;
            case Utility.Audio.soundTypes.BOSS_MUSIC:
                foreach (Sound s in musicSounds)
                    UpdateSoundVolume(s);
                break;
        }
    }

    public void UpdateAllVolumes() {
        for (int i = 0; i < Enum.GetValues(typeof(Utility.Audio.soundTypes)).Length; i++)
            UpdateVolume((Utility.Audio.soundTypes)Enum.GetValues(typeof(Utility.Audio.soundTypes)).GetValue(i));
    }

    //Play Methods
    public void PlayByNumber(Utility.Audio.soundTypes typeOfSound, int numberInArray) {
        Sound finalSound = null;
        switch (typeOfSound) {
            case Utility.Audio.soundTypes.UI_SOUNDS:
                finalSound = uiSounds[numberInArray];
                break;
            case Utility.Audio.soundTypes.SFX:
                finalSound = sfxSounds[numberInArray];
                break;
            case Utility.Audio.soundTypes.BOSS_MUSIC:
                finalSound = bossMusic[numberInArray];
                break;
            case Utility.Audio.soundTypes.MUSIC:
                finalSound = musicSounds[numberInArray];
                break;
        }
        finalSound.source.Play();
    }

    public void PlayRandomMusic() {
        int i;
        System.Random random = new System.Random();
        if (alreadyPlaying) {
            i = random.Next(0, musicSounds.Length - 1);
            StopMusic();
        } else
            i = random.Next(0, musicSounds.Length);
        alreadyPlaying = true;
        nextSong = musicSounds[i];
        nextSong.source = musicSounds[i].source;
        musicSounds[i] = musicSounds[musicSounds.Length - 1];
        musicSounds[i].source = musicSounds[musicSounds.Length - 1].source;
        musicSounds[musicSounds.Length - 1] = nextSong;
        musicSounds[musicSounds.Length - 1].source = nextSong.source;
        StartCoroutine(FadeIn(nextSong, 5f, true));
        ChangeS = ChangeSong(nextSong.clip.length);
        StartCoroutine(ChangeS);
    }

    public void PlayBossMusic() {
        if (!bossMusic[0].source.isPlaying) {
            StartCoroutine(FadeIn(nextSong, 1f, false));
            StartCoroutine(FadeIn(bossMusic[0], 2f, true));
        }
        StopCoroutine(ChangeS);
    }

    public void StopMusic() {
        if (nextSong.source.isPlaying || bossMusic[0].source.isPlaying) {
            nextSong.source.Stop();
            bossMusic[0].source.Stop();
            StopCoroutine(ChangeS);
        }
    }

    public void CreateSoundSources(Sound s) {
        s.source = gameObject.AddComponent<AudioSource>();
        s.source.clip = s.clip;
        s.source.pitch = s.pitch;
        s.source.loop = s.loop;
        UpdateSoundVolume(s);
    }

    static IEnumerator FadeIn(Sound s, float timeInSeconds, bool fadeIn) {
        float volume = 0f;
        float maxVolume = s.source.volume;
        timeInSeconds /= maxVolume;
        switch (fadeIn) {
            case true:
                s.source.volume = 0f;
                s.source.Play();
                while (volume < maxVolume) {
                    volume += 0.1f / timeInSeconds;
                    s.source.volume = volume;
                    yield return new WaitForSecondsRealtime(0.1f);
                }
                break;
            case false:
                volume = maxVolume;
                while (volume > 0f) {
                    volume -= 0.1f / timeInSeconds;
                    s.source.volume = volume;
                    yield return new WaitForSecondsRealtime(0.1f);
                }
                s.source.Stop();
                break;
        }
        GameObject.FindGameObjectWithTag(Utility.Tags.AUDIO_MANAGER_TAG).GetComponent<AudioManager>().UpdateSoundVolume(s);
    }

    public static IEnumerator FadeInFrom(Sound s, float timeInSeconds, float startingVolume, float endingVolume) {
        if (endingVolume != startingVolume) {
            timeInSeconds /= Mathf.Abs(endingVolume - startingVolume);
            if (endingVolume >= startingVolume) {
                if (!s.source.isPlaying) {
                    s.source.Play();
                    s.source.volume = startingVolume;
                }
                while (startingVolume < endingVolume) {
                    startingVolume += 0.1f / timeInSeconds;
                    s.source.volume = startingVolume;
                    yield return new WaitForSecondsRealtime(0.1f);
                }
            } else {
                while (startingVolume > 0f) {
                    startingVolume -= 0.1f / timeInSeconds;
                    s.source.volume = startingVolume;
                    yield return new WaitForSecondsRealtime(0.1f);
                }
                s.source.Stop();
            }
            GameObject.FindGameObjectWithTag(Utility.Tags.AUDIO_MANAGER_TAG).GetComponent<AudioManager>().UpdateSoundVolume(s);
        }
    }

    static IEnumerator ChangeSong(float duration) {
        float time = 0f;
        while (time < duration) {
            time += 1f;
            yield return new WaitForSecondsRealtime(1f);
        }
        GameObject.FindGameObjectWithTag(Utility.Tags.AUDIO_MANAGER_TAG).GetComponent<AudioManager>().PlayRandomMusic();
    }
}