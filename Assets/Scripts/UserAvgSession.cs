using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;

public class UserAvgSession
{
    private string id;
    private string Username;
    private float SessionAvgTime;
    public string getUsername
    {
        get { return Username; }
    }
    public UserAvgSession() {
        ((PlayGamesLocalUser)Social.localUser).GetStats((rc, stats) =>
        {
            
            SessionAvgTime = stats.AvgSessionLength;
            Username = Social.localUser.userName;
            id = Social.localUser.id;
            
        });
    }
    public UserAvgSession(float sessionTime, string uName, string uID)
    {     
        
        SessionAvgTime = sessionTime;
        Username = uName;
        id = uID;
        
    }

}
