using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PowerupSelector : MonoBehaviour
{
    public GameControls gameController;
    public Sprite[] levelOnePowerupImages;
    public Sprite[] levelTwoPowerupImages;
    public Image iconOne;
    public Image iconTwo;
    public TextMeshProUGUI descriptionOne;
    public TextMeshProUGUI descriptionTwo;
    public List<string> levelOnePowerupDescriptions = new List<string>();
    public void pickPowerups(int numOne, int numTwo)
    {
        iconOne.sprite = levelOnePowerupImages[numOne];
        iconTwo.sprite = levelOnePowerupImages[numTwo];
        descriptionOne.text = levelOnePowerupDescriptions[numOne];
        descriptionTwo.text = levelOnePowerupDescriptions[numTwo];
    }

}
