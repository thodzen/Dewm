﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthManager : MonoBehaviour {

    public float enemyHealth;
    public float maxHealth;

    public GameObject healthBar;
    public Slider healthBarSlider;

    // Use this for initialization
    void Start()
    {
        enemyHealth = maxHealth;
    }

    public void giveDamage(float damageToGive)
    {
        healthBar.SetActive(true);
        healthBarSlider.value = CalculateHealthPercentage();
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
