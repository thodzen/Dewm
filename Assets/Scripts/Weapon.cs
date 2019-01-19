using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public float fireRate = 0;
    public int damageToGive = 1;
    public LayerMask WhatToHit;

    public Transform bulletTrailPrefab;
    public Transform shellReleasePrefab;

    private float timeToSpawnEffect = 0;
    public float effectSpawnRate = 10;
    private float timeToFire = 0;
    Transform firePoint;
    Transform casingReleasePoint;


    // Use this for initialization
    void Awake () {
        firePoint = transform.Find("FirePoint");
        if (firePoint == null)
        {
            Debug.LogError("No FirePoint");
        }
        casingReleasePoint = transform.Find("CasingReleasePoint");
    }
	
	// Update is called once per frame
	void Update () {
        if (fireRate == 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
        else
        {
            if (Input.GetButton("Fire1") && Time.time > timeToFire)
            {
                timeToFire = Time.time + 1 / fireRate;
                Shoot();
            }
        }
	}

    void Shoot()
    {
        Vector2 mousePosition = new Vector2 (Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
        RaycastHit2D hit = Physics2D.Raycast(firePointPosition, mousePosition - firePointPosition, 100, WhatToHit);
        if (Time.time >= timeToSpawnEffect)
        {
            Effect();
            timeToSpawnEffect = Time.time + 1 / effectSpawnRate;
        }

        Debug.DrawLine(firePointPosition, (mousePosition - firePointPosition) * 100, Color.cyan);
        if (hit.collider != null)
        {
            Debug.DrawLine(firePointPosition, hit.point, Color.red);
            EnemyHealthManager enemy = hit.collider.GetComponent<EnemyHealthManager>();
            if (enemy != null)
            {
                enemy.giveDamage(damageToGive);
                Debug.Log("You hit " + hit.collider.name + " and did " + damageToGive + " damage.");
            }
        }                
    }

    void Effect()
    {
        Instantiate(bulletTrailPrefab, firePoint.position, firePoint.rotation);
        Instantiate(shellReleasePrefab, casingReleasePoint.position, casingReleasePoint.rotation);
    }

}
