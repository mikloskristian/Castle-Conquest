using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D playerRB;
    [SerializeField] private float playerSpeed = 100.0f;
    float horizontalMovement;
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
    }

    void Run()
    {
        horizontalMovement = Input.GetAxis("Horizontal");
        playerRB.velocity = new Vector2(horizontalMovement * playerSpeed * Time.deltaTime, playerRB.velocity.y);
    }
}
