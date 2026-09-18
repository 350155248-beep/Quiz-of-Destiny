using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
public class PowerupActivator : MonoBehaviour
{
    [Header("Source")]
    public GameControls gameController;

    [Header("Point Multiplier/Doubler")]
    public PointsToGetUIAnim pointsToGetInRegularModeScript;
    public double multiplierValue;

    [Header("Dig Deep(er)")]
    public string[] sectionOneHints;
    public string[] sectionTwoHints;
    public string[] sectionThreeHints;
    public string[] sectionFourHints;
    public string[] allHints;
    public TextMeshProUGUI hintText;
    public float hintCountdown;
    public bool hintCountdownRunning;
    public TextMeshProUGUI hintCountdownText;
    [Header("Answer Remover/Half Off")]
    public List<GameObject> questionBoxes = new List<GameObject>();
    public List<GameObject> removedQuestionBoxes = new List<GameObject>();
    public List<GameObject> qBoxesData = new List<GameObject>();
    private int removeCount;

    void Start() {
        allHints = sectionOneHints.Concat(sectionTwoHints).Concat(sectionThreeHints)
        .Concat(sectionFourHints).ToArray();
    }
    void Update()
    {
        if(hintCountdownRunning == true)
        {
            hintCountdown -= Time.deltaTime;
            hintCountdownText.text = Mathf.CeilToInt(hintCountdown).ToString();
        } else
        {
            hintCountdownText.text = "0";
            hintText.text = "Activate Dig Deep for a hint! For an extra hint, use Dig Deeper!";
        }
    }
    public void ActivatePowerup(string name)
    {
        if(name == "Lifesaver")
        {
            gameController.lives += 1;
            gameController.livesText.text = gameController.lives.ToString();
        } else if (name == "Boss Stop")
        {
            gameController.bossQuestionChance -= 5;
            gameController.bqcText.text = gameController.bossQuestionChance.ToString();
        } else if (name == "Point Multiplier")
        {
            multiplierValue = 1.5;
            gameController.multiplierText.text = "X1.5";
        } else if(name == "Pass")
        {
            gameController.questionText = gameController.allQuestionsList[Random.Range(0, gameController.allQuestionsList.Length)];
            gameController.question.StartTyping(gameController.questionText);
        } else if(name == "Dig Deep")
        {
            ShowHint(5);
        } else if (name == "Answer Remover")
        {
            removeCount = 1;
            questionBoxes.Remove(questionBoxes[(int)gameController.correctButtonNum]);
            for(int i = 0; i < removeCount && questionBoxes.Count > 0; i++)
            {
                int randomIndex = Random.Range(0, questionBoxes.Count);
                GameObject btnToRemove = questionBoxes[randomIndex];
                btnToRemove.SetActive(false);
                removedQuestionBoxes.Add(questionBoxes[randomIndex]);
                questionBoxes.RemoveAt(randomIndex);
            }
        } else if (name == "Enough Time")
        {
            gameController.time += 15f;
        } else if (name == "Ultra Lifesaver")
        {
            gameController.lives += 1;
            gameController.livesText.text = gameController.lives.ToString();
            if(GameControls.endlessActivated)
            {
                gameController.score += 1;
                gameController.scoreText.text = gameController.score.ToString();
            } else
            {
                gameController.score += Mathf.Round((pointsToGetInRegularModeScript.currentScore * 0.25f));
                gameController.scoreText.text = gameController.score.ToString();
            }
        } else if (name == "Boss Protector")
        {
            gameController.bossQuestionChance -= 10;
            gameController.bqcText.text = gameController.bossQuestionChance.ToString();
        } else if (name == "Point Doubler")
        {
            multiplierValue = 2;
            gameController.multiplierText.text = "X2";
        } else if (name == "Ultra Pass")
        {
            gameController.questionText = gameController.allQuestionsList[Random.Range(0, gameController.allQuestionsList.Length)];
            gameController.question.StartTyping(gameController.questionText);
            if(GameControls.endlessActivated)
            {
                gameController.score += 1;
                gameController.scoreText.text = gameController.score.ToString();
            } else
            {
                gameController.score += Mathf.Round((pointsToGetInRegularModeScript.currentScore * 0.25f));
                gameController.scoreText.text = gameController.score.ToString();
            }
        } else if (name == "Dig Deeper")
        {
            ShowHint(10);
        } else if (name == "Half Off")
        {
            removeCount = 2;
            questionBoxes.Remove(questionBoxes[(int)gameController.correctButtonNum]);
            for(int i = 0; i < removeCount && questionBoxes.Count > 0; i++)
            {
                int randomIndex = Random.Range(0, questionBoxes.Count);
                GameObject btnToRemove = questionBoxes[randomIndex];
                btnToRemove.SetActive(false);
                removedQuestionBoxes.Add(questionBoxes[randomIndex]);
                questionBoxes.RemoveAt(randomIndex);
            }
        } else if (name == "Scroll of Time")
        {
            gameController.time += 30f;
        }
    }
    public void ShowHint(float seconds)
    {
        hintCountdown = seconds;
        hintCountdownRunning = true;
        int i = gameController.questionCode;
        hintText.text = (i >= 0 && i < allHints.Length ? allHints[i]: "No hint available.");
    }
}
