using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class highscoreTable : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    private List<HighscoreEntry> highscoreEntryList;
    private List<Transform> highscoreEntryTransformList;
    public int length;

    private void Awake()
    {
        entryContainer = transform.Find("highscoreEntryContainer");
        entryTemplate = entryContainer.Find("highscoreEntryTemplate");
        entryTemplate.gameObject.SetActive(false);

        // AddHighscoreEntry(2322, "Zaki");
        if (!PlayerPrefs.HasKey("highscoreTable")) {
            highscoreEntryList = new List<HighscoreEntry>() {
                new HighscoreEntry{ score = 0, name = "default" },
            };
            Highscores hs = new Highscores { highscoreEntryList = highscoreEntryList  };
            string json = JsonUtility.ToJson(hs);
            PlayerPrefs.SetString("highscoreTable", json);
            PlayerPrefs.Save();
        }
        string jsonString  = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        length = highscores.highscoreEntryList.Count;
        // Bubble Sort Score List
        int swapCount = 1;
        while (swapCount > 0) {
            swapCount = 0;
            for (int i = 0; i < highscores.highscoreEntryList.Count - 1; i++) {
                if (highscores.highscoreEntryList[i].score < highscores.highscoreEntryList[i + 1].score) {
                    //swap
                    HighscoreEntry temp = highscores.highscoreEntryList[i];
                    highscores.highscoreEntryList[i] = highscores.highscoreEntryList[i + 1];
                    highscores.highscoreEntryList[i + 1] = temp;
                    swapCount++;
                }
            }

        }

        highscoreEntryTransformList = new List<Transform>();

        int count = 1;
        foreach (HighscoreEntry item in highscores.highscoreEntryList) {
            CreateHighscoreEntryTransform(item, entryContainer, highscoreEntryTransformList);
            count++;
            if (count > 10) break;
            
        }
    }

    private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList) {
        float templateHeight = 2f;
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * (transformList.Count + 1));
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString = rank.ToString();
        entryTransform.Find("posText").GetComponent<Text>().text = rankString;

        int score = highscoreEntry.score;
        entryTransform.Find("scoreText").GetComponent<Text>().text = score.ToString();

        string name = highscoreEntry.name;
        entryTransform.Find("nameText").GetComponent<Text>().text = name;

        transformList.Add(entryTransform);
    }



    public void AddHighscoreEntry(int score, string name) {
        //Create highscore entry
        HighscoreEntry highscoreEntry = new HighscoreEntry { score = score, name = name };

        //Load saved highscores
        if (!PlayerPrefs.HasKey("highscoreTable"))
        {
            highscoreEntryList = new List<HighscoreEntry>() {
                new HighscoreEntry{ score = 0, name = "default" },
            };
            Highscores hs = new Highscores { highscoreEntryList = highscoreEntryList };
            string jsonStr = JsonUtility.ToJson(hs);
            PlayerPrefs.SetString("highscoreTable", jsonStr);
            PlayerPrefs.Save();
        }

        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        

        // Add new entry to Highscores
        highscores.highscoreEntryList.Add(highscoreEntry);

        //save updated highscores
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
    }

    public int lowestHighscores() {
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        // Bubble Sort Score List
        int swapCount = 1;
        while (swapCount > 0)
        {
            swapCount = 0;
            for (int i = 0; i < highscores.highscoreEntryList.Count - 1; i++)
            {
                if (highscores.highscoreEntryList[i].score < highscores.highscoreEntryList[i + 1].score)
                {
                    //swap
                    HighscoreEntry temp = highscores.highscoreEntryList[i];
                    highscores.highscoreEntryList[i] = highscores.highscoreEntryList[i + 1];
                    highscores.highscoreEntryList[i + 1] = temp;
                    swapCount++;
                }
            }

        }

        if (highscores.highscoreEntryList.Count < 10)
        {
            return highscores.highscoreEntryList[highscores.highscoreEntryList.Count - 1].score;
        }
        else
        {
            return highscores.highscoreEntryList[9].score;
        }
    }

    public int highest()
    {
        if (!PlayerPrefs.HasKey("highscoreTable"))
        {
            return 0;
        }
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        // Bubble Sort Score List
        int swapCount = 1;
        while (swapCount > 0)
        {
            swapCount = 0;
            for (int i = 0; i < highscores.highscoreEntryList.Count - 1; i++)
            {
                if (highscores.highscoreEntryList[i].score < highscores.highscoreEntryList[i + 1].score)
                {
                    //swap
                    HighscoreEntry temp = highscores.highscoreEntryList[i];
                    highscores.highscoreEntryList[i] = highscores.highscoreEntryList[i + 1];
                    highscores.highscoreEntryList[i + 1] = temp;
                    swapCount++;
                }
            }

        }

        
        
        return highscores.highscoreEntryList[0].score;
        
    }


    private class Highscores
    {
        public List<HighscoreEntry> highscoreEntryList;
    }
    /*
     * Represents a single High Score Entry
     * */
    [System.Serializable]
   
    private class HighscoreEntry {
        public int score;
        public string name;
    }
}
