using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D playerRigidBody;
    private Animator playerAnimator;
    [SerializeField] private float playerSpeed = 100.0f;
    private float horizontalMovement;
    private bool isRunning;
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        
    }

    private void Run()
    {
        horizontalMovement = Input.GetAxis("Horizontal");
        playerRigidBody.velocity = new Vector2(horizontalMovement * playerSpeed * Time.deltaTime, playerRigidBody.velocity.y);
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
