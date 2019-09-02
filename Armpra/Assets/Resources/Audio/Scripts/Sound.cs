using UnityEngine;

[System.Serializable]
public class Sound {
    public string name;
    public Constants.Audio.soundTypes soundType;
    public bool loop = false;

    public AudioClip clip;

    [Range(0.1f, 3f)]
    public float pitch = 1f;

    [HideInInspector]
    public AudioSource source;
}
