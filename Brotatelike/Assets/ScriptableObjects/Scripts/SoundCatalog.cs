using NUnit.Framework;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundCatalog", menuName = "Audio/SoundCatalog")]
public class SoundCatalog : ScriptableObject
{
    public ReadOnlySpan<AudioClip> SeAudioClips => _SeAudioClips;
    public ReadOnlySpan<AudioClip> BgmAudioClips => _BgmAudioClips;

    [SerializeField]
    private AudioClip[] _SeAudioClips;

    [SerializeField]
    private AudioClip[] _BgmAudioClips;
}
