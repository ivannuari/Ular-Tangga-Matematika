using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private Sound[] sounds;

    [SerializeField] private float audioVolume, sfxVolume;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        foreach (var s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.klip;
            s.source.loop = s.berulang;
        }

        SetVolume();
    }

    public float GetAudioVolume()
    {
        return audioVolume;
    }

    public float GetSfxVolume()
    {
        return sfxVolume;
    }

    public void SetVolume()
    {
        if (PlayerPrefs.HasKey("Audio"))
        {
            audioVolume = PlayerPrefs.GetFloat("Audio");
        }

        if (PlayerPrefs.HasKey("Sfx"))
        {
            sfxVolume = PlayerPrefs.GetFloat("Sfx");
        }

        //find audio bgm
        Sound findSound = Array.Find(sounds, s => s.nama == "BGM");
        findSound.source.volume = audioVolume;

        //find all sfx
        Sound[] findSfx = Array.FindAll(sounds, s => s.type == AudioType.Sfx);
        foreach (var item in findSfx)
        {
            item.source.volume = sfxVolume;
        }
    }

    public void MainkanSuara(string namaSuara)
    {
        Sound findSound = Array.Find(sounds, s => s.nama == namaSuara);
        if(findSound != null)
        {
            findSound.source.Play();
        }
    }

    public void HentikanSuara(string namaSuara)
    {
        Sound findSound = Array.Find(sounds, s => s.nama == namaSuara);
        if (findSound != null)
        {
            findSound.source.Stop();
        }
    }

    public void HentikanSemuaSuara()
    {
        foreach (var item in sounds)
        {
            item.source.mute = true;
        }
    }

    public void NyalakanSemuaSuara()
    {
        foreach (var item in sounds)
        {
            item.source.mute = false;
        }
    }
}
