using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Rigidbody2D rb;

    private Vector2 targetPos;

    [Header("Movement")]
    public float moveSpeed;
    public float moveVelocity;

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
    private bool isJumping;

    //[Header("Animation")]
    //private Animator anim;

    private void Awake()
    {
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
    }

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        //anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    // Update is called once per frame
    void Update()
    {

        if (grounded)
            doubleJumped = false;

       // anim.SetBool("Grounded", grounded);

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            Jump();
        }

        if (Input.GetKey(KeyCode.Space) && isJumping == true) //variable jumping
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpHeight;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }

        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }

         if (Input.GetKeyDown(KeyCode.Space) && !doubleJumped && !grounded)
         {
             Jump();
             doubleJumped = true;
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

        //anim.SetFloat("Speed", Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x));

        if (rb.velocity.x > 0)
        {
            playerGraphics.localScale = new Vector3(1f, 1f, 1f); //Flips player body and NOT arm
            playerArm.localScale = new Vector3(1f, 1f, 1f); //Keeps are flipped the right way when facing right
        }
        else if (rb.velocity.x < 0)
        {
            playerGraphics.localScale = new Vector3(-1f, 1f, 1f); //Flips player body and NOT arm
            playerArm.localScale = new Vector3(1f, -1f, 1f); //Flips arm verticle when turned around left
        }
    }


    public void Jump()
    {
        isJumping = true;
        jumpTimeCounter = jumpTime;
        rb.velocity = Vector2.up * jumpHeight;
        rb.velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpHeight); //CHECK
    }

    public void MoveLeft()
    {
        rb.velocity = new Vector2(-moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
        moveVelocity = -moveSpeed;
    }

    public void MoveRight()
    {
        rb.velocity = new Vector2(moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
        moveVelocity = moveSpeed;
    }

}

