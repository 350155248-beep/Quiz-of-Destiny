using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestionTextAnim : MonoBehaviour
{
    public TextMeshProUGUI textOnScreen;
        private string textToShow;
        private Coroutine typingCoroutine;
        public GameControls gc;

        private readonly WaitForSeconds activateTimeGapForAnimation = new WaitForSeconds(0.05f);

    // Start is called before the first frame update
    void Start()
    {
        textToShow = gc.questionText;
        StartTyping(textToShow);
    }

    public void StartTyping(string text)
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);   
        }
        typingCoroutine = StartCoroutine(typeText(text));
    }

    private IEnumerator typeText (string typedText)
    {
        textOnScreen.text = typedText;
        textOnScreen.maxVisibleCharacters = 0;
        for (int i = 0; i < typedText.Length; i++)
        {
            textOnScreen.maxVisibleCharacters = i+1;
            yield return activateTimeGapForAnimation;
        }
    }
}


