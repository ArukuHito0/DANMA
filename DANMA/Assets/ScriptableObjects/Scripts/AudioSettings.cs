using System;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioSettings", menuName = "Audio/Settings")]
public class AudioSettings : ScriptableObject
{
    [SerializeField] private float defaultMusicVolume = 30;
    [SerializeField] private float defaultSeVolume = 30;

    private float _musicVolume = 30;
    private float _seVolume = 30;

    public float musicVolume
    {
        get => _musicVolume;
        set
        {
            _musicVolume = value;
            onMusicVolumeChanged?.Invoke();
        }
    }

    public float seVolume
    {
        get => _seVolume;
        set
        {
            _seVolume = value;
            onSeVolumeChanged?.Invoke();
        }
    }

    public event Action onMusicVolumeChanged;
    public event Action onSeVolumeChanged;
    public event Action onSetDefaultVolume;

    public void OnMusicVolumeChanged(float volume)
    {
        musicVolume = volume;
    }

    public void OnSeVolumeChanged(float volume)
    {
        seVolume = volume;
    }

    public void OnSetDefaultVolume()
    {
        musicVolume = defaultMusicVolume;
        seVolume = defaultSeVolume;

        onSetDefaultVolume?.Invoke();
    }
}
