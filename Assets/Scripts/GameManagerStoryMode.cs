using System.Collections.Generic;
using UnityEngine;

public class GameManagerStoryMode : MonoBehaviour
{

    //--------------------------------------------------------
    // Game variables

    public static int Level = 0;
    public static int lives = 3;

    public enum GameState { Init, Game, Dead, Scores }
    public static GameState gameState;

    private GameObject pacman;
    private GameGUINavigation gui;

    public static bool scared;
    static public int score;

    public float scareLength;
    private float _timeToCalm;

    public float SpeedPerLevel;

    //-------------------------------------------------------------------
    // singleton implementation
    private static GameManagerStoryMode _instance;

    public static GameManagerStoryMode instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameManagerStoryMode>();
                DontDestroyOnLoad(_instance.gameObject);
            }

            return _instance;
        }
    }

    //-------------------------------------------------------------------
    // function definitions

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            if (this != _instance)
                Destroy(this.gameObject);
        }

        AssignGhosts();
    }

    void Start()
    {
        gameState = GameState.Init;
    }

    void OnLevelWasLoaded()
    {
        if (Level == 0) lives = 3;

        Debug.Log("Level " + Level + " Loaded!");
        ResetVariables();


        // Adjust Ghost variables!
        pacman.GetComponent<PlayerControllerStoryMode>().speed += Level * SpeedPerLevel / 2;
    }

    private void ResetVariables()
    {
        _timeToCalm = 0.0f;
        scared = false;
        PlayerControllerStoryMode.killstreak = 0;
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    public void ResetScene()
    {
        CalmGhosts();

        pacman.GetComponent<PlayerControllerStoryMode>().ResetDestination(0,0);

        gameState = GameState.Init;
        gui.H_ShowReadyScreen();

    }

    public void ToggleScare()
    {
        if (!scared) ScareGhosts();
        else CalmGhosts();
    }

    public void ScareGhosts()
    {
        scared = true;
        _timeToCalm = Time.time + scareLength;

        Debug.Log("Ghosts Scared");
    }

    public void CalmGhosts()
    {
        scared = false;
        PlayerControllerStoryMode.killstreak = 0;
    }

    void AssignGhosts()
    {
        // find and assign ghosts
        pacman = GameObject.Find("pacman");

        
        if (pacman == null) Debug.Log("Pacman is NULL");

        gui = GameObject.FindObjectOfType<GameGUINavigation>();

        if (gui == null) Debug.Log("GUI Handle Null!");

    }

    public void LoseLife()
    {
        lives--;
        gameState = GameState.Dead;

        // update UI too
        UIScript ui = GameObject.FindObjectOfType<UIScript>();
        Destroy(ui.lives[ui.lives.Count - 1]);
        ui.lives.RemoveAt(ui.lives.Count - 1);
    }

    public static void DestroySelf()
    {

        score = 0;
        Level = 0;
        lives = 3;
        Destroy(GameObject.Find("Game Manager"));
    }
}
