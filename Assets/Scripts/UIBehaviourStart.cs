using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBehaviourStart : MonoBehaviour {

    public void Start()
    {
        int coins = PlayerPrefs.GetInt("Coins");
        GameObject.Find("Coins/Number").GetComponent<Text>().text = coins.ToString("000");
        int points = PlayerPrefs.GetInt("HighScore");
        if (points != 0)
            GameObject.Find("Points").GetComponent<Text>().text = points.ToString("00");
        else
        {
            GameObject.Find("Points").SetActive(false);
            GameObject.Find("HighScore").SetActive(false);
            PlayerPrefs.SetInt("Input", InputType.Tap);
            PlayerPrefs.SetInt("Coins", 0);
            PlayerPrefs.SetInt("TimesPlayed", 0);
        }
    }

    public void StartGame()
    {
        Initiate.Fade("Main", Color.black, 3.0f);
    }

    public void SetMute(Toggle toggle)
    {
        if(toggle.isOn)
            PlayerPrefs.SetInt("Mute", 1);
        else
            PlayerPrefs.SetInt("Mute", 0);
    }
    
}
