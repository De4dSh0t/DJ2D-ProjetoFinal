using UnityEngine;

[CreateAssetMenu(fileName = "AudioSettings", menuName = "ScriptableObjects/AudioSettings", order = 4)]
public class AudioSettings : ScriptableObject
{
    public bool sfx;
    public bool music;
    public float sfxVolume;
    public float musicVolume;
}