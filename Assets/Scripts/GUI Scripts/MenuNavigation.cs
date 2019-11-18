using UnityEngine;
using System.Collections;
using System;

public class MenuNavigation : MonoBehaviour {
    public FirebaseInit firebase;

    public void Start()
    {
        firebase = GameObject.Find("FireBase").GetComponent<FirebaseInit>();
    }
    public void MainMenu()
	{
		Application.LoadLevel("menu");
	}

	public void Quit()
	{
        firebase.PostToDatabase(new UserAvgSession());
        firebase.Logout();
		Application.Quit();
	}
	
	public void Play()
	{
        Firebase.Analytics.FirebaseAnalytics
        .LogEvent("playing_classic_mode", "time_started_playing", DateTime.Now.ToString());
        Application.LoadLevel("game");
	}
	
	public void HighScores()
	{
		Application.LoadLevel("scores");
		
	}

    public void Credits()
    {
        Application.LoadLevel("credits");
    }

	public void SourceCode()
	{
		Application.OpenURL("https://github.com/vilbeyli/Pacman-Clone/");
	}

    public void LevelSelector() {
        Application.LoadLevel("level_selector");
    }
}
