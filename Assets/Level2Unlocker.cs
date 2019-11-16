using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Unlocker : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "pacman" || other.name == "nosis")
        {
            if (!PlayerPrefs.HasKey("LevelsUnlocked"))
            {
                PlayerPrefs.SetInt("LevelsUnlocked", 1);
                PlayerPrefs.Save();
            }

            
                PlayerPrefs.SetInt("LevelsUnlocked", 2);
                PlayerPrefs.Save();
            
        }
    }

}
