using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OutcomeTextAnim : MonoBehaviour
{
    public TextMeshProUGUI outcomeTextToShow;
    private Coroutine fadeRoutine;

    public void PlayFade(Color colourToChange, float timeToFadeIn, float timeToHold, float timeToFadeOut)
    {
        if(fadeRoutine != null)
        {
            StopCoroutine(fadeRoutine);
        }
        fadeRoutine = StartCoroutine(FadeSequence(colourToChange, timeToFadeIn, timeToHold, timeToFadeOut));
    }

    private IEnumerator FadeSequence(Color targetColour, float fadeInTime, float holdTime, float fadeOutTime)
    {
        // Setup
        targetColour.a = 0f;
        outcomeTextToShow.color = targetColour;

        // Fade In
        float t = 0f;
        while (t < fadeInTime)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Clamp01(t / fadeInTime);
            outcomeTextToShow.color = new Color(targetColour.r, targetColour.g, targetColour.b, alpha);
            yield return null;
        }

        // Hold
        yield return new WaitForSeconds(holdTime);

        // Fade Out
        t = 0f;

        // To ensure its fully transparent
        outcomeTextToShow.color = new Color(targetColour.r, targetColour.g, targetColour.b, 0f);
        fadeRoutine = null;

    }
}
