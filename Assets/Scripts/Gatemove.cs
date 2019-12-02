using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gatemove : MonoBehaviour
{
    private Vector3 posA;
    private Vector3 posB;
    private Vector3 nextPos;
    [SerializeField]
    private float speed;
    [SerializeField]
    private Transform child;
    [SerializeField]
    private Transform transformB;
    // Start is called before the first frame update
    void Start()
    {
        posA = child.localPosition;
        posB = transformB.localPosition;
        nextPos = posB;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    private void Move()
    {
        child.localPosition = Vector3.MoveTowards(child.localPosition,nextPos,speed * Time.deltaTime);
    }
}
