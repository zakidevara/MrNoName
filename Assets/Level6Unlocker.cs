using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level6Unlocker : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "pacman" || other.name == "nosis")
        {
            if (!PlayerPrefs.HasKey("LevelsUnlocked"))
            {
                PlayerPrefs.SetInt("LevelsUnlocked", 1);
                PlayerPrefs.Save();
            }else if(PlayerPrefs.GetInt("LevelsUnlocked") < 6){
                PlayerPrefs.SetInt("LevelsUnlocked", 6);
                PlayerPrefs.Save();
            }
        }
    }
}

