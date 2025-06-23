using UnityEngine;

[System.Serializable]
public class Sound
{
    public string nama;
    public AudioType type;
    public AudioClip klip;
    public float vol;
    public bool berulang;

    [HideInInspector]
    public AudioSource source;
}

public enum AudioType
{
    Audio,
    Sfx
}
