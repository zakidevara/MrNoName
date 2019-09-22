using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class nextLevel : MonoBehaviour
{
    private int nextSceneToLoad;
    private GameManager _gm;
    // Start is called before the first frame update
    void Start()
    {
       nextSceneToLoad = SceneManager.GetActiveScene().buildIndex + 1 ; 
    }

    void OnTriggerEnter2D(Collider2D other)
	{
		if(other.name == "pacman")
		{
		    SceneManager.LoadScene(nextSceneToLoad);
		}
	}
}
