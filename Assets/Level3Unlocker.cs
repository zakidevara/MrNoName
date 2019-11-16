using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3Unlocker : MonoBehaviour
{
    // Start is called before the first frame upda
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "pacman" || other.name == "nosis")
        {
            if (!PlayerPrefs.HasKey("LevelsUnlocked"))
            {
                PlayerPrefs.SetInt("LevelsUnlocked", 1);
                PlayerPrefs.Save();
            }
         
                PlayerPrefs.SetInt("LevelsUnlocked", 3);
                PlayerPrefs.Save();

        }
    }
}
