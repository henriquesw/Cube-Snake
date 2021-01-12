using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SkinManager : MonoBehaviour {

    public GameObject Head;

    //[HideInInspector]
    [Range(0, 3)]
    public int selected = 0;
    
    public Skin[] skins;

    void Start()
    {
        Repaint();
    }

    public void Repaint()
    {
        Reset();
        Head.GetComponent<Renderer>().material = NextMaterial();
        GameObject tail = Head;
        while (tail.GetComponent<TailController>().Tail != null)
        {
            tail = tail.GetComponent<TailController>().Tail;
            tail.GetComponent<Renderer>().material = NextMaterial();
        }
    }

    public void SetSelected(int id)
    {
        selected = id;
    }

    public void Reset()
    {
        skins[selected].Reset();
    }

    public Material NextMaterial()
    {
        return skins[selected].NextMaterial();
    }

}
