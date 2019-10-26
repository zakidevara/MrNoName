using System;
using UnityEngine;
using System.Collections;

public class PlayerControllerStoryMode : MonoBehaviour
{

    public Joystick joystick;
    public Joystick button;
    public float speed = 0.2f;
    Vector2 _dest = Vector2.zero;
    Vector2 _dir = Vector2.zero;
    Vector2 _nextDir = Vector2.zero;

    // Dash Ability variables
    private Rigidbody2D rb;
    public float dashCooldown;
    private float dashCooldownTime;
    private float dashStart;
    public float dashSpeed;
    public float dashDistance;
    private bool isDashing = false;
    public ParticleSystem dashEffect;

    [Serializable]
    public class PointSprites
    {
        public GameObject[] pointSprites;
    }

    public PointSprites points;

    public static int killstreak = 0;

    

    // script handles
    private GameGUINavigationStory GUINav;
    private GameManagerStoryMode GM;
    private ScoreManager SM;
    private highscoreTable HT;

    private bool _deadPlaying = false;

    // Use this for initialization
    void Start()
    {
        GM = GameObject.Find("Game Manager").GetComponent<GameManagerStoryMode>();
        SM = GameObject.Find("Game Manager").GetComponent<ScoreManager>();
        GUINav = GameObject.Find("UI Manager").GetComponent<GameGUINavigationStory>();
        HT = GameObject.Find("Game Manager").GetComponent<highscoreTable>();
        rb = GetComponent<Rigidbody2D>();
        dashCooldownTime = 0;
        _dest = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (GameManagerStoryMode.gameState)
        {
            case GameManagerStoryMode.GameState.Game:
                
                ReadInputAndMove();
                Animate();
                break;

            case GameManagerStoryMode.GameState.Dead:
                
                if (!_deadPlaying)
                {
                    StartCoroutine("PlayDeadAnimation");
                }
                
                break;
        }


    }
    void Update()
    {
        
    }
  
    IEnumerator PlayDeadAnimation()
    {
        
        _deadPlaying = true;
        GetComponent<Animator>().SetBool("Die", true);
        GM.disableEnemyMovements();
        GM.disableTrapMovements();
        yield return new WaitForSeconds(1);
        GetComponent<Animator>().SetBool("Die", false);
        _deadPlaying = false;

        
        if (GameManagerStoryMode.lives <= 0)
        {
            
            GUINav.H_ShowGameOverScreen();
        }

        else
            GM.ResetScene();
    }

    void Animate()
    {
        Vector2 dir = _dest - (Vector2)transform.position;
        GetComponent<Animator>().SetFloat("DirX", dir.x);
        GetComponent<Animator>().SetFloat("DirY", dir.y);
    }

    bool Valid(Vector2 direction)
    {
        // cast line from 'next to pacman' to pacman
        // not from directly the center of next tile but just a little further from center of next tile
        Vector2 pos = transform.position;
        RaycastHit2D hit = Physics2D.Linecast(pos + direction, pos);
        //Debug.Log(hit.distance);
        return hit.collider.name == "pacdot" || (hit.collider == GetComponent<Collider2D>());
    }

    public void ResetDestination(float x, float y)
    {
        _dest = new Vector2(x, y);
        GetComponent<Animator>().SetFloat("DirX", 1);
        GetComponent<Animator>().SetFloat("DirY", 0);
    }

    void ReadInputAndMove()
    {
        // move closer to destination
        Vector2 p = Vector2.MoveTowards(transform.position, _dest, speed);
        GetComponent<Rigidbody2D>().MovePosition(p);
        

        // get the next direction from keyboard
              
        if (Input.GetAxis("Horizontal") > 0) _nextDir = Vector2.right;
        if (Input.GetAxis("Horizontal") < 0) _nextDir = Vector2.left;
        if (Input.GetAxis("Vertical") > 0) _nextDir = Vector2.up;
        if (Input.GetAxis("Vertical") < 0) _nextDir = Vector2.down;

        if (joystick.Horizontal > 0) _nextDir = Vector2.right;
        if (joystick.Horizontal < 0) _nextDir = Vector2.left;
        if (joystick.Vertical > 0) _nextDir = Vector2.up;
        if (joystick.Vertical < 0) _nextDir = Vector2.down;
        if (dashCooldownTime > 0) dashCooldownTime -= Time.deltaTime;
        //Debug.Log(dashCooldownTime);
        if (this.name.Equals("nosis") && (dashCooldownTime <= 0) && (Input.GetKeyDown(KeyCode.Z)||button.Horizontal!=0))
        {
            _nextDir = _dir;
            isDashing = true;
        }
        //Debug.Log(Time.time - dashStartTime);

        // if pacman is in the center of a tile
        if (Vector2.Distance(_dest, transform.position) < 0.00001f)
        {
            if (isDashing)
            {
                /*RaycastHit2D[] theHit = Physics2D.RaycastAll(transform.position + (Vector3)(_nextDir * dashDistance), transform.position);
                Debug.Log(theHit);
                foreach (RaycastHit2D i in theHit) {
                    Debug.Log(i.collider);
                }*/
                /*if (theHit != null && theHit.distance >= dashDistance)
                {
                    _dest = (Vector2)transform.position + (_nextDir * dashDistance);
                }
                else
                {
                    _dest = (Vector2)transform.position + (_nextDir * theHit.distance);
                }*/

                Dash(_nextDir);

            }
            else {
                if (Valid(_nextDir)){
                    _dest = (Vector2)transform.position + _nextDir;
                    _dir = _nextDir;
                    speed = 0.2f;
                }else{
                    if (Valid(_dir)) // and the prev. direction is valid
                    {
                        _dest = (Vector2)transform.position + _dir;   // continue on that direction
                        speed = 0.2f;
                    }
                }
            }
        }
    }

    public Vector2 getDir()
    {
        return _dir;
    }

    public void UpdateScore()
    {
        killstreak++;

        // limit killstreak at 4
        if (killstreak > 4) killstreak = 4;
        if (killstreak - 1 < points.pointSprites.Length)
        {
            Instantiate(points.pointSprites[killstreak - 1], transform.position, Quaternion.identity);
        }
        GameManagerStoryMode.score += (int)Mathf.Pow(2, killstreak) * 100;

    }

    private void Dash(Vector2 direction)
    {
        Debug.Log("Use dash ability!");
        float actualDistance = 1;
        for (int i = 1; i <= dashDistance; i++)
        {
            actualDistance = (float)i - 1;
            if (!Valid(direction * i))
            {
                break;
            }
        }
        //Instantiate(dashEffect, transform.position, Quaternion.identity);
        dashEffect.Play();
        _dest = (Vector2)transform.position + (direction * actualDistance);
        speed = dashSpeed;
        dashStart = Time.time;
        dashCooldownTime = dashCooldown;
        isDashing = false;
    }

   
    
}
