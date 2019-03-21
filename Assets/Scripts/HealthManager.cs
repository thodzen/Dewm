using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{

    public float maxPlayerHealth;
  
    public Text healthText;

    public static float playerHealth;

    Text text;
    public Slider healthBar;

    private LevelManager levelManager;

    public bool isDead;

    public AudioSource oof;

    // Use this for initialization
    void Start()
    {
        healthBar.value = 1;
        text = GetComponent<Text>();
        healthBar = GetComponent<Slider>();

        playerHealth = maxPlayerHealth;
        SetHealthUI();

        levelManager = FindObjectOfType<LevelManager>();
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (playerHealth <= 0 && !isDead)
        {
            playerHealth = 0;
            levelManager.RespawnPlayer();
            isDead = true;
            oof.Play();
            //LevelManager.KillPlayer(gameObject);
        }

        text.text = "" + playerHealth;
        healthBar.value = playerHealth;

    }

    public static void HurtPlayer(float damageToGive)
    {
        playerHealth -= damageToGive;
    }

    public void FullHealth()
    {
        playerHealth = maxPlayerHealth;
    }

    private void SetHealthUI()
    {
        healthBar.value = CalculateHealthPercentage();
        healthText.text = Mathf.Ceil(playerHealth).ToString() + " / " + Mathf.Ceil(maxPlayerHealth).ToString();
    }

    float CalculateHealthPercentage()
    {
        return playerHealth / maxPlayerHealth;
    }
}
