using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class kill : MonoBehaviour
{
    private GameManager _gm;
    // Start is called before the first frame update
    void Start()
    {
        _gm = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    
    void OnTriggerEnter2D(Collider2D other)
	{
		if(other.name == "pacman")
		{
		    _gm.LoseLife();
		}
	}
}
