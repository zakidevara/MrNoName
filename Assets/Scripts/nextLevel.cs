using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;
using System;

public class nextLevel : MonoBehaviour
{
    private static int MAX_LEVEL = 3;
    private int nextSceneToLoad;
    private GameManagerStoryMode _gm;
    private InterstitialAd interstitial;

    // Start is called before the first frame update
    void Start()
    {
        nextSceneToLoad = SceneManager.GetActiveScene().buildIndex + 1 ;
        _gm = GameObject.Find("Game Manager").GetComponent<GameManagerStoryMode>();
        
    }

    void OnTriggerEnter2D(Collider2D other)
	{
		if(other.name == "pacman"||other.name == "nosis")
		{
            _gm.respawnX=-0.479f;
            _gm.respawnY=0.461f;
            SceneManager.LoadScene(nextSceneToLoad);
		}
	}
    
}
