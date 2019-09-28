using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BabyBroMove : MonoBehaviour
{

    private GameManagerStoryMode _gm;

    //-----------------------------------------------------------------------------------------
    // variables end, functions begin
    void Start()
    {
        _gm = GameObject.Find("Game Manager").GetComponent<GameManagerStoryMode>();
    }



    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "pacman" || other.name == "nosis")
        {

            _gm.LoseLife();


        }
    }
}




