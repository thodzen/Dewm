using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtOnContact : MonoBehaviour
{
    public int damage;

    private Collider2D col;

    private void Awake()
    {
        col = GetComponent<Collider2D>();

        if (!col) Destroy(this);

        col.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Subtract health

        Health health = other.GetComponent<Health>();

        if (health)
        {
            health.Subtract(damage);
        }

        // Knockback

        KnockbackBehaviour knockback = other.GetComponent<KnockbackBehaviour>();

        if (knockback)
        {
            knockback.Knockback((other.transform.position - transform.position).normalized);
        }
    }
}
