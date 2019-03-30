using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackBehaviour : MonoBehaviour
{
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        if (!rb) Destroy(this);
    }

    public void Knockback(Vector2 force)
    {
        rb.AddForce(force);
    }
}
