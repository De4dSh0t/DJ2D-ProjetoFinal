using System;
using UnityEngine;

namespace Audio
{
    [Serializable]
    public struct SoundInfo
    {
        public SoundType soundType;
        public AudioClip audioClip;
    }
}