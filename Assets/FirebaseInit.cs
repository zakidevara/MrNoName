using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Analytics;
using Proyecto26;
using System;

public class FirebaseInit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith( task =>
       {
           FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
       });
    }

    public void Login() {
        Firebase.Analytics.FirebaseAnalytics
        .LogEvent(Firebase.Analytics.FirebaseAnalytics.EventLogin, "time_logged_in", DateTime.Now.ToString());
    }
    public void Logout()
    {
        Firebase.Analytics.FirebaseAnalytics
        .LogEvent("logout", "time_logged_out", DateTime.Now.ToString());
    }

    public void PostToDatabase(UserAvgSession userAvgSession) {
        RestClient.Post("https://brorun-horror-adventure-8806.firebaseio.com/" + userAvgSession.getUsername + "/.json", userAvgSession);
    }

}


