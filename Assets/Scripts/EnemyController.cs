using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D rb;

    public float knockbackForce;
    public float knockbackLength;
    private float knockbackCounter;
    public bool knockback; // use this so the enemy cannot move forward while being knocked back

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(knockbackForce * transform.localScale.x, rb.velocity.y);

        if (knockbackCounter <= 0)
        {
            knockback = false;
        }
        return;
    }

    // Update is called once per frame
    void Update()
    {
        if (knockback)
        {
            knockbackCounter -= Time.deltaTime;
        }
    }

    public void Knockback(Vector2 direction)
    {
        knockbackCounter = knockbackLength;
        rb.velocity = direction * knockbackForce;
        knockback = true;
    }
}
