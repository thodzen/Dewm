using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public float fireRate = 0;
    //public float randSpread;
    public int damage = 1;
    public LayerMask WhatToHit;

    public float camShakeAmount = 0;
    public float camShakeLength = 0;
    CameraShake camShake;

    public Transform bulletTrailPrefab;
    public Transform shellReleasePrefab;
    public Transform hitPrefab;
    public Transform lightPrefab;
    public Light muzzleFlash;
    public Transform muzzleFlashSprite;

    public AudioSource hitSound;
    public AudioSource goreSound;
    public bool canUse;

    private float timeToSpawnEffect = 0;
    public float effectSpawnRate = 10;
    private float timeToFire = 0;
    Transform firePoint;
    Transform casingReleasePoint;

    private bool muzzle = false;

    public AudioSource shootSound;


    // Use this for initialization
    void Awake()
    {
        firePoint = transform.Find("FirePoint");
        if (firePoint == null)
        {
            Debug.LogError("No FirePoint");
        }
        casingReleasePoint = transform.Find("CasingReleasePoint"); //Unity added this line
        //muzzlePosition = transform.Find("FirePoint");
    }

    private void Start()
    {
        camShake = LevelManager.lm.GetComponent<CameraShake>();
        if (camShake == null)
        {
            Debug.LogError("No CameraShake script found on LM object.");
        }
    }

    // Update is called once per frame
    void Update()
    {
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
        //Vector2 spread = new Vector2(0, randSpread);
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
        RaycastHit2D hit = Physics2D.Raycast(firePointPosition, mousePosition - firePointPosition, 100, WhatToHit);


        Debug.DrawLine(firePointPosition, (mousePosition - firePointPosition) * 100, Color.cyan);
        if (hit)
        {
            Debug.DrawLine(firePointPosition, hit.point, Color.red);
            Health hp = hit.transform.GetComponent<Health>();
            if (hp)
            {
                hp.Subtract(damage);
                Debug.Log("You hit " + hit.collider.name + " and did " + damage + " damage.");
            }

            KnockbackBehaviour knockback = hit.transform.GetComponent<KnockbackBehaviour>();
            if (knockback)
            {
                knockback.Knockback((mousePosition - firePointPosition).normalized * damage * 8);
            }

        }
        shootSound.Play();

        if (Time.time >= timeToSpawnEffect)
        {
            Vector3 hitPos;
            Vector3 hitNormal; // An effect that will shoot out perpendicular to what it collides with
            if (hit.collider == null)
            {
                hitPos = (mousePosition - firePointPosition) * 50; // Bullet Effect continues off screen
                hitNormal = new Vector3(9999, 9999, 9999);
            }
            else
            {
                hitPos = hit.point; // Bullet Effect stops when it hits something
                hitNormal = hit.normal;
            }
            Effect(hitPos, hitNormal, hit);
            timeToSpawnEffect = Time.time + 1 / effectSpawnRate;
        }
    }

    void Effect(Vector3 hitPos, Vector3 hitNormal, RaycastHit2D hit)
    {
        Transform trail = Instantiate(bulletTrailPrefab, firePoint.position, firePoint.rotation) as Transform;
        LineRenderer lineRender = trail.GetComponent<LineRenderer>();
        if (lineRender != null)
        {
            lineRender.SetPosition(0, firePoint.position);
            lineRender.SetPosition(1, hitPos);
        }
        Destroy(trail.gameObject, 0.04f);

        if (hitNormal != new Vector3(9999, 9999, 9999) && hit.transform.tag == "Enemy" || hit.transform.tag == "Corpse")
        {
            Instantiate(hitPrefab, hitPos, Quaternion.FromToRotation(Vector3.forward, hitNormal));
            goreSound.Play();
        }
        hitSound.Play();

        StartCoroutine(Muzzle());

        Rigidbody2D casing = Instantiate(shellReleasePrefab, casingReleasePoint.position, casingReleasePoint.rotation).gameObject.GetComponent<Rigidbody2D>();
        Instantiate(muzzleFlashSprite, firePoint.position, firePoint.rotation);
        casing.AddForce(new Vector2(-100f, 50f));
        camShake.Shake(camShakeAmount, camShakeLength);

    }

    private IEnumerator Muzzle()
    {
        if (muzzle)
        {
            yield break;
        }

        muzzleFlash.intensity = 0.3f;
        yield return new WaitForSeconds(0.15f);
        muzzleFlash.intensity = 0;
    }
}
