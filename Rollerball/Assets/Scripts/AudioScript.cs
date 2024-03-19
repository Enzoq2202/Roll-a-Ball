using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour
{
    public AudioSource music;
    public AudioSource soundeffect;
    public  AudioClip clip;
    public AudioClip backgroundmusic;

    void Start()
    {
        music.clip = backgroundmusic;
        music.loop = true;
        music.Play();
    }

    public void PlaySoundEffect()
    {
        soundeffect.clip = clip;
        soundeffect.Play();
    }
}
