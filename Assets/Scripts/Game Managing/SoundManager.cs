using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioSource[] audioSources;
    public static float soundVolume = 1f;
    private static AudioClip wingSFX, pointSFX, hitSFX, dieSFX, swooshingSFX;

    public enum AudioType
    {
        wing = 0, point, hit, die, swooshing
    };

    public AudioSource AddAudio(AudioClip clip, bool loop, bool playAwake, float vol)
    {
        AudioSource newAudio = gameObject.AddComponent<AudioSource>();
        newAudio.clip = clip;
        newAudio.loop = loop;
        newAudio.playOnAwake = playAwake;
        newAudio.volume = vol;
        return newAudio;
    }

    void Awake()
    {
        // Load SFX
        wingSFX = Resources.Load<AudioClip>("sfx_wing");
        pointSFX = Resources.Load<AudioClip>("sfx_point");
        hitSFX = Resources.Load<AudioClip>("sfx_hit");
        dieSFX = Resources.Load<AudioClip>("sfx_die");
        swooshingSFX = Resources.Load<AudioClip>("sfx_swooshing");

        audioSources = new AudioSource[5];
        audioSources[0] = AddAudio(wingSFX, false, false, soundVolume);
        audioSources[1] = AddAudio(pointSFX, false, false, soundVolume);
        audioSources[2] = AddAudio(hitSFX, false, false, soundVolume);
        audioSources[3] = AddAudio(dieSFX, false, false, soundVolume);
        audioSources[4] = AddAudio(swooshingSFX, false, false, soundVolume);
    }

    // Play SFX 
    public static void Play(AudioType audioType)
    {
        audioSources[(int)audioType].Play();
    }

    public static void Play(int audioType)
    {
        audioSources[audioType].Play();
    }
}
