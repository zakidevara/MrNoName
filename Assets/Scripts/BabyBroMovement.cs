using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyBroMovement : MonoBehaviour
{
    private GameManager _gm;
    public Transform[] waypoints;
    int cur = 0;

    public float speed = 0.3f;
    private void FixedUpdate()
    {
        if (transform.position != waypoints[cur].position)
        {
            Vector2 p = Vector2.MoveTowards(transform.position,
                                            waypoints[cur].position,
                                            speed);
            GetComponent<Rigidbody2D>().MovePosition(p);
        }
        // Waypoint reached, select next one
        else cur = (cur + 1) % waypoints.Length;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "pacman")
        {
            
                _gm.LoseLife();
            

        }
    }
}
