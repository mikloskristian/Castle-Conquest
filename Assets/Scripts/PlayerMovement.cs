using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 100.0f;
    [SerializeField] private float playerJumpAmmount = 150.0f;
    private Rigidbody2D playerRigidBody;
    private Animator playerAnimator;
    private BoxCollider2D boxCollider;
    private PolygonCollider2D polygonCollider;
    private float horizontalMovement;
    private bool isRunning;
    private bool isJumping;
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
            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, playerJumpAmmount);
        }
    }

    private void Run()
    {
        horizontalMovement = Input.GetAxis("Horizontal");
        playerRigidBody.velocity = new Vector2(horizontalMovement * playerSpeed, playerRigidBody.velocity.y);
        FlipSprite();
        ChangeRunningAnimation();
    }

    private void FlipSprite()
    {
        isRunning = Mathf.Abs(playerRigidBody.velocity.x) > Mathf.Epsilon;
        if(isRunning)
        {
            transform.localScale = new Vector2(Mathf.Sign(playerRigidBody.velocity.x), transform.localScale.y);
        }
    }

    private void ChangeRunningAnimation()
    {
        playerAnimator.SetBool("isRunning", isRunning);
    }
}
