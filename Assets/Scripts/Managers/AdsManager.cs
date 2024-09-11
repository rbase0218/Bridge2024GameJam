using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdsManager : MonoBehaviour
{
#if UNITY_ANDROID
    readonly string adUnitId = "ca-app-pub-3940256099942544/1033173712";
#else
readonly string adUnitId = "unexpected_platform";
#endif
    
    private InterstitialAd interstitialAd;

    private void Awake()
    {
        MobileAds.Initialize(status => {});
    }

    private void Start()
    {
        LoadInterstitialAd();
    }

    private void LoadInterstitialAd()
    {
        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
            interstitialAd = null;
        }

        var adRequest = new AdRequest();
        
        InterstitialAd.Load(adUnitId, adRequest, (InterstitialAd ad, LoadAdError error) =>
        {
            if (error != null || ad == null)
            {
                Debug.LogError("광고 로드 실패 error명 : " + error);
                return;
            }
            
            Debug.Log("로드 성공 " + ad.GetResponseInfo());

            interstitialAd = ad;
            
            RegisterEventHandlers(interstitialAd);
        });
    }

    private void RegisterEventHandlers(InterstitialAd interstitialAd)
    {
        // 광고 지급 관련 이벤트
        interstitialAd.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log($"Interstitial ad paid {adValue.Value} {adValue.CurrencyCode}.");
        };

        // 광고 노출 기록 이벤트
        interstitialAd.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Interstitial ad recorded an impression.");
        };

        // 광고가 클릭되었을 때 이벤트
        interstitialAd.OnAdClicked += () =>
        {
            Debug.Log("Interstitial ad was clicked.");
        };

        // 전면 광고가 열렸을 때 호출
        interstitialAd.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Interstitial ad full screen content opened.");
        };

        // 전면 광고가 닫혔을 때 호출
        interstitialAd.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("close Scene");
        };

        // 전면 광고가 열리지 못했을 때 호출
        interstitialAd.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Interstitial ad failed to open full screen content with error: " + error);
        };
    }
    
    public void ShowAd()
    {
        if (interstitialAd != null && interstitialAd.CanShowAd())
        {
            Debug.Log("전면광고 띄우기");
            interstitialAd.Show();
        }
        else
        {
            LoadInterstitialAd();
            Debug.LogError("광고가 준비되어 있지 않습니다.");
        }
    }
}
