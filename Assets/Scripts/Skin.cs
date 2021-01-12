using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Skin {
    public string name;
    public Material[] skin;
    public int price;

    private int position = 0;
    
    public Material NextMaterial()
    {
        Material temp = skin[position];
        position++;
        position = position % skin.Length;
        return temp;
    }

    public void Reset()
    {
        position = 0;
    }
}
