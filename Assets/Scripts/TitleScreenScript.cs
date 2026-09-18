using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScreenScript : MonoBehaviour
{
    public GameControls gameController;
    [Header("Mode Selection")]
    public Button normalModeButton;
    public Button endlessModeButton;
    public AudioSource menuSoundPlayer;
    public GameObject titleScreen;
    public AudioClip menuSelectionSound;

    void Start()
    {   
        normalModeButton.onClick.AddListener(normalModeSelected);
        endlessModeButton.onClick.AddListener(endlessModeSelected);
    }

    private void normalModeSelected()
    {
        menuSoundPlayer.PlayOneShot(menuSelectionSound);
        GameControls.endlessActivated = false;
        gameController.categorySelectionScreen.SetActive(true);
    }

    private void endlessModeSelected()
    {
        menuSoundPlayer.PlayOneShot(menuSelectionSound);
        GameControls.endlessActivated = true;
        titleScreen.SetActive(false);
        gameController.StartGame();
    }
}
