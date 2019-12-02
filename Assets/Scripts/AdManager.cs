using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdManager : MonoBehaviour
{
    private string APP_ID = "ca-app-pub-6247336146924124~5686950497";
    private InterstitialAd interstitial;
    private BannerView banner;

    // Start is called before the first frame update
    void Start()
    {
        //when published
        //MobileAds.Initialize(APP_ID);
        MobileAds.Initialize(initStatus => { });
        RequestBanner();
        RequestInterstitial();
    }
    

    private void RequestInterstitial()
    {
        #if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/1033173712";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(adUnitId);

        // Called when an ad request has successfully loaded.
        this.interstitial.OnAdLoaded += HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when an ad is shown.
        this.interstitial.OnAdOpening += HandleOnAdOpened;
        // Called when the ad is closed.
        this.interstitial.OnAdClosed += HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        this.interstitial.OnAdLeavingApplication += HandleOnAdLeavingApplication;

        // Create an empty ad request. FOR REAL APP
        // AdRequest request = new AdRequest.Builder().Build();

        // Create an empty ad request. FOR TESTING
        AdRequest request = new AdRequest.Builder().AddTestDevice("d797d3b07cf5").Build();
        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);

        void HandleOnAdLoaded(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleAdLoaded event received");
        }

        void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
        {
            MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
                                + args.Message);
        }

        void HandleOnAdOpened(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleAdOpened event received");
        }

        void HandleOnAdClosed(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleAdClosed event received");
        }

        void HandleOnAdLeavingApplication(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleAdLeavingApplication event received");
        }
    }

    private void RequestBanner()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/6300978111";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Initialize an bannerAd.
        this.banner = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);

        // Called when an ad request has successfully loaded.
        this.banner.OnAdLoaded += HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.banner.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when an ad is shown.
        this.banner.OnAdOpening += HandleOnAdOpened;
        // Called when the ad is closed.
        this.banner.OnAdClosed += HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        this.banner.OnAdLeavingApplication += HandleOnAdLeavingApplication;

        // Create an empty ad request. FOR REAL APP
        // AdRequest request = new AdRequest.Builder().Build();

        // Create an empty ad request. FOR TESTING
        AdRequest request = new AdRequest.Builder().AddTestDevice("d797d3b07cf5").Build();
        // Load the banner with the request.
        this.banner.LoadAd(request);

        void HandleOnAdLoaded(object sender, EventArgs args)
        {
            banner.Show();
        }

        void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
        {
            RequestBanner();
        }

        void HandleOnAdOpened(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleAdOpened event received");
        }

        void HandleOnAdClosed(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleAdClosed event received");
        }

        void HandleOnAdLeavingApplication(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleAdLeavingApplication event received");
        }
    }

    public void showInterstitial() {
        if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
        }
    }
    public void showBanner() {


        this.banner.Show();
        
    }
}
