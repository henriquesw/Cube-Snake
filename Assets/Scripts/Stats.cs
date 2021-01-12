using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour {

    public float Speed;
    public float Acceleration;
    public int Points;
    public int Coins;

    void Start()
    {
        Coins = PlayerPrefs.GetInt("Coins");
        if(PlayerPrefs.GetInt("Mute") == 1)
        {
            AudioListener.pause = true;
        }
    }

    void Update()
    {
        GameObject.Find("Points").GetComponent<Text>().text = Points.ToString("00");
    }
}
