using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{

    public LevelManager levelManager;

    // Use this for initialization
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>(); //gets the empty levelManager and makes it into the one in the scene
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other) //detects when player enters a trigger zone
    {
        if (other.name == "Player")
        {
            levelManager.RespawnPlayer();
        }
    }

}
