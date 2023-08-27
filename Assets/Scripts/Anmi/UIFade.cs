using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class UIFade : MonoBehaviour {
    private Image fadeScreen;
    private readonly float fadeSpeed = 1f;

    private IEnumerator fadeRoutine;

    private void Awake() {
        fadeScreen = GetComponent<Image>();
    }

    private void Start() {
        fadeScreen = GetComponent<Image>();
        FadeToClear();
    }

    public void FadeToBlack() {
        if (fadeRoutine != null) {
            StopCoroutine(fadeRoutine);
        }

        fadeRoutine = FadeRoutine(1);
        StartCoroutine(fadeRoutine);
    }

    public void FadeToClear() {
        if (fadeRoutine != null) {
            StopCoroutine(fadeRoutine);
        }

        fadeRoutine = FadeRoutine(0);
        StartCoroutine(fadeRoutine);
    }

    private IEnumerator FadeRoutine(float targetAlpha) {
        while (!Mathf.Approximately(fadeScreen.color.a, targetAlpha)) {
            float alpha = Mathf.MoveTowards(fadeScreen.color.a, targetAlpha, fadeSpeed * Time.deltaTime);
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, alpha);
            yield return null;
        }
    }
}