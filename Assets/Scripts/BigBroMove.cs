using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BigBroMove : MonoBehaviour
{
    

    public float speed = 0.3f;
    

    enum State { Wait, Init, Scatter, Chase, Run };
    State state;

    public Vector3 _startPos;
    private float _timeToWhite;
    private float _timeToToggleWhite;
    private float _toggleInterval;
    private bool isWhite = false;

    // handles
    public GameGUINavigation GUINav;
    public PlayerController pacman;
    private GameManagerStoryMode _gm;

    //-----------------------------------------------------------------------------------------
    // variables end, functions begin
    void Start()
    {
        _gm = GameObject.Find("Game Manager").GetComponent<GameManagerStoryMode>();

        InitializeGhost();
    }

    public float DISTANCE;

    void FixedUpdate()
    {
        DISTANCE = Vector3.Distance(transform.position, waypoint);

        if (GameManager.gameState == GameManager.GameState.Game)
        {
            animate();

            switch (state)
            {
                case State.Wait:
                    Wait();
                    break;

                case State.Init:
                    Init();
                    break;

                case State.Scatter:
                    Scatter();
                    break;

                case State.Chase:
                    ChaseAI();
                    break;

                case State.Run:
                    RunAway();
                    break;
            }
        }
    }

    //-----------------------------------------------------------------------------------
    // Start() functions

    public void InitializeGhost()
    {     
        state = State.Wait;
        timeToEndWait = Time.time + waitLength + GUINav.initialDelay;
        //InitializeWaypoints(state);
    }

    public void InitializeGhost(Vector3 pos)
    {
        transform.position = pos;
        waypoint = transform.position;	// to avoid flickering animation
        state = State.Wait;
        timeToEndWait = Time.time + waitLength + GUINav.initialDelay;
        InitializeWaypoints(state);
    }


    

    //------------------------------------------------------------------------------------
    // Update functions
    void animate()
    {
        Vector3 dir = waypoint - transform.position;
        GetComponent<Animator>().SetFloat("DirX", dir.x);
        GetComponent<Animator>().SetFloat("DirY", dir.y);
        GetComponent<Animator>().SetBool("Run", false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "pacman")
        {
            
            _gm.LoseLife();
            

        }
    }

    //-----------------------------------------------------------------------------------
    // State functions
    void Wait()
    {
        if (Time.time >= timeToEndWait)
        {
            state = State.Init;
            waypoints.Clear();
        }

        // get the next waypoint and move towards it
        MoveToWaypoint(true);
    }

    void Init()
    {
        _timeToWhite = 0;

        // if the Queue is cleared, do some clean up and change the state
        if (waypoints.Count == 0)
        {
            state = State.Scatter;

            //get direction according to sprite name
            string name = GetComponent<SpriteRenderer>().sprite.name;
            if (name[name.Length - 1] == '0' || name[name.Length - 1] == '1') direction = Vector3.right;
            if (name[name.Length - 1] == '2' || name[name.Length - 1] == '3') direction = Vector3.left;
            if (name[name.Length - 1] == '4' || name[name.Length - 1] == '5') direction = Vector3.up;
            if (name[name.Length - 1] == '6' || name[name.Length - 1] == '7') direction = Vector3.down;

            InitializeWaypoints(state);
            timeToEndScatter = Time.time + scatterLength;

            return;
        }

        // get the next waypoint and move towards it
        MoveToWaypoint();
    }

    void Scatter()
    {
        if (Time.time >= timeToEndScatter)
        {
            waypoints.Clear();
            state = State.Chase;
            return;
        }

        // get the next waypoint and move towards it
        MoveToWaypoint(true);

    }

    void ChaseAI()
    {

        // if not at waypoint, move towards it
        if (Vector3.Distance(transform.position, waypoint) > 0.000000000001)
        {
            Vector2 p = Vector2.MoveTowards(transform.position, waypoint, speed);
            GetComponent<Rigidbody2D>().MovePosition(p);
        }

        // if at waypoint, run AI module
        else GetComponent<AI>().AILogic();

    }

    void RunAway()
    {
        GetComponent<Animator>().SetBool("Run", true);

        if (Time.time >= _timeToWhite && Time.time >= _timeToToggleWhite) ToggleBlueWhite();

        // if not at waypoint, move towards it
        if (Vector3.Distance(transform.position, waypoint) > 0.000000000001)
        {
            Vector2 p = Vector2.MoveTowards(transform.position, waypoint, speed);
            GetComponent<Rigidbody2D>().MovePosition(p);
        }

        // if at waypoint, run AI run away logic
        else GetComponent<AI>().RunLogic();

    }

    //------------------------------------------------------------------------------
    // Utility functions
    void MoveToWaypoint(bool loop = false)
    {
        waypoint = waypoints.Peek();		// get the waypoint (CHECK NULL?)
        if (Vector3.Distance(transform.position, waypoint) > 0.000000000001)    // if its not reached
        {                                                           // move towards it
            _direction = Vector3.Normalize(waypoint - transform.position);  // dont screw up waypoint by calling public setter
            Vector2 p = Vector2.MoveTowards(transform.position, waypoint, speed);
            GetComponent<Rigidbody2D>().MovePosition(p);
        }
        else    // if waypoint is reached, remove it from the queue
        {
            if (loop) waypoints.Enqueue(waypoints.Dequeue());
            else waypoints.Dequeue();
        }
    }

    public void Frighten()
    {
        state = State.Run;
        _direction *= -1;

        _timeToWhite = Time.time + _gm.scareLength * 0.66f;
        _timeToToggleWhite = _timeToWhite;
        GetComponent<Animator>().SetBool("Run_White", false);

    }

    public void Calm()
    {
        // if the ghost is not running, do nothing
        if (state != State.Run) return;

        waypoints.Clear();
        state = State.Chase;
        _timeToToggleWhite = 0;
        _timeToWhite = 0;
        GetComponent<Animator>().SetBool("Run_White", false);
        GetComponent<Animator>().SetBool("Run", false);
    }

    public void ToggleBlueWhite()
    {
        isWhite = !isWhite;
        GetComponent<Animator>().SetBool("Run_White", isWhite);
        _timeToToggleWhite = Time.time + _toggleInterval;
    }

}
