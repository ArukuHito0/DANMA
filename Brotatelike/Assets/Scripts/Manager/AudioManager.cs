using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private SoundCatalog _Catalog;

    [SerializeField] private AudioSettings audioSettings;
    [SerializeField] private AudioSource bgmAudio;
    [SerializeField] private AudioSource seAudio;

    [SerializeField] private float minPlayInterbal = 0.05f; // 音が鳴る間隔の最小時間

    private Dictionary<string, AudioClip> _BgmAudioClipDictionary = new Dictionary<string, AudioClip>();
    private Dictionary<string, AudioClip> _SeAudioClipDictionary = new Dictionary<string, AudioClip>();

    private Dictionary<string, float> lastPlayTimeDictionary = new Dictionary<string, float>(); // 各SEが鳴った最後の時間を保存する辞書

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            audioSettings.onMusicVolumeChanged += SetBgmVolume;
            audioSettings.onSeVolumeChanged += SetSeVolume;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            audioSettings.onMusicVolumeChanged -= SetBgmVolume;
            audioSettings.onSeVolumeChanged -= SetSeVolume;
        }
    }

    private void Start()
    {
        Instance = this;
        SetupSeAudioClip();
        SetupBgmAudioClip();
        DontDestroyOnLoad(this);

        bgmAudio.volume = audioSettings.musicVolume * 0.01f;
        seAudio.volume = audioSettings.seVolume * 0.01f;
    }

    private void SetBgmVolume()
    {
        bgmAudio.volume = audioSettings.musicVolume * 0.01f;
    }

    private void SetSeVolume()
    {
        seAudio.volume = audioSettings.seVolume * 0.01f;
    }

    public void PlaySe(string assetName)
    {
        if (!TryGetSeAudioClip(assetName, out var audioClip))
        {
            return;
        }

        if (Time.time - lastPlayTimeDictionary[assetName] < minPlayInterbal)
        {
            return;
        }
        else
        {
            seAudio.PlayOneShot(audioClip);
            lastPlayTimeDictionary[assetName] = Time.time;
        }
    }

    public void PlayBgm(string assetName)
    {
        if (!TryGetBgmAudioClip(assetName, out var audioClip))
        {
            return;
        }

        bgmAudio.clip = audioClip;
        bgmAudio.loop = true;
        bgmAudio.Play();
    }

    public void StopBgm()
    {
        bgmAudio.Stop();
    }

    private void SetupSeAudioClip()
    {
        foreach (var audioClip in _Catalog.SeAudioClips)
        {
            if (_SeAudioClipDictionary.ContainsKey(audioClip.name))
            {
                Debug.LogError($"SEのAudioClip = {audioClip.name} のアセット名が重複しています。");
                continue;
            }

            _SeAudioClipDictionary[audioClip.name] = audioClip;
            lastPlayTimeDictionary[audioClip.name] = -10f;
        }
    }

    private void SetupBgmAudioClip()
    {
        foreach (var audioClip in _Catalog.BgmAudioClips)
        {
            if (_BgmAudioClipDictionary.ContainsKey(audioClip.name))
            {
                Debug.LogError($"BGMのAudioClip = {audioClip.name} のアセット名が重複しています。");
                continue;
            }

            _BgmAudioClipDictionary[audioClip.name] = audioClip;
        }
    }

    private bool TryGetSeAudioClip(string assetName, out AudioClip audioClip)
    {
        if (!_SeAudioClipDictionary.TryGetValue(assetName, out audioClip))
        {
            Debug.LogError($"SEのAudioClip = {assetName} が存在しません。");
            return false;
        }

        return true;
    }

    private bool TryGetBgmAudioClip(string assetName, out AudioClip audioClip)
    {
        if (!_BgmAudioClipDictionary.TryGetValue(assetName, out audioClip))
        {
            Debug.LogError($"BGMのAudioClip = {assetName} が存在しません。");
            return false;
        }

        return true;
    }
}
