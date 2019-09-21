using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BabyBroMove : MonoBehaviour
{

    private GameManager _gm;

    //-----------------------------------------------------------------------------------------
    // variables end, functions begin
    void Start()
    {
        _gm = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }



    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "pacman")
        {

            _gm.LoseLife();


        }
    }
}




