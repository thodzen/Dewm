using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

	public static PlayerController Current
	{
		get;
		private set;
	}

	private Rigidbody2D rb;

	private Vector2 targetPos;

    private bool isdied;

    public GameObject armToDisable;

	public SpriteRenderer sprite;

	[Header("Movement")]
	public float moveSpeed;
	public float moveVelocity;
	public AudioSource footstep;

	[Header("Arm")]
	Transform playerGraphics;
	Transform playerArm;

	[Header("Jumping")]
	public Transform groundCheck;
	public float groundCheckRadius;
	public LayerMask whatIsGround;
	public bool grounded;
	private bool doubleJumped;
	public float jumpHeight;
	private float jumpTimeCounter;
	public float jumpTime;
	private bool isJumpGuy;
	public GameObject jetPackEffect;
	Transform JetpackLightReleasePoint;
	Transform jetpackLightReleasePoint;
	public AudioSource jetpack;
	public Transform lightPrefab;

	[Header("Animation")]
	private Animator anim;
	private Health health;

	public Health Health
	{
		get { return health; }
	}

	private void Awake()
	{
		health = GetComponent<Health>();
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();

		playerGraphics = transform.Find("Graphics");
		if (playerGraphics == null)
		{
			Debug.LogError("No graphics objects as child of player");
		}

		playerArm = transform.Find("Arm");
		if (playerArm == null)
		{
			Debug.LogError("No arm objects as child of player");
		}

		JetpackLightReleasePoint = transform.Find("JetpackReleasePoint");
		jetpackLightReleasePoint = transform.Find("JetpackLightReleasePoint");

		Current = this;
	}

	// Use this for initialization
	void Start()
	{
	}

    public void Die()
    {
        isdied = true;
        StartCoroutine(DieAnimation());
    }

    public IEnumerator DieAnimation()
    {
        armToDisable.SetActive(false);
        anim.Play("PlayerDeath");
        yield return new WaitForSeconds(2.0f);

        LevelManager.lm.RestartLevel();
    }

    void FixedUpdate()
    {
        if (isdied) return;
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
	}

	// Update is called once per frame
	void Update()
	{
        if (isdied) return;

		if (grounded)
			doubleJumped = false;

		// anim.SetBool("Grounded", grounded);

		if (Input.GetKeyDown(KeyCode.Space) && grounded)
		{
			Jump();
		}

		if (Input.GetKey(KeyCode.Space) && isJumpGuy == true) //variable jumping
		{
			if (jumpTimeCounter > 0)
			{
				rb.velocity = Vector2.up * jumpHeight;
				jumpTimeCounter -= Time.deltaTime;

				if (doubleJumped == true)
					Instantiate(lightPrefab, JetpackLightReleasePoint.position, JetpackLightReleasePoint.rotation, JetpackLightReleasePoint);
			}
			else
			{
				isJumpGuy = false;
			}

		}

		if (Input.GetKeyUp(KeyCode.Space))
		{
			isJumpGuy = false;
		}

		if (Input.GetKeyDown(KeyCode.Space) && !doubleJumped && !grounded)
		{
			Jump();
			doubleJumped = true;
			Instantiate(jetPackEffect, JetpackLightReleasePoint.position, JetpackLightReleasePoint.rotation, JetpackLightReleasePoint);
			Instantiate(lightPrefab, JetpackLightReleasePoint.position, JetpackLightReleasePoint.rotation, JetpackLightReleasePoint);
			jetpack.Play();

		}

		moveVelocity = 0f;

		if (Input.GetKey(KeyCode.D))
		{
			MoveRight();
		}
		if (Input.GetKey(KeyCode.A))
		{
			MoveLeft();
		}

		anim.SetFloat("Speed", Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x));

		if (rb.velocity.x > 0)
		{
			//transform.rotation = Quaternion.Euler(0, 0, 0);
			sprite.flipX = false;
			JetpackLightReleasePoint.rotation = Quaternion.Euler(0, 0, 0);

			// rb.position += new Vector2(delta, 0.0f);
		}
		else if (rb.velocity.x < 0)
		{
			//transform.rotation = Quaternion.Euler(0, 180, 0);
			sprite.flipX = true;
			JetpackLightReleasePoint.rotation = Quaternion.Euler(0, 180, 0);
		}
	}


	public void Jump()
	{
		isJumpGuy = true;
		jumpTimeCounter = jumpTime;
		rb.AddForce(Vector2.up * jumpHeight);
	}

	public void MoveLeft()
	{
		rb.velocity = new Vector2(-moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
		moveVelocity = -moveSpeed;
		//footstep.Play();
	}

	public void MoveRight()
	{
		rb.velocity = new Vector2(moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
		moveVelocity = moveSpeed;
		//footstep.Play();
	}
}

