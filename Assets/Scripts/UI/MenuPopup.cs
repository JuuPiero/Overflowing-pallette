using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPopup : MonoBehaviour
{
    public Button continueButton;
    public Button levelsButton;

    public Button quitButton;

    public Canvas levelsCanvas;

    void Start()
    {
        continueButton?.onClick.AddListener(() =>
        {
            SoundManager.Instance?.PlaySound("Click");

            gameObject.SetActive(false);
        });

        levelsButton?.onClick.AddListener(() =>
        {
            SoundManager.Instance?.PlaySound("Click");

            gameObject.SetActive(false);
            levelsCanvas.gameObject.SetActive(true);
        });

        quitButton?.onClick.AddListener(() =>
        {
            SoundManager.Instance?.PlaySound("Click");

            SceneManager.LoadScene("Menu");
        });
    }

}
