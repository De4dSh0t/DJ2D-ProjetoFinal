using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    [Header("UI Settings")]
    [SerializeField] private Slider sfx;
    [SerializeField] private Slider music;
    [SerializeField] private Toggle sfxToggle;
    [SerializeField] private Toggle musicToggle;
    
    [Header("Mixer Settings")]
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private AudioSettings audioSettings;
    
    private void Start()
    {
        sfx.onValueChanged.AddListener(UpdateSfxVolume);
        music.onValueChanged.AddListener(UpdateMusicVolume);
        sfxToggle.onValueChanged.AddListener(UpdateSfx);
        musicToggle.onValueChanged.AddListener(UpdateMusic);
        
        sfx.value = audioSettings.sfxVolume;
        music.value = audioSettings.musicVolume;
        sfxToggle.isOn = audioSettings.sfx;
        musicToggle.isOn = audioSettings.music;
    }
    
    private void UpdateSfxVolume(float value)
    {
        mixer.SetFloat("sfxVolume", Mathf.Log10(value) * 20);
        audioSettings.sfxVolume = value;
    }
    
    private void UpdateMusicVolume(float value)
    {
        mixer.SetFloat("musicVolume", Mathf.Log10(value) * 20);
        audioSettings.musicVolume = value;
    }
    
    private void UpdateSfx(bool state)
    {
        switch (state)
        {
            case true:
                UpdateSfxVolume(audioSettings.sfxVolume);
                break;
            case false:
                mixer.SetFloat("sfxVolume", -80f);
                break;
        }
        
        audioSettings.sfx = state;
    }
    
    private void UpdateMusic(bool state)
    {
        switch (state)
        {
            case true:
                UpdateMusicVolume(audioSettings.musicVolume);
                break;
            case false:
                mixer.SetFloat("musicVolume", -80f);
                break;
        }
        
        audioSettings.music = state;
    }
}