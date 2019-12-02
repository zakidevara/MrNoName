using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opengate : MonoBehaviour
{
    [SerializeField]
    private Transform transformA;
    [SerializeField]
    private Transform transformB;
    [SerializeField]
    private Transform transformC;
    [SerializeField]
    private Transform transformD;
    void Start()
    {
    }
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D other)
	{
		if(other.name == "pacman"||other.name == "nosis")
		{
		    transformA.position = transformB.position;
            transformC.position = transformD.position;
		}
	}

}
