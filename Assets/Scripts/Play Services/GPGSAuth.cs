using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System;

public class GPGSAuth : MonoBehaviour
{
    public static PlayGamesPlatform platform;
    public FirebaseInit firebase;
    // Start is called before the first frame update
    void Start()
    {

        firebase = GameObject.Find("FireBase").GetComponent<FirebaseInit>();
        if (platform == null) {

            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
            PlayGamesPlatform.InitializeInstance(config);
            PlayGamesPlatform.DebugLogEnabled = true;
            platform = PlayGamesPlatform.Activate();
        }


        if (!Social.localUser.authenticated)
        {
            Social.localUser.Authenticate((bool success, string err) =>
            {
                if (success)
                {
                    firebase.Login();
                    firebase.PostToDatabase(new UserAvgSession());
                }
            });

        }
        
    }

}
