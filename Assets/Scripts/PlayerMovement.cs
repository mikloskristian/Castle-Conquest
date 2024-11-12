using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.Mathematics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 10.0f;
    [SerializeField] private float playerJumpAmmount = 15.0f;
    [SerializeField] private float climbingSpeed = 10f;
    private Rigidbody2D playerRigidBody;
    private Animator playerAnimator;
    private BoxCollider2D boxCollider;
    private PolygonCollider2D polygonCollider;
    private float horizontalMovement;
    private float verticalMovement;
    private bool isRunning;
    private bool isJumping;
    private bool isClimbing;
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        polygonCollider = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        Climb();
        if(polygonCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            Jump();
        }
    }

    private void Jump()
    {
        isJumping = Input.GetButtonDown("Jump");

        if(isJumping)
        {
            playerRigidBody.velocity = new UnityEngine.Vector2(playerRigidBody.velocity.x, playerJumpAmmount);
        }
    }

    private void Climb()
    {
        if(boxCollider.IsTouchingLayers(LayerMask.GetMask("Climbable")))
        {
            verticalMovement = Input.GetAxis("Vertical");
            UnityEngine.Vector2 climbingVelocity = new UnityEngine.Vector2(playerRigidBody.velocity.x, verticalMovement * climbingSpeed);

            playerRigidBody.velocity = climbingVelocity;

            playerAnimator.SetBool("isClimbing", true);
        }

        playerAnimator.SetBool("isClimbing", false);
    }

    private void ChangeClimbingAnimation()
    {
        isClimbing = Mathf.Abs(playerRigidBody.velocity.y) > Mathf.Epsilon;
        playerAnimator.SetBool("isClimbing", isClimbing);
    }

    private void Run()
    {
        horizontalMovement = Input.GetAxis("Horizontal");
        playerRigidBody.velocity = new UnityEngine.Vector2(horizontalMovement * playerSpeed, playerRigidBody.velocity.y);
        FlipSprite();
        ChangeRunningAnimation();
    }

    private void FlipSprite()
    {
        isRunning = Mathf.Abs(playerRigidBody.velocity.x) > Mathf.Epsilon;
        if(isRunning)
        {
            transform.localScale = new UnityEngine.Vector2(Mathf.Sign(playerRigidBody.velocity.x), transform.localScale.y);
        }
    }

    private void ChangeRunningAnimation()
    {
        playerAnimator.SetBool("isRunning", isRunning);
    }
}
