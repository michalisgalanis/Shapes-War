using System;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    public Sound[] sounds;
    public float masterVolume = 1;
    public float musicVolume = 1;
    public float sfxVolume = 1;

    private void Awake() {
        foreach (Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            if (s.clip.name == "ButtonClick")
                s.type = "SFX";
        }
    }

    public void Play(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

    public void UpdateVolume() {
        foreach (Sound s in sounds) {
            switch (s.type) {
                case "Music":
                    s.source.volume = musicVolume* masterVolume;
                    break;
                case "SFX":
                    s.source.volume = sfxVolume* masterVolume;
                    break;
            }
            //Debug.Log("Master Volume = " + masterVolume+" Source volume = " + s.source.volume);
        }
    }
}
