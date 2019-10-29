using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUnlocker : MonoBehaviour
{
    private int levelsUnlocked;
    private Button[] buttons;
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("LevelsUnlocked"))
        {
            PlayerPrefs.SetInt("LevelsUnlocked", 1);
            PlayerPrefs.Save();
        }
        levelsUnlocked = PlayerPrefs.GetInt("LevelsUnlocked");
        GameObject levelButtons = GameObject.Find("Level Buttons");
        buttons = levelButtons.GetComponentsInChildren<Button>();     

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < levelsUnlocked; i++) {
            buttons[i].interactable = true;
        }
    }
}
