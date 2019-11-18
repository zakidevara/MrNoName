using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level5Unlocker : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "pacman" || other.name == "nosis")
        {
            if (!PlayerPrefs.HasKey("LevelsUnlocked"))
            {
                PlayerPrefs.SetInt("LevelsUnlocked", 1);
                PlayerPrefs.Save();
            }else if(PlayerPrefs.GetInt("LevelsUnlocked") < 5){
                PlayerPrefs.SetInt("LevelsUnlocked", 5);
                PlayerPrefs.Save();
            }
        }
    }
}
