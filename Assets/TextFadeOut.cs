using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class TextFadeOut : MonoBehaviour
{
    public float fadeStartTime = 2;

    public Text levelText;
    //Fade time in seconds
    public float fadeOutTime = 2;

    Color originalColor;

    void Start()
    {
        Invoke("FadeOut", fadeStartTime);
    }

    public void FadeOut()
    {
        StartCoroutine(FadeOutRoutine());
    }
    private IEnumerator FadeOutRoutine()
    {
        Text text = GetComponent<Text>();
        Color originalColor = text.color;
        for (float t = 0.01f; t < fadeOutTime; t += Time.deltaTime)
        {
            text.color = Color.Lerp(originalColor, Color.clear, Mathf.Min(1, t / fadeOutTime));
            yield return null;
        }
    }
}

