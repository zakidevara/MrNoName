// Patrol.cs
using UnityEngine;
using UnityEngine.AI;
using System.Collections;


public class Patrol : MonoBehaviour
{

    public float speed;
    private float waitTime;
    public float startWaitTime;

    public Transform[] moveSpots;
    private int nextSpot;
    void Start()
    {
        waitTime = startWaitTime;
        nextSpot = 0;
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, moveSpots[nextSpot].position, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, moveSpots[nextSpot].position) < 0.2f) {
            if (nextSpot == moveSpots.Length - 1) nextSpot = 0;
            else nextSpot++;
            waitTime = startWaitTime;
        }
        else {
            waitTime -= Time.deltaTime;
        }
    }




}
