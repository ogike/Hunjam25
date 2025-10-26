using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFade : MonoBehaviour
{
    public static ScreenFade Instance { get; private set; }
    
    public Image fadeToBlackScreen;

    public float defaultFadeInTime;
    public float defaultFadeOutTime;
    public float fullBlackTime;

    public AnimationCurve fadeInCurve;
    public AnimationCurve fadeOutCurve;
    private void Awake() 
    {
        if (Instance != null)
        {
            Debug.LogWarning("Found more than one ScreenFade in the scene");
        }
        Instance = this;
    }

    private void Start()
    {
        FadeIn(defaultFadeInTime);
    }

    public void FadeBetweenDays()
    {
        StartCoroutine(FadeOutSequence(defaultFadeOutTime, fullBlackTime, defaultFadeInTime));
    }


    public IEnumerator FadeOutSequence(float fadeOut, float wait, float fadeIn)
    {
        FadeOut(fadeOut);
        yield return new WaitForSeconds(fadeOut + wait);
        FadeIn(fadeIn);
    }
    
    public void SetFadeToBlackColor(float opacity)
    {
        Color color = fadeToBlackScreen.color;
        color.a = opacity;
        fadeToBlackScreen.color = color;
    }
    
    public void FadeIn(float duration)
    {
        StartCoroutine(FadeInCoroutine(duration));
    }

    public IEnumerator FadeInCoroutine(float duration)
    {
        float time = 0.0f;
        while (time <= duration)
        {
            float opacity = fadeInCurve.Evaluate(time / duration);
            SetFadeToBlackColor(opacity);
            time += Time.deltaTime;
            yield return new WaitForSeconds(0);
        }
        SetFadeToBlackColor(0);
    }
    
    public void FadeOut(float duration)
    {
        StartCoroutine(FadeOutCoroutine(duration));
    }
    
    public IEnumerator FadeOutCoroutine(float duration)
    {
        float time = 0;
        while (time <= duration)
        {
            float opacity = fadeOutCurve.Evaluate(time / duration);
            SetFadeToBlackColor(opacity);
            time += Time.deltaTime;
            yield return new WaitForSeconds(0);
        }
        SetFadeToBlackColor(1);
    }
}
