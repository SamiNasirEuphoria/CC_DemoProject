using System;
using System.Collections;
using System.Collections.Generic;
using GameVanilla.Game.Common;
using UnityEngine;
using UnityEngine.UI;

public class Implementation : MonoBehaviour
{
    public static Implementation Instance { set; get; }
    private static int count;

    /// <summary>
    /// Initialize the ads
    /// </summary>
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(this.gameObject);
        
    }


    void Start()
    {
        count = 0;
        Advertisements.Instance.Initialize();
    }

    /// <summary>
    /// Show banner, assigned from inspector
    /// </summary>
    public void ShowBanner()
    {
        Advertisements.Instance.ShowBanner(BannerPosition.BOTTOM);
    }

    public void HideBanner()
    {
        Advertisements.Instance.HideBanner();
    }


    /// <summary>
    /// Show Interstitial, assigned from inspector
    /// </summary>
    public void ShowInterstitial()
    {
        Advertisements.Instance.ShowInterstitial();
    }

    /// <summary>
    /// Show rewarded video, assigned from inspector
    /// </summary>
    public void ShowRewardedVideo()
    {
        Advertisements.Instance.ShowRewardedVideo(CompleteMethod);
    }


    /// <summary>
    /// This is for testing purpose
    /// </summary>
    void Update()
    {
        
    }

    private void CompleteMethod(bool completed)
    {
        if (completed)
        {
            if (count < 1)
            {
                int coinAmount = PlayerPrefs.GetInt("num_coins");
                coinAmount += 50;
                PlayerPrefs.SetInt("num_coins", coinAmount);
                count++;
            }
            else if (count >=1)
            {

                Debug.Log("Booster will be added as gift");
                var playerPrefsKey = string.Format("num_boosters_{0}", 1);
                var numBoosters = PlayerPrefs.GetInt(playerPrefsKey);
                numBoosters += 1;
                PlayerPrefs.SetInt(playerPrefsKey, numBoosters);
                Debug.Log($"Here is the booster number {playerPrefsKey} & numbers won is {numBoosters}");
            }
            else if (count >=2)
            {

                Debug.Log("Booster will be added as gift");
                var playerPrefsKey = string.Format("num_boosters_{0}", 0);
                var numBoosters = PlayerPrefs.GetInt(playerPrefsKey);
                numBoosters += 1;
                PlayerPrefs.SetInt(playerPrefsKey, numBoosters);
                Debug.Log($"Here is the booster number {playerPrefsKey} & numbers won is {numBoosters}");
                count = 0;
            }
            
        }
    }
}
