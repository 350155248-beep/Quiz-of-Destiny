using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameControls : MonoBehaviour
{
    [Header("Game Settings")]
    public static bool endlessActivated;
    public GameObject scoreToGetArea;
    public float questionsToGameEnd;
    public GameObject mainScreen;
    public GameObject resultsScreen;
    public TextMeshProUGUI finalScoreText;

    [Header("Questions")]
    public string[] topicOneQuestionList;
    public string[] topicTwoQuestionList;
    public string[] topicThreeQuestionList;
    public string[] topicFourQuestionList;
    public string[] allQuestionsList;
    public string[] correctAnswers;
    public List <string> wrongAnswersSet1 = new List<string>();
    public List <string> wrongAnswersSet2 = new List<string>();
    public List <string> wrongAnswersSet3 = new List<string>();
    public QuestionTextAnim question;
    [TextArea] public string questionText;
    public int questionCode;
    public int questionCodeNumber;

    [Header("Timer")]
    public float time;
    public bool timerRunning;
    public TextMeshProUGUI timerText;
    public PointsToGetUIAnim timerSliderAnimation;

    [Header("Lives")]
    public float lives;
    public TextMeshProUGUI livesText;

    [Header("BQC")]
    public float bossQuestionChance;
    public TextMeshProUGUI bqcText;
    public float bqcValue;
    public GameObject bqcAlertText;

    [Header("QTCP")]
    public TextMeshProUGUI qtcpText;
    public float questionsToPowerupSelect;

    [Header("Evolution Cooldown")]
    public float correctQuestionsToEvolve;
    public TextMeshProUGUI questionsToEvolveText;

    [Header("Powerups Selection and Generation")]
    public List<Powerup> levelOnePowerups = new List<Powerup>();
    public List<Powerup> levelTwoPowerups = new List<Powerup>();
    public PowerupSelector powerupSelector;
    public List<Powerup> powerupsChosen = new List<Powerup>();
    public TextMeshProUGUI powerupNameOne;
    public TextMeshProUGUI powerupNameTwo;
    public Button selectButtonOne;
    public Button selectButtonTwo;
    public List<Powerup> powerupsInInventory = new List<Powerup>();
    public Powerup newPowerup = new Powerup();
    public GameObject maxPowerupsReachedText;
    public float amountOfPowerups;
    public Powerup removedPowerup = new Powerup();
    public Sprite pointMultiplierIconSprite;
    public Sprite bossStopIconSprite;

    [Header("Powerups Usage")]
    public List<Image> powerupScreenImages = new List<Image>();
    public List<TextMeshProUGUI> powerupScreenTextBoxes = new List<TextMeshProUGUI>();
    public Sprite blankPowerupImage;
    private Sprite buttonUsed;
    public GameObject powerupScreenErrorText;
    public TextMeshProUGUI powerupScreenErrorEditableText;
    public bool usingPowerup;
    public TextMeshProUGUI usingPowerupButtonText;
    public PowerupActivator powerupActivator;
    public TextMeshProUGUI multiplierText;
    public TextMeshProUGUI powerupScreenTitleText;
    public GameObject mainPowerupScreen;
    public GameObject evoBookletScreen;
    public bool onEvolutionMode;
    public Powerup bossStopPowerup;
    public Powerup pointMultiplierPowerup;
    [Header("Others")]
    public GameObject powerUpScreen;
    public float score;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI outcomeText;
    public OutcomeTextAnim outcomeTextAnimation;
    public GameObject gameOverScreen;
    public GameObject gameOverText;
    public GameObject[] objectsToDisappearForPowerupScreen;
    public GameObject powerupSelectionScreen;

    [Header("Buttons")]
    public Button optionButton1;
    public Button optionButton2;
    public Button optionButton3;
    public Button optionButton4;
    public TextMeshProUGUI optionButtonText1;
    public TextMeshProUGUI optionButtonText2;
    public TextMeshProUGUI optionButtonText3;
    public TextMeshProUGUI optionButtonText4;
    public Button usePowerupButton;
    public Button powerupButtonOne;
    public Button powerupButtonTwo;
    public Button powerupButtonThree;
    public Button powerupButtonFour;
    public Button powerupButtonFive;
    public Button evoBookletButton;
    public Button evolvePowerupButton;
    public Button returnToPowerupScreenButton;
    public Button returnToTitleScreenButton;
    public float correctButtonNum;
    private float buttonPressed;
    private bool powerupScreenOpen;

    [Header("Sound Effects")]
    public AudioSource soundPlayer;
    public AudioSource gameMusicPlayer;
    public AudioClip correctSound;
    public AudioClip wrongSound;
    public AudioClip openPowerupsSound;
    public AudioClip timesUpSound;
    public AudioClip gameOverSound;
    public AudioClip powerupScreenAppearsSound;
    public AudioClip claimPowerupSound;
    public AudioClip usePowerupSound;
    public AudioClip errorSound;
    public AudioClip evoBookletSound;
    public AudioClip evolvePowerupSound;
    public AudioClip bossQuestionAlertSound;

    [Header("Category Selection")]
    public TitleScreenScript titleScreenScript;
    public int categoryNumber;
    public GameObject categorySelectionScreen;
    public Button categoryButtonOne;
    public Button categoryButtonTwo;
    public Button categoryButtonThree;
    public Button categoryButtonFour;

    // Start is called before the first frame update
    void Start()
    {
        usingPowerup = false;
        timerText.text = Mathf.CeilToInt(time).ToString();
        scoreText.text = score.ToString();
        livesText.text = lives.ToString();
        qtcpText.text = questionsToPowerupSelect.ToString();
        questionsToEvolveText.text = correctQuestionsToEvolve.ToString() + " to evolve!";
        optionButton1.onClick.AddListener(ButtonOneClicked);
        optionButton2.onClick.AddListener(ButtonTwoClicked);
        optionButton3.onClick.AddListener(ButtonThreeClicked);
        optionButton4.onClick.AddListener(ButtonFourClicked);
        usePowerupButton.onClick.AddListener(OpenPowerups);
        selectButtonOne.onClick.AddListener(PowerupOneSelected);
        selectButtonTwo.onClick.AddListener(PowerupTwoSelected);
        powerupButtonOne.onClick.AddListener(() => UsePowerupSlot(0));
        powerupButtonTwo.onClick.AddListener(() => UsePowerupSlot(1));
        powerupButtonThree.onClick.AddListener(() => UsePowerupSlot(2));
        powerupButtonFour.onClick.AddListener(() => UsePowerupSlot(3));
        powerupButtonFive.onClick.AddListener(() => UsePowerupSlot(4));
        categoryButtonOne.onClick.AddListener(loadCategoryOne);
        categoryButtonTwo.onClick.AddListener(loadCategoryTwo);
        categoryButtonThree.onClick.AddListener(loadCategoryThree);
        categoryButtonFour.onClick.AddListener(loadCategoryFour);
        evoBookletButton.onClick.AddListener(openEvolutionBooklet);
        returnToPowerupScreenButton.onClick.AddListener(closeEvolutionBooklet);
        evolvePowerupButton.onClick.AddListener(evolvePowerup);
        returnToTitleScreenButton.onClick.AddListener(returnToTitle);
        soundPlayer = GetComponent<AudioSource>();
        allQuestionsList = topicOneQuestionList.Concat(topicTwoQuestionList)
        .Concat(topicThreeQuestionList).Concat(topicFourQuestionList).ToArray();
        correctButtonNum = Random.Range(0, 4);
        for( int i = 0; i < powerupScreenImages.Count; i++)
        {
            powerupScreenImages[i].sprite = powerupsInInventory[i].icon;
            powerupScreenTextBoxes[i].text = powerupsInInventory[i].name;
        }
    }

    public void StartGame()
    {
        time = 30f;
        timerRunning = true;
        resultsScreen.SetActive(false);
        mainScreen.SetActive(true);
        maxPowerupsReachedText.SetActive(false);
        powerupActivator.hintCountdownText.text = "0";
        gameMusicPlayer.Play();
        if(endlessActivated)
        {
        scoreToGetArea.SetActive(false);
        questionCodeNumber = Random.Range(0, allQuestionsList.Length);
        questionText = allQuestionsList[questionCodeNumber];
        question.StartTyping(questionText);
        bqcText.text = bossQuestionChance.ToString() + "%";
        levelOnePowerups.RemoveAll(p => p.name == "Point Multiplier");
        } else {
        scoreToGetArea.SetActive(true);
        bqcText.text = "0%";
        levelOnePowerups.RemoveAll(p => p.name == "Boss Stop");
        questionsToPowerupSelect = 5;
        qtcpText.text = questionsToPowerupSelect.ToString();
        correctQuestionsToEvolve = 10;
        questionsToEvolveText.text = correctQuestionsToEvolve.ToString();
        questionsToGameEnd = 20;
        correctButtonNum = Random.Range(0, 4);
        score = 0;
        scoreText.text = score.ToString();
        lives = 1;
        livesText.text = lives.ToString();
        powerupSelectionScreen.SetActive(false);
        mainScreen.SetActive(true);
            if(categoryNumber == 0)
            {
                questionCode = Random.Range(0, topicOneQuestionList.Length);
                questionCodeNumber = questionCode;
                questionText = topicOneQuestionList[questionCode];
                question.StartTyping(questionText);
            } 
            else if(categoryNumber == 1)
            {
                questionCode = Random.Range(0, topicTwoQuestionList.Length);
                questionCodeNumber = questionCode + 24;
                questionText = topicTwoQuestionList[questionCode];
                question.StartTyping(questionText);
            } 
            else if(categoryNumber == 2)
            {
                questionCode = Random.Range(0, topicThreeQuestionList.Length);
                questionText = topicThreeQuestionList[questionCode];
                questionCodeNumber = questionCode + 49;
                question.StartTyping(questionText);
            } 
            else if(categoryNumber == 3)
            {
                questionCode = Random.Range(0, topicFourQuestionList.Length);
                questionCodeNumber = questionCode + 74;
                questionText = topicThreeQuestionList[questionCode];
                question.StartTyping(questionText);
            }
        }
        SetAnswerButtons();
    }
    // Update is called once per frame
    void Update()
    {
        if(timerRunning)
        {
        if (time > 0)
        {
            time -= Time.deltaTime;
            timerText.text = Mathf.CeilToInt(time).ToString();
            timerRunning = true;
        } else {
            timerText.text = "0";
            timerRunning = false;
            soundPlayer.PlayOneShot(timesUpSound);
            StartCoroutine(ShowGameOverScreen());
        }
        }
        
    }

    public void SetAnswerButtons()
    {
        if(correctButtonNum == 0)
        {
            optionButtonText1.text =  correctAnswers[questionCodeNumber];
            optionButtonText2.text =  wrongAnswersSet1[questionCodeNumber];
            optionButtonText3.text =  wrongAnswersSet2[questionCodeNumber];
            optionButtonText4.text =  wrongAnswersSet3[questionCodeNumber];
            
        }else if(correctButtonNum == 1)
        {
            optionButtonText1.text =  wrongAnswersSet1[questionCodeNumber];
            optionButtonText2.text =  correctAnswers[questionCodeNumber];
            optionButtonText3.text =  wrongAnswersSet2[questionCodeNumber];
            optionButtonText4.text =  wrongAnswersSet3[questionCodeNumber];
        } else if(correctButtonNum == 2)
        {
            optionButtonText1.text = wrongAnswersSet1[questionCodeNumber];
            optionButtonText2.text = wrongAnswersSet2[questionCodeNumber];
            optionButtonText3.text = correctAnswers[questionCodeNumber];
            optionButtonText4.text = wrongAnswersSet3[questionCodeNumber];
        } else if(correctButtonNum == 3)
        {
            optionButtonText1.text = wrongAnswersSet1[questionCodeNumber];
            optionButtonText2.text = wrongAnswersSet2[questionCodeNumber];
            optionButtonText3.text = wrongAnswersSet3[questionCodeNumber];
            optionButtonText4.text = correctAnswers[questionCodeNumber];
        }
    }
    public void CheckAnswer(float buttonNum)
    {
        buttonPressed = buttonNum;
        usingPowerup = false;
        usingPowerupButtonText.text = "Use Powerup";
        powerupActivator.multiplierValue = 1;
        multiplierText.text = "X1";
        maxPowerupsReachedText.SetActive(false);
        powerupActivator.questionBoxes.Clear();
        powerupActivator.questionBoxes.AddRange(powerupActivator.qBoxesData);
        foreach (GameObject button in powerupActivator.questionBoxes)
        {
            button.SetActive(true);
        }
    if(correctButtonNum == buttonPressed)
    {
        timerRunning = true;
        outcomeText.text = "Correct!";
        outcomeTextAnimation.PlayFade(Color.green, 0.5f, 1f, 1.5f);
        soundPlayer.PlayOneShot(correctSound);
        if(correctQuestionsToEvolve > 0)
        {
            correctQuestionsToEvolve -= 1;
            questionsToEvolveText.text = correctQuestionsToEvolve.ToString() + " to evolve!"; 
        }
        if(endlessActivated)
        {
        questionCodeNumber = Random.Range(0, allQuestionsList.Length);
        questionText = allQuestionsList[questionCodeNumber];
        question.StartTyping(questionText);  
        score += 1;
        scoreText.text = score.ToString();
        } 
        else
        {
            score += timerSliderAnimation.currentScore * (float)powerupActivator.multiplierValue;
            scoreText.text = score.ToString();
            if(categoryNumber == 0)
            {
                questionCode = Random.Range(0, topicOneQuestionList.Length);
                questionCodeNumber = questionCode;
                questionText = topicOneQuestionList[questionCode];
                question.StartTyping(questionText);
            } else if(categoryNumber == 1)
            {
                questionCode = Random.Range(0, topicTwoQuestionList.Length);
                questionCodeNumber = questionCode + 24;
                questionText = topicTwoQuestionList[questionCode];
                question.StartTyping(questionText);
            } else if(categoryNumber == 2)
            {
                questionCode = Random.Range(0, topicThreeQuestionList.Length);
                questionText = topicThreeQuestionList[questionCode];
                questionCodeNumber = questionCode + 49;
                question.StartTyping(questionText);
            } else if(categoryNumber == 3)
            {
                questionCode = Random.Range(0, topicFourQuestionList.Length);
                questionCodeNumber = questionCode + 74;
                questionText = topicThreeQuestionList[questionCode];
                question.StartTyping(questionText);
            }
        }
        if(amountOfPowerups != 5)
        {
            questionsToPowerupSelect -= 1;
            qtcpText.text = questionsToPowerupSelect.ToString();
            if(questionsToPowerupSelect == 0f && amountOfPowerups != 5) {
            foreach(GameObject obj in objectsToDisappearForPowerupScreen)
            {
                obj.SetActive(false);
            }
            timerRunning = false;
            powerupSelectionScreen.SetActive(true);
            soundPlayer.PlayOneShot(powerupScreenAppearsSound);
            int firstIndex = Random.Range(0, levelOnePowerups.Count);
            int secondIndex = Random.Range(0, levelOnePowerups.Count);
            powerupsChosen.Add(levelOnePowerups[firstIndex]);
            powerupsChosen.Add(levelOnePowerups[secondIndex]);
            powerupNameOne.text = powerupsChosen[0].name;
            powerupNameTwo.text = powerupsChosen[1].name;
            powerupSelector.pickPowerups(firstIndex, secondIndex);
            }
        } 
    } else {
            outcomeText.text = "Wrong!";
            outcomeTextAnimation.PlayFade(Color.red, 0.5f, 1f, 1.5f);
            soundPlayer.PlayOneShot(wrongSound);
            livesText.text = lives.ToString();
            if(endlessActivated) {
                if(lives == 0)
                {
                    timerText.text = "0";
                    StartCoroutine(ShowGameOverScreen());
                } else {
                    if(endlessActivated)
                    {
                    questionCode = Random.Range(0, allQuestionsList.Length);
                    questionText = allQuestionsList[questionCode];
                    question.StartTyping(questionText);  
                    } else
                    {
                        if(categoryNumber == 0)
                        {
                            questionCode = Random.Range(0, topicOneQuestionList.Length);
                            questionCodeNumber = questionCode;
                            questionText = topicOneQuestionList[questionCode];
                            question.StartTyping(questionText);
                        } else if(categoryNumber == 1)
                        {
                            questionCode = Random.Range(0, topicTwoQuestionList.Length);
                            questionCodeNumber = questionCode + 24;
                            questionText = topicTwoQuestionList[questionCode];
                            question.StartTyping(questionText);
                        } else if(categoryNumber == 2)
                        {
                            questionCode = Random.Range(0, topicThreeQuestionList.Length);
                            questionText = topicThreeQuestionList[questionCode];
                            questionCodeNumber = questionCode + 49;
                            question.StartTyping(questionText);
                        } else if(categoryNumber == 3)
                        {
                            questionCode = Random.Range(0, topicFourQuestionList.Length);
                            questionCodeNumber = questionCode + 74;
                            questionText = topicThreeQuestionList[questionCode];
                            question.StartTyping(questionText);
                        }
                    } 
                }
            } else {
                lives -= 1;
                livesText.text = lives.ToString();
                if(lives == 0)
                {
                    timerRunning = false;
                    timerText.text = "0";
                    StartCoroutine(ShowGameOverScreen());
                } else {
                    if(endlessActivated)
                    {
                    questionCode = Random.Range(0, allQuestionsList.Length);
                    questionText = allQuestionsList[questionCode];
                    question.StartTyping(questionText);  
                    } else
                    {
                        if(categoryNumber == 0)
                        {
                            questionCode = Random.Range(0, topicOneQuestionList.Length);
                            questionCodeNumber = questionCode;
                            questionText = topicOneQuestionList[questionCode];
                            question.StartTyping(questionText);
                        } else if(categoryNumber == 1)
                        {
                            questionCode = Random.Range(0, topicTwoQuestionList.Length);
                            questionCodeNumber = questionCode + 24;
                            questionText = topicTwoQuestionList[questionCode];
                            question.StartTyping(questionText);
                        } else if(categoryNumber == 2)
                        {
                            questionCode = Random.Range(0, topicThreeQuestionList.Length);
                            questionText = topicThreeQuestionList[questionCode];
                            questionCodeNumber = questionCode + 49;
                            question.StartTyping(questionText);
                        } else if(categoryNumber == 3)
                        {
                            questionCode = Random.Range(0, topicFourQuestionList.Length);
                            questionCodeNumber = questionCode + 74;
                            questionText = topicThreeQuestionList[questionCode];
                            question.StartTyping(questionText);
                        }
                    } 
                }
            }
        }
        correctButtonNum = Random.Range(0, 4);
        if(endlessActivated) {
        bqcValue = Random.Range(0f, 101f);
        bqcValue = Mathf.Clamp(bqcValue, 0f, 100f);
        time = 30;
        if(bqcValue <= bossQuestionChance)
        {
            soundPlayer.PlayOneShot(bossQuestionAlertSound);
            time = 5;
            timerText.text = Mathf.CeilToInt(time).ToString();
            bqcAlertText.SetActive(true);
        } else
        {
            soundPlayer.Stop();
            time = 30;
            timerText.text = Mathf.CeilToInt(time).ToString();
            bqcAlertText.SetActive(false);
        }
        } 
        else
        {
            questionsToGameEnd -= 1;
            if(questionsToGameEnd == 0)
            {
                finalScoreText.text = "Score:" + score.ToString();
                mainScreen.SetActive(false);
                resultsScreen.SetActive(true);
            }
        }
        SetAnswerButtons();
    }

    public void ButtonOneClicked()
    {
        CheckAnswer(0f);
    }

    public void ButtonTwoClicked()
    {
        CheckAnswer(1f);
    }

    public void ButtonThreeClicked()
    {
        CheckAnswer(2f);
    }

    public void ButtonFourClicked()
    {
        CheckAnswer(3f);
    }

    public void PowerupOneSelected()
    {
        AddPowerupToInventory(0);
    }

    public void PowerupTwoSelected()
    {
        AddPowerupToInventory(1);
    }

    public void UsePowerup(int index)
    {
        if(powerupsInInventory[index].name == "None")
        {
            powerupScreenTextBoxes[index].text = "None";
            powerupScreenImages[index].sprite = blankPowerupImage;
            powerupScreenErrorEditableText.text = "There is no powerup in this slot!";
            StartCoroutine(ShowError(powerupScreenErrorText, 2f));
            soundPlayer.PlayOneShot(errorSound);   
            updatePowerupSlots();
            return;
        } 
        else 
        {
            usingPowerup = true;
            usingPowerupButtonText.text = "You are using a powerup.";
            powerUpScreen.SetActive(false);
            maxPowerupsReachedText.SetActive(false);
            powerupScreenOpen = false;
            powerupActivator.ActivatePowerup(powerupsInInventory[index].name);
            powerupsInInventory[index].name = "None";
            powerupsInInventory[index].icon = blankPowerupImage;
            powerupScreenTextBoxes[index].text = "None";
            powerupScreenImages[index].sprite = blankPowerupImage;
            soundPlayer.PlayOneShot(usePowerupSound);
            amountOfPowerups -= 1;
        }
    }
    public void UsePowerupSlot(int slot)
    {
        if(onEvolutionMode)
        {
            switch(powerupScreenTextBoxes[slot].text)
            {
                case "Lifesaver":
                soundPlayer.PlayOneShot(evolvePowerupSound);
                powerupScreenImages[slot].sprite = levelTwoPowerups[0].icon;
                powerupScreenTextBoxes[slot].text = levelTwoPowerups[0].name;
                powerupsInInventory[slot].name = powerupScreenTextBoxes[4].text;
                powerupsInInventory[slot].icon = powerupScreenImages[4].sprite;
                powerupScreenTitleText.text = "Powerups:";
                onEvolutionMode = false;
                correctQuestionsToEvolve = 10;
                questionsToEvolveText.text = correctQuestionsToEvolve.ToString() + " to evolve!";
                break;

                case "Boss Stop":
                soundPlayer.PlayOneShot(evolvePowerupSound);
                powerupScreenImages[slot].sprite = levelTwoPowerups[1].icon;
                powerupScreenTextBoxes[slot].text = levelTwoPowerups[1].name;
                powerupsInInventory[slot].name = powerupScreenTextBoxes[4].text;
                powerupsInInventory[slot].icon = powerupScreenImages[4].sprite;
                powerupScreenTitleText.text = "Powerups:";
                onEvolutionMode = false;
                correctQuestionsToEvolve = 10;
                questionsToEvolveText.text = correctQuestionsToEvolve.ToString() + " to evolve!";
                break;

                case "Point Multiplier":
                soundPlayer.PlayOneShot(evolvePowerupSound);
                powerupScreenImages[slot].sprite = levelTwoPowerups[2].icon;
                powerupScreenTextBoxes[slot].text = levelTwoPowerups[2].name;
                powerupsInInventory[slot].name = powerupScreenTextBoxes[4].text;
                powerupsInInventory[slot].icon = powerupScreenImages[4].sprite;
                powerupScreenTitleText.text = "Powerups:";
                onEvolutionMode = false;
                correctQuestionsToEvolve = 10;
                questionsToEvolveText.text = correctQuestionsToEvolve.ToString() + " to evolve!";
                break;

                case "Pass":
                soundPlayer.PlayOneShot(evolvePowerupSound);
                powerupScreenImages[slot].sprite = levelTwoPowerups[3].icon;
                powerupScreenTextBoxes[slot].text = levelTwoPowerups[3].name;
                powerupsInInventory[slot].name = powerupScreenTextBoxes[4].text;
                powerupsInInventory[slot].icon = powerupScreenImages[4].sprite;
                powerupScreenTitleText.text = "Powerups:";
                onEvolutionMode = false;
                correctQuestionsToEvolve = 10;
                questionsToEvolveText.text = correctQuestionsToEvolve.ToString() + " to evolve!";
                break;

                case "Dig Deep":
                soundPlayer.PlayOneShot(evolvePowerupSound);
                powerupScreenImages[slot].sprite = levelTwoPowerups[4].icon;
                powerupScreenTextBoxes[slot].text = levelTwoPowerups[4].name;
                powerupsInInventory[slot].name = powerupScreenTextBoxes[4].text;
                powerupsInInventory[slot].icon = powerupScreenImages[4].sprite;
                powerupScreenTitleText.text = "Powerups:";
                onEvolutionMode = false;
                correctQuestionsToEvolve = 10;
                questionsToEvolveText.text = correctQuestionsToEvolve.ToString() + " to evolve!";
                break;

                case "Answer Remover":
                soundPlayer.PlayOneShot(evolvePowerupSound);
                powerupScreenImages[slot].sprite = levelTwoPowerups[5].icon;
                powerupScreenTextBoxes[slot].text = levelTwoPowerups[5].name;
                powerupsInInventory[slot].name = powerupScreenTextBoxes[4].text;
                powerupsInInventory[slot].icon = powerupScreenImages[4].sprite;
                powerupScreenTitleText.text = "Powerups:";
                onEvolutionMode = false;
                correctQuestionsToEvolve = 10;
                questionsToEvolveText.text = correctQuestionsToEvolve.ToString() + " to evolve!";
                break;

                case "Enough Time":
                soundPlayer.PlayOneShot(evolvePowerupSound);
                powerupScreenImages[slot].sprite = levelTwoPowerups[6].icon;
                powerupScreenTextBoxes[slot].text = levelTwoPowerups[6].name;
                powerupsInInventory[slot].name = powerupScreenTextBoxes[4].text;
                powerupsInInventory[slot].icon = powerupScreenImages[4].sprite;
                powerupScreenTitleText.text = "Powerups:";
                onEvolutionMode = false;
                correctQuestionsToEvolve = 10;
                questionsToEvolveText.text = correctQuestionsToEvolve.ToString() + " to evolve!";
                break;

                case "Ultra Lifesaver":
                powerupScreenErrorEditableText.text = "You can only evolve level 1 powerups.";
                soundPlayer.PlayOneShot(errorSound);
                StartCoroutine(ShowError(powerupScreenErrorText, 2f));
                powerupScreenTitleText.text = "Powerups:";
                onEvolutionMode = false;
                break;

                case "Boss Protector":
                powerupScreenErrorEditableText.text = "You can only evolve level 1 powerups.";
                soundPlayer.PlayOneShot(errorSound);
                StartCoroutine(ShowError(powerupScreenErrorText, 2f));
                powerupScreenTitleText.text = "Powerups:";
                onEvolutionMode = false;
                break;

                case "Point Doubler":
                powerupScreenErrorEditableText.text = "You can only evolve level 1 powerups.";
                soundPlayer.PlayOneShot(errorSound);
                StartCoroutine(ShowError(powerupScreenErrorText, 2f));
                powerupScreenTitleText.text = "Powerups:";
                onEvolutionMode = false;
                break;

                case "Ultra Pass":
                powerupScreenErrorEditableText.text = "You can only evolve level 1 powerups.";
                soundPlayer.PlayOneShot(errorSound);
                StartCoroutine(ShowError(powerupScreenErrorText, 2f));
                powerupScreenTitleText.text = "Powerups:";
                onEvolutionMode = false;
                break;

                case "Dig Deeper":
                powerupScreenErrorEditableText.text = "You can only evolve level 1 powerups.";
                soundPlayer.PlayOneShot(errorSound);
                StartCoroutine(ShowError(powerupScreenErrorText, 2f));
                powerupScreenTitleText.text = "Powerups:";
                onEvolutionMode = false;
                break;

                case "Half off":
                powerupScreenErrorEditableText.text = "You can only evolve level 1 powerups.";
                soundPlayer.PlayOneShot(errorSound);
                StartCoroutine(ShowError(powerupScreenErrorText, 2f));
                powerupScreenTitleText.text = "Powerups:";
                onEvolutionMode = false;
                break;

                case "Scroll of Time":
                powerupScreenErrorEditableText.text = "You can only evolve level 1 powerups.";
                soundPlayer.PlayOneShot(errorSound);
                StartCoroutine(ShowError(powerupScreenErrorText, 2f));
                powerupScreenTitleText.text = "Powerups:";
                onEvolutionMode = false;
                break;

                default:
                powerupScreenErrorEditableText.text = "There is no powerup in this slot!";
                soundPlayer.PlayOneShot(errorSound);
                StartCoroutine(ShowError(powerupScreenErrorText, 2f));
                powerupScreenTitleText.text = "Powerups:";
                onEvolutionMode = false;
                break;
            }
        }
        else
        {
            UsePowerup(slot);
        }
    }
    public void OpenPowerups()
    {
        if(!usingPowerup)
        {
        if(!powerupScreenOpen)
        {
            soundPlayer.PlayOneShot(openPowerupsSound);
            powerUpScreen.SetActive(true);
            powerupScreenOpen = true;
        } else {
            powerUpScreen.SetActive(false);
            powerupScreenOpen = false;
        }
        } else
        {
            soundPlayer.PlayOneShot(errorSound);
        }
    }

    private IEnumerator ShowGameOverScreen()
    {
        gameMusicPlayer.Stop();
        gameOverText.SetActive(false);
        gameOverScreen.SetActive(true);
        yield return new WaitForSeconds(2f);
        soundPlayer.PlayOneShot(gameOverSound);
        gameOverText.SetActive(true);
        yield return new WaitForSeconds(2f);
        gameOverScreen.SetActive(false);
        titleScreenScript.titleScreen.SetActive(true);
    }

    public void AddPowerupToInventory(int powerupChosen)
    {
        amountOfPowerups += 1;
        if(powerupChosen == 0)
        {
            newPowerup.name = powerupsChosen[0].name;
            newPowerup.icon = powerupsChosen[0].icon;
        } else {
            newPowerup.name = powerupsChosen[1].name;
            newPowerup.icon = powerupsChosen[1].icon;
        }
        for(int i = 0; i < powerupsInInventory.Count; i++)
        {
            if(powerupsInInventory[i].icon == blankPowerupImage)
            {
                powerupsInInventory[i].name = newPowerup.name;
                powerupsInInventory[i].icon = newPowerup.icon;
                break;
            }
        }
        powerupsChosen.Clear();
        powerupSelectionScreen.SetActive(false);
        timerRunning = true;
        soundPlayer.PlayOneShot(claimPowerupSound);
        questionsToPowerupSelect = 5;
        qtcpText.text = questionsToPowerupSelect.ToString();
        foreach(GameObject obj in objectsToDisappearForPowerupScreen)
        {
            obj.SetActive(true); 
        }
        if(endlessActivated)
        {
        questionCode = Random.Range(0, allQuestionsList.Length);
        questionText = allQuestionsList[questionCodeNumber];
        question.StartTyping(questionText);  
        } else
        {
            if(categoryNumber == 0)
            {
                questionCode = Random.Range(0, topicOneQuestionList.Length);
                questionCodeNumber = questionCode;
                questionText = topicOneQuestionList[questionCode];
                question.StartTyping(questionText);
            } else if(categoryNumber == 1)
            {
                questionCode = Random.Range(0, topicTwoQuestionList.Length);
                questionCodeNumber = questionCode + 24;
                questionText = topicTwoQuestionList[questionCode];
                question.StartTyping(questionText);
            } else if(categoryNumber == 2)
            {
                questionCode = Random.Range(0, topicThreeQuestionList.Length);
                questionText = topicThreeQuestionList[questionCode];
                questionCodeNumber = questionCode + 49;
                question.StartTyping(questionText);
            } else if(categoryNumber == 3) 
            {
                questionCode = Random.Range(0, topicFourQuestionList.Length);
                questionCodeNumber = questionCode + 74;
                questionText = topicThreeQuestionList[questionCode];
                question.StartTyping(questionText);
            }
        }
        updatePowerupSlots();
        SetAnswerButtons();
    }

    private IEnumerator ShowError(GameObject objectToUse, float duration)
    {
        objectToUse.SetActive(true);
        yield return new WaitForSeconds(duration);
        objectToUse.SetActive(false);
    }

    private void updatePowerupSlots()
    { 
       for(int i = 0; i < powerupsInInventory.Count; i++)
        {
            if(powerupsInInventory[i].name != "None")
            {
                powerupScreenTextBoxes[i].text = powerupsInInventory[i].name;
                powerupScreenImages[i].sprite = powerupsInInventory[i].icon;
            } 
        }
        if(amountOfPowerups == 5)
        {
            maxPowerupsReachedText.SetActive(true);
        } else
        {
            maxPowerupsReachedText.SetActive(false);
        }
    }

    public void loadCategory(int categoryNum)
    {
        categoryNumber = categoryNum;
        titleScreenScript.titleScreen.SetActive(false);
        categorySelectionScreen.SetActive(false);
        soundPlayer.PlayOneShot(titleScreenScript.menuSelectionSound);
        StartGame();
    }
    public void loadCategoryOne()
    {
        loadCategory(0);
    }
    public void loadCategoryTwo()
    {
        loadCategory(1);
    }
    public void loadCategoryThree()
    {
        loadCategory(2);
    }
    public void loadCategoryFour()
    {
        loadCategory(3);
    }

    public void openEvolutionBooklet()
    {
        soundPlayer.PlayOneShot(evoBookletSound);
        powerupScreenTitleText.text = "Evolution Booklet";
        mainPowerupScreen.SetActive(false);
        evoBookletScreen.SetActive(true);
        powerupScreenErrorText.SetActive(false);
    }

    public void closeEvolutionBooklet()
    {
        soundPlayer.PlayOneShot(evoBookletSound);
        powerupScreenTitleText.text = "Powerups:";
        mainPowerupScreen.SetActive(true);
        evoBookletScreen.SetActive(false);
        powerupScreenErrorText.SetActive(false);
    }

    public void evolvePowerup()
    {
        if(correctQuestionsToEvolve == 0) {
        powerupScreenTitleText.text = "Select powerup to evolve:";
        onEvolutionMode = true;
        } else
        {
            powerupScreenErrorEditableText.text = "Answer more questions to evolve a powerup!";
            soundPlayer.PlayOneShot(errorSound);
            StartCoroutine(ShowError(powerupScreenErrorText, 2f));
        }
    }

    public void returnToTitle()
    {
        resultsScreen.SetActive(false);
        titleScreenScript.titleScreen.SetActive(true);
        gameMusicPlayer.Stop();
        if(endlessActivated)
        {
            levelOnePowerups.Add(pointMultiplierPowerup);
        } else
        {
            levelOnePowerups.Add(bossStopPowerup);
        }
    }
}


[System.Serializable]
public class Powerup
{
    public string name;
    public Sprite icon;
}



