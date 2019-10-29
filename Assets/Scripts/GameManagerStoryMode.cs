﻿using System.Collections.Generic;
using UnityEngine;

public class GameManagerStoryMode : MonoBehaviour {

    //--------------------------------------------------------
    // Game variables

    public static int Level = 0;
    public static int lives = 3;
    public float respawnX;
    public float respawnY;

	public enum GameState { Init, Game, Dead, Scores }
	public static GameState gameState;

    public GameObject pacman;
    private GameObject blinky;
    private GameObject pinky;
    private GameObject inky;
    private GameObject clyde;
    private GameGUINavigationStory gui;

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
            if(this != _instance)   
                Destroy(this.gameObject);
        }

        AssignGhosts();
    }

	void Start () 
	{
		gameState = GameState.Init;
	}

    void OnLevelWasLoaded()
    {
        if (Level == 0) lives = 3;

        Debug.Log("Level " + Level + " Loaded!");
        AssignGhosts();
        ResetVariables();


        // Adjust Ghost variables!
        //clyde.GetComponent<GhostMove>().speed += Level * SpeedPerLevel;
        //blinky.GetComponent<GhostMove>().speed += Level * SpeedPerLevel;
        //pinky.GetComponent<GhostMove>().speed += Level * SpeedPerLevel;
        //inky.GetComponent<GhostMove>().speed += Level * SpeedPerLevel;
        //pacman.GetComponent<PlayerController>().speed += Level*SpeedPerLevel/2;
    }

    private void ResetVariables()
    {
        _timeToCalm = 0.0f;
        scared = false;
        PlayerController.killstreak = 0;
    }

    // Update is called once per frame
	void Update () 
	{
		if(scared && _timeToCalm <= Time.time)
			CalmGhosts();

	}

	public void ResetScene()
	{
        //CalmGhosts();
        enableEnemyMovements();
        enableTrapMovements();
        pacman.transform.position = new Vector3(respawnX, respawnY, 0f);
		/*blinky.transform.position = new Vector3(15f, 20f, 0f);
		pinky.transform.position = new Vector3(14.5f, 17f, 0f);
		inky.transform.position = new Vector3(16.5f, 17f, 0f);
		clyde.transform.position = new Vector3(12.5f, 17f, 0f);*/

		pacman.GetComponent<PlayerControllerStoryMode>().ResetDestination(respawnX, respawnY);
		/*blinky.GetComponent<GhostMove>().InitializeGhost();
		pinky.GetComponent<GhostMove>().InitializeGhost();
		inky.GetComponent<GhostMove>().InitializeGhost();
		clyde.GetComponent<GhostMove>().InitializeGhost();*/

        gameState = GameState.Init;  
        gui.H_ShowReadyScreen();

	}

	public void ToggleScare()
	{
		if(!scared)	ScareGhosts();
		else 		CalmGhosts();
	}

	public void ScareGhosts()
	{
		scared = true;
		blinky.GetComponent<GhostMove>().Frighten();
		pinky.GetComponent<GhostMove>().Frighten();
		inky.GetComponent<GhostMove>().Frighten();
		clyde.GetComponent<GhostMove>().Frighten();
		_timeToCalm = Time.time + scareLength;

        Debug.Log("Ghosts Scared");
	}

	public void CalmGhosts()
	{
		scared = false;
		blinky.GetComponent<GhostMove>().Calm();
		pinky.GetComponent<GhostMove>().Calm();
		inky.GetComponent<GhostMove>().Calm();
		clyde.GetComponent<GhostMove>().Calm();
	    PlayerController.killstreak = 0;
    }
    
    public void disableEnemyMovements() {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject e in enemies)
        {
            if(e.GetComponent<Patrol>() != null)
                e.GetComponent<Patrol>().enabled = false;
            if(e.GetComponent<BabyBroMove>() != null)
                e.GetComponent<BabyBroMove>().enabled = false;
            if(e.GetComponent<patrolBigBro>() != null)
                e.GetComponent<patrolBigBro>().enabled = false;
        }
    }
    public void disableTrapMovements()
    {
        GameObject[] traps = GameObject.FindGameObjectsWithTag("Trap");
        foreach (GameObject e in traps)
        {
            if(e.GetComponent<trapmovement>() != null)
                e.GetComponent<trapmovement>().enabled = false;
        }
    }
    public void enableEnemyMovements()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject e in enemies)
        {
            if (e.GetComponent<Patrol>() != null)
                e.GetComponent<Patrol>().enabled = true;
            if (e.GetComponent<BabyBroMove>() != null)
                e.GetComponent<BabyBroMove>().enabled = true;
            if (e.GetComponent<patrolBigBro>() != null)
                e.GetComponent<patrolBigBro>().enabled = true;
        }
    }
    public void enableTrapMovements()
    {
        GameObject[] traps = GameObject.FindGameObjectsWithTag("Trap");
        foreach (GameObject e in traps)
        {
            if(e.GetComponent<trapmovement>() != null)
                e.GetComponent<trapmovement>().enabled = true;
        }
    }
    void AssignGhosts()
    {
        // find and assign ghosts
        //clyde = GameObject.Find("clyde");
        //pinky = GameObject.Find("pinky");
        //inky = GameObject.Find("inky");
        //blinky = GameObject.Find("blinky");
        pacman = GameObject.Find("pacman");
        if (pacman == null) {
            pacman = GameObject.Find("nosis");
        }

        

        gui = GameObject.FindObjectOfType<GameGUINavigationStory>();

        if(gui == null) Debug.Log("GUI Handle Null!");

    }

    public void LoseLife()
    {
        lives--;
        gameState = GameState.Dead;
    
        // update UI too
        UIScriptStoryMode ui = GameObject.FindObjectOfType<UIScriptStoryMode>();
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
