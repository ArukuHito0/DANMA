using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance { get; private set; }

    private Volume postProcessVolume;
    [SerializeField] private float fadeDuration;

    private ColorAdjustments colorAdjustments;
    private bool isTransitioning = false;

    private bool isFadeIn = false;
    private bool isFadeOut = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        postProcessVolume = GetComponent<Volume>();

        if (postProcessVolume != null)
        {
            postProcessVolume.profile.TryGet(out colorAdjustments);
        }
    }

    public void OnLoadScendClicked(string sceneName)
    {
        if (isTransitioning) return;
        StartCoroutine(FadeOutAndLoad(sceneName));
    }

    private IEnumerator FadeIn()
    {
        float startValue = colorAdjustments.postExposure.value;
        float endValue = 0;

        isFadeIn = true;

        yield return Fade(startValue, endValue);

        isFadeIn = false;
    }

    private IEnumerator FadeOutAndLoad(string sceneName)
    {
        SoundUtil.StopBgm();

        isTransitioning = true;
        float startValue = colorAdjustments.postExposure.value;
        float endValue = -10f;

        isFadeOut = true;

        yield return Fade(startValue, endValue);

        isFadeOut = false;

        SceneManager.LoadScene(sceneName);

        yield return FadeIn();

        isTransitioning = false;

        switch (sceneName)
        {
            case "TitleScene": SoundUtil.PlayBgm("Title"); break;
            case "MainScene":  SoundUtil.PlayBgm("InGame"); break;
        }
    }

    private IEnumerator Fade(float start, float end)
    {
        float elapsed = 0;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / fadeDuration;

            if (colorAdjustments != null)
            {
                colorAdjustments.postExposure.value = Mathf.Lerp(start, end, t * t);
            }

            yield return null;
        }
    }
}
