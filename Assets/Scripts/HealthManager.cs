using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{

    public int maxPlayerHealth;
    public Slider healthSlider;
    public Text healthText;

    public static int playerHealth;

    Text text;
    public Slider healthBar;

    public LevelManager levelManager;

    public bool isDead;

    public AudioSource oof;

    private Animator anim;

    // Use this for initialization
    void Start()
    {
        text = GetComponent<Text>();
        healthBar = GetComponent<Slider>();

        playerHealth = maxPlayerHealth;
        SetHealthUI();
        isDead = false;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (playerHealth <= 0 && !isDead)
        {
            playerHealth = 0;
            isDead = true;
            oof.Play();
            anim.SetBool("IsDead", true);
            levelManager.RespawnPlayer();
        }

        //text.text = "" + playerHealth;
        healthBar.value = playerHealth;

    }

    public static void HurtPlayer(int damageToGive)
    {
        playerHealth -= damageToGive;
    }

    public void FullHealth()
    {
        playerHealth = maxPlayerHealth;
    }

    private void SetHealthUI()
    {
        healthSlider.value = CalculateHealthPercentage();
        healthText.text = Mathf.Ceil(playerHealth).ToString() + " / " + Mathf.Ceil(maxPlayerHealth).ToString();
    }

    int CalculateHealthPercentage()
    {
        return playerHealth / maxPlayerHealth;
    }

}
