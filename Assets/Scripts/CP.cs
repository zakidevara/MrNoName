using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CP : MonoBehaviour
{
    [SerializeField]
    private Transform transform;
    // script handles
    private GameGUINavigationStory GUINav;
    private GameManagerStoryMode GM;

    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.Find("Game Manager").GetComponent<GameManagerStoryMode>();
    }

    void OnTriggerEnter2D(Collider2D other)
	{
		if(other.name == "pacman" || other.name == "nosis")
		{
            GM.respawnX=transform.position.x;
            GM.respawnY=transform.position.y;
		}
	}
}
