using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PointsToGetUIAnim : MonoBehaviour
{
   public TextMeshProUGUI scoreText;
   public int maxScore = 1000;
   public int currentScore;
   public float totalTime = 30f;
   private float timeRemaining;
   private float scoreDecreaseRate;
   public GameControls gameControls;
   void Start()
    {
        totalTime = gameControls.time;
    }

    void Update()
    {
        timeRemaining = gameControls.time;
        currentScore = Mathf.CeilToInt(maxScore * (timeRemaining / totalTime));
        currentScore = Mathf.Max(currentScore, 0);
        scoreText.text = "+ " + currentScore.ToString();
    }
}
