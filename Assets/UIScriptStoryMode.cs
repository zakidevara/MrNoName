using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScriptStoryMode : MonoBehaviour
{
    public List<Image> lives = new List<Image>(3);
    

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < 3 - GameManager.lives; i++)
        {
            Destroy(lives[lives.Count - 1]);
            lives.RemoveAt(lives.Count - 1);
        }
    }


}
