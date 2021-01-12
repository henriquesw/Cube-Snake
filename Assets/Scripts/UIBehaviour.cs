using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIBehaviour : MonoBehaviour {

    public GameObject Hide;
    public GameObject Snake;
    public Animator animator;

    public GameObject tutorialTap;
    public GameObject tutorialSwipe;
    public GameObject pauseScreen;
    public GameObject configScreen;

    void Start ()
    {
        Hide.SetActive(false);
        if (PlayerPrefs.GetInt("HighScore") == 0)
        {
            if (PlayerPrefs.GetInt("Input") == InputType.Tap)
                TutorialTap();
            else if (PlayerPrefs.GetInt("Input") == InputType.Swipe)
                TutorialSwipe();
        }
	}

    public void Lose ()
    {
        Hide.SetActive(true);
        Stats stats = Snake.GetComponent<Stats>();
        int points = PlayerPrefs.GetInt("HighScore");
        if (points < stats.Points)
        {
            points = stats.Points;
            PlayerPrefs.SetInt("HighScore", points);
        }
        GameObject.Find("HighScorePoints").GetComponent<Text>().text = points.ToString("00");

        int coins = 1;
        coins += stats.Points / 10;
        stats.Coins += coins;
        PlayerPrefs.SetInt("Coins", stats.Coins);
        GameObject.Find("Text/Coins/Number").GetComponent<Text>().text = stats.Coins.ToString("000");

        GameObject.Find("Button/Coins/Number").GetComponent<Text>().text = coins.ToString("00");

        animator.SetBool("End", true);
        PlayerPrefs.Save();
    }

    public void Reload ()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void Home()
    {
        SceneManager.LoadScene("Start");
    }

    private void TutorialTap ()
    {
        tutorialTap.SetActive (true);
    }

    private void TutorialSwipe ()
    {
        tutorialSwipe.SetActive(true);
    }

    public void Pause()
    {
        pauseScreen.SetActive(!pauseScreen.activeSelf);
        Snake.GetComponentInChildren<HeadController>().Pause();
    }
}
