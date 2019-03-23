using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthManager : MonoBehaviour {

    public float enemyHealth;
    public float maxHealth;

    public GameObject healthBar;
    public Slider healthBarSlider;

    private EnemyController enemyController;

    public AudioSource oof;
    public AudioSource splat;

    // Use this for initialization
    void Start()
    {
        enemyHealth = maxHealth;
        enemyController = GetComponent<EnemyController>();
    }

    public void giveDamage(float damageToGive)
    {
        healthBar.SetActive(true);
        healthBarSlider.value = CalculateHealthPercentage();
        enemyHealth -= damageToGive;
        CheckDeath();
        enemyController.Knockback();
    }

    public void CheckDeath()
    {
        if (enemyHealth <= 0)
        {
            //Instantiate(deathEffect, transform.position, transform.rotation);
            Destroy(gameObject);
           // oof.Play();
            splat.Play();
        }


    }

    public float CalculateHealthPercentage()
    {
        return (enemyHealth / maxHealth);
    }
}
