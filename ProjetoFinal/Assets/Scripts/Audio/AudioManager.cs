using System.Collections.Generic;
using Audio;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;

    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = new GameObject("AudioManager");
                obj.AddComponent<AudioManager>();
            }

            return instance;
        }
    }
    
    [Header("Sound List Settings")]
    [SerializeField] private List<SoundInfo> sounds;
    
    [Header("Player Settings")]
    [SerializeField] private AudioSource playerAudioSource;
    
    [Header("SFX Settings")]
    [SerializeField] private AudioMixerGroup mixerGroup;
    private AudioSource oneShotAudioSource;
    private GameObject oneShotObj;
    private AudioSource withTimeAudioSource;
    private GameObject withTimeObj;
    
    private void Awake()
    {
        instance = this;
    }

    public void PlaySound(SoundType soundType, float volume)
    {
        // Intantiates an object with an audio source (if it doesn't exist)
        if (oneShotObj == null)
        {
            oneShotObj = new GameObject("OneShotObj");
            oneShotAudioSource = oneShotObj.AddComponent<AudioSource>();
            oneShotAudioSource.outputAudioMixerGroup = mixerGroup;
        }

        oneShotAudioSource.volume = volume;
        oneShotAudioSource.PlayOneShot(GetAudioClip(soundType));
    }

    public void PlaySoundTimed(SoundType soundType, float volume)
    {
        // Intantiates an object with an audio source (if it doesn't exist)
        if (withTimeObj == null)
        {
            withTimeObj = new GameObject("WithTimeObj");
            withTimeAudioSource = withTimeObj.AddComponent<AudioSource>();
            withTimeAudioSource.outputAudioMixerGroup = mixerGroup;
        }

        if (!withTimeAudioSource.isPlaying)
        {
            withTimeAudioSource.volume = volume;
            withTimeAudioSource.PlayOneShot(GetAudioClip(soundType));
        }
    }
    
    public void PlayCleaningSound()
    {
        if (!playerAudioSource.isPlaying) playerAudioSource.Play();
    }
    
    public void StopCleaingSound()
    {
        if (playerAudioSource.isPlaying) playerAudioSource.Stop();
    }
    
    private AudioClip GetAudioClip(SoundType soundType)
    {
        foreach (var soundClip in sounds)
        {
            if (soundClip.soundType == soundType) return soundClip.audioClip;
        }

        print($"Sound ({soundType}) not found in sounds list!");

        return null;
    }
}