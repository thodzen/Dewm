using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtEnemyOnContact : MonoBehaviour {

    public int damageToGive;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyHealthManager>().giveDamage(damageToGive);
            Debug.Log("ouch");
        }
    }

}
