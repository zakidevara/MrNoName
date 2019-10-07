// Patrol.cs
using UnityEngine;
using UnityEngine.AI;
using System.Collections;


public class patrolBigBro : MonoBehaviour
{

    public float speed;
    private float waitTime;
    public float startWaitTime;

    public Transform[] moveSpots;
    private int nextSpot;
    private bool reverse;
    void Start()
    {
        waitTime = startWaitTime;
        nextSpot = 0;
        reverse = false ;
    }

    void Update()
    {
        
        transform.position = Vector2.MoveTowards(transform.position, moveSpots[nextSpot].position, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, moveSpots[nextSpot].position) < 0.2f)
        {
            if (nextSpot == moveSpots.Length - 1 && !reverse)
            {
                reverse = true;
            }
            else if(nextSpot == 0 && reverse){
                reverse = false;
            }

            if (!reverse)
            {
                nextSpot++;
            }
            else {
                nextSpot--;
            }
            
            waitTime = startWaitTime;
        }
        else
        {
            waitTime -= Time.deltaTime;
        }
    }




}
