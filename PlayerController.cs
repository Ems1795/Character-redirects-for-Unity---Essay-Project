using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


    Rigidbody2D playerRB;
    Animator playerAnimator;
    public float moveSpeed = 1f;
    public float jumpSpeed = 1f, jumpFrequency = 1f, nextJumpTime;

    bool facingRight = true;

    public bool isGrounded = false;
    public Transform groundcheckPosition;
    public float groundCheckRadius;
    public LayerMask groundCheckLayer;

    void Awake()
    {

    }


    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        HorizontalMove();
        OnGroundCheck();

        if (playerRB.velocity.x < 0 && facingRight)
        {
            flipFace();

        }

        else if (playerRB.velocity.x > 0 && !facingRight)
        {

            flipFace();
        }

        if (Input.GetAxis("Vertical") > 0 && isGrounded && (nextJumpTime < Time.timeSinceLevelLoad))

        {
            nextJumpTime = Time.timeSinceLevelLoad + jumpFrequency;
            jump();
        }
    }

    void FixedUpdate()
    {

    }

    void HorizontalMove()
    {
        playerRB.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, playerRB.velocity.y);
        playerAnimator.SetFloat("playerSpeed", Mathf.Abs(playerRB.velocity.x));
    }

    void flipFace()
    {

        facingRight = !facingRight;
        Vector3 tempLocalScale = transform.localScale;
        tempLocalScale.x *= -1;
        transform.localScale = tempLocalScale;
    }

    void jump()
    {
        playerRB.AddForce(new Vector2(0f, jumpSpeed));
    }

    void OnGroundCheck()
    {
        isGrounded = Physics2D.OverlapCircle(groundcheckPosition.position, groundCheckRadius, groundCheckLayer);
        playerAnimator.SetBool("ÝsGroundedAnim", isGrounded);
    }
}