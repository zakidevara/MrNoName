using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tringger : MonoBehaviour
{
    private GameManager GM;
    public GameObject pacman;
    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }
    void OnTringgerEnter2D(Collider2D col){
        if (col.name == "pacman"){
            GM.LoseLife();
        }else{
            GM.LoseLife();
        }
    }

}
