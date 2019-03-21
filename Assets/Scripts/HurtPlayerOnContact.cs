using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayerOnContact : MonoBehaviour
{

    public int damageToGive;
    public GameObject deathEffect;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnTriggerEnter2D(Collider2D other) //detects when player enters a trigger zone
    {

        if (other.name == "Player")
        {
            HealthManager.HurtPlayer(damageToGive);
            Debug.Log("Hurt Player");

            Vector3 particlePosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            Instantiate(deathEffect, particlePosition, transform.rotation);

            //var player = other.GetComponent<PlayerController>(); //calling PlayerController script
            //player.knockbackCount = player.knockbackLength;

            //if (other.transform.position.x < transform.position.x)
               // player.knockbackFromRight = true;
           // else
               // player.knockbackFromRight = false;
        }
    }
}
