using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfigScreen : MonoBehaviour {

    public Toggle toggle1;
    public Toggle toggle2;

    void Start () {
        switch (PlayerPrefs.GetInt("Input"))
        {
            case 0:
                PlayerPrefs.SetInt("Input", 1);
                toggle1.isOn = true;
                toggle2.isOn = false;
                break;
            case 1:
                toggle1.isOn = true;
                toggle2.isOn = false;
                break;
            case 2:
                toggle1.isOn = false;
                toggle2.isOn = true;
                break;
        }
    }

    public void SetInput(int input)
    {
        PlayerPrefs.SetInt("Input", input);
    }
}
