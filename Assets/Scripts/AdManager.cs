﻿using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour
{
    [SerializeField] string gameID = "33675";
    static bool hasShowed = false;

    void Awake()
    {
        if (!hasShowed)
        {
            DontDestroyOnLoad(gameObject);
            Advertisement.Initialize(gameID, true);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //void Update()
    //{
    //    if (Advertisement.IsReady() && !hasShowed)
    //    {
    //        ShowAd();
    //        hasShowed = true;
    //    }
    //}

    public void ShowAd(string zone = "")
    {
        #if UNITY_EDITOR
                StartCoroutine(WaitForAd());
        #endif

        if (string.Equals(zone, ""))
            zone = null;

        ShowOptions options = new ShowOptions();
        options.resultCallback = AdCallbackhandler;

        if (Advertisement.IsReady(zone))
            Advertisement.Show(zone, options);
    }

    void AdCallbackhandler(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("Ad Finished. Rewarding player...");
                break;
            case ShowResult.Skipped:
                Debug.Log("Ad skipped. Son, I am dissapointed in you");
                break;
            case ShowResult.Failed:
                Debug.Log("I swear this has never happened to me before");
                break;
        }
    }

    IEnumerator WaitForAd()
    {
        float currentTimeScale = Time.timeScale;
        Time.timeScale = 0f;
        yield return null;

        while (Advertisement.isShowing)
            yield return null;

        Time.timeScale = currentTimeScale;
    }
}
