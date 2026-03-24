using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AudioVolumeTextUpdater : MonoBehaviour
{
    [SerializeField] private AudioSettings audioSettings;

    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider seVolumeSlider;
    [SerializeField] private Image musicVolumeHandleIcon;
    [SerializeField] private Image seVolumeHandleIcon;
    [SerializeField] private Sprite[] volumeIcons;
    [SerializeField] private TextMeshProUGUI musicVolumeText;
    [SerializeField] private TextMeshProUGUI seVolumeText;

    private void OnEnable()
    {
        audioSettings.onMusicVolumeChanged += UpdateMusicVolumeText;
        audioSettings.onSeVolumeChanged += UpdateSeVolumeText;
        audioSettings.onSetDefaultVolume += SetVolumeBar;
    }

    private void OnDisable()
    {
        audioSettings.onMusicVolumeChanged -= UpdateMusicVolumeText;
        audioSettings.onSeVolumeChanged -= UpdateSeVolumeText;
        audioSettings.onSetDefaultVolume -= SetVolumeBar;
    }

    private void Start()
    {
        UpdateMusicVolumeText();
        UpdateSeVolumeText();
        SetVolumeBar();
    }

    private void UpdateMusicVolumeText()
    {
        musicVolumeText.SetText("{0} %", (int)audioSettings.musicVolume);
        musicVolumeHandleIcon.sprite = GetVolumeSprite(audioSettings.musicVolume);
    }

    private void UpdateSeVolumeText()
    {
        seVolumeText.SetText("{0} %", (int)audioSettings.seVolume);
        seVolumeHandleIcon.sprite = GetVolumeSprite(audioSettings.seVolume);
    }

    private void SetVolumeBar()
    {
        musicVolumeSlider.SetValueWithoutNotify(audioSettings.musicVolume);
        seVolumeSlider.SetValueWithoutNotify(audioSettings.seVolume);
    }

    private Sprite GetVolumeSprite(float volume)
    {
        if (volume <= 0)
            return volumeIcons[0];
        else if (volume < 30)
            return volumeIcons[1];
        else if (volume < 80)
            return volumeIcons[2];
        else
            return volumeIcons[3];
    }
}
