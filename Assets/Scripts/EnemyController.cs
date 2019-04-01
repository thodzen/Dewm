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

	[SerializeField]
	private GameObject spriteObject;
	private SpriteRenderer sprite;

	[SerializeField]
	private float speed = 1.0f;
	[SerializeField]
	private float jumpHeight = 1.0f;

	// AI Movement duration settings
	[SerializeField]
	private float aiMaxMovementDuration = 5.0f;
	[SerializeField]
	private float aiMinMovementDuration = 2.0f;

	// AI Idle duration settings
	[SerializeField]
	private float aiMaxIdleDuration = 3.0f;
	[SerializeField]
	private float aiMinIdleDuration = 1.5f;

	[SerializeField]
	private GameObject playerObject;

	// AI Movement
	private float aiMovementDuration = 0.0f;
	private float aiDirection = 0.0f;

	private bool aiSwitch = true; // True = Moving, False = Idle
	private bool gotPlayerInSights = false;

	// AI Idle
	private float aiIdleDuration = 0.0f;

	// Start is called before the first frame update
	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(knockbackForce * transform.localScale.x, rb.velocity.y);

        if (knockbackCounter <= 0)
        {
            knockback = false;
        }

		sprite = spriteObject.GetComponent<SpriteRenderer> ();
    }

    // Update is called once per frame
    void Update()
    {
        if (knockback)
        {
            knockbackCounter -= Time.deltaTime;
        }

		if (gotPlayerInSights)
		{
			var vector = playerObject.transform.position.x - transform.position.x;
			var direction = vector / Mathf.Abs (vector);
			var delta = direction * speed * Time.deltaTime;

			sprite.flipX = delta < 0;

			rb.position += new Vector2 (delta, 0.0f);
		}
		else
		{
			Patrol ();
		}
    }

	void MoveLeft ()
	{
	}

	void MoveRight ()
	{
		sprite.flipX = false;
	}

	void Patrol ()
	{
		if (aiIdleDuration > 0.0f)
		{
			aiIdleDuration -= Time.deltaTime;
		}
		else if (aiMovementDuration > 0.0f)
		{
			var delta = aiDirection * speed * Time.deltaTime;

			sprite.flipX = delta < 0;

			rb.position += new Vector2 (delta, 0.0f);

			aiMovementDuration -= Time.deltaTime;
		}
		else if (aiSwitch && aiMovementDuration <= 0.0f)
		{
			aiMovementDuration = Random.Range (aiMinMovementDuration, aiMaxMovementDuration);

			aiDirection = Random.Range (-1.0f, 1.0f);
			aiDirection /= Mathf.Abs (aiDirection);

			aiSwitch = false;
		}
		else if (!aiSwitch && aiIdleDuration <= 0.0f)
		{
			aiIdleDuration = Random.Range (aiMinIdleDuration, aiMaxIdleDuration);

			aiSwitch = true;
		}
	}

	public void SetPlayerInSight (bool on)
	{
		if (on)
		{
			aiIdleDuration = 0.0f;
			aiMovementDuration = 0.0f;

			gotPlayerInSights = true;
		}
		else
		{
			aiSwitch = false;
			gotPlayerInSights = false;
		}
	}

	public void SomethingOnFeet (Collider2D collider)
	{
		if (collider.gameObject.tag.Equals ("Obstacle"))
		{
			rb.velocity = new Vector2 (rb.velocity.x, jumpHeight);
		}
		else
		{
			aiDirection = -aiDirection;
		}
	}

	public void Knockback(Vector2 direction)
    {
        knockbackCounter = knockbackLength;
        rb.velocity = direction * knockbackForce;
        knockback = true;
    }
}
