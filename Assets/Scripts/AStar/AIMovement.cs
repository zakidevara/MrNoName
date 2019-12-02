using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    AStarPathfinding pathfinding;
    public GameObject target;

    private Vector3 targetLoc;
    private List<Node> path;

    public float speed;
    public Vector3 spawn;
    public float aggroRange;

    // Start is called before the first frame update
    void Start()
    {
        //target = GameObject.Find("noname"); 
        target = GameObject.Find("pacman");
        if(target == null) GameObject.Find("nosis");
        pathfinding = this.GetComponentInParent<AStarPathfinding>();
        this.transform.position = spawn;
    }

    // Update is called once per frame
    void Update()
    {
        targetLoc = target.transform.position;
        if (Vector3.Distance(this.transform.position, targetLoc) <= aggroRange)
        {
            path = pathfinding.FindPath(this.GetComponentInParent<Transform>().position, targetLoc);
            if (path.Count > 0)
            {
                this.transform.position = Vector3.MoveTowards(transform.position, pathfinding.WorldPointFromNode(path[0]), speed * Time.deltaTime);
            }
        }
        else {
            this.transform.position = Vector3.MoveTowards(transform.position,spawn, speed * Time.deltaTime);
        }

    }
}
