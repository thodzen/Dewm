using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour {

    public float enemyHealth;
    public float maxHealth;

    // Use this for initialization
    void Start()
    {
        enemyHealth = maxHealth;
    }

    public void giveDamage(float damageToGive)
    {
        enemyHealth -= damageToGive;
        CheckDeath();

    }

    public void CheckDeath()
    {
        if (enemyHealth <= 0)
        {
            //Instantiate(deathEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }


    }

    public float CalculateHealthPercentage()
    {
        return (enemyHealth / maxHealth);
    }
}
