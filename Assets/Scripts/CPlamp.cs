using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlamp : MonoBehaviour
{
    public Light FlashLightObject;
    private bool LightEnabled = false;
    void Start()
    {
        FlashLightObject.enabled = LightEnabled;
    }
   
    void OnTriggerEnter2D(Collider2D other)
	{
		if(other.name == "pacman" || other.name == "nosis")
		{
            LightEnabled = true;
            FlashLightObject.enabled = LightEnabled;
		}
	}
}
