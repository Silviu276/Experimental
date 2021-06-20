using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed, jumpSpeed;
    private Vector2 movement;
    [SerializeField] private Rigidbody2D playerBody;
    [SerializeField] private BoxCollider2D playerCollider;
    private LayerMask platformMask;
    private bool grounded;

    private void Start()
    {
        platformMask = LayerMask.GetMask("Platforms");
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        playerBody.velocity = new Vector2(movement.x * speed, playerBody.velocity.y);

        RaycastHit2D groundCheck = Physics2D.BoxCast(playerCollider.bounds.center, new Vector2(0.5f, 1f), 0f, Vector2.down, 0.01f,
            platformMask);
        if (groundCheck.collider != null)
        {
            grounded = true;
            Debug.DrawRay(playerCollider.bounds.center, Vector2.down * 0.51f, Color.green);
        }
        else
        {
            grounded = false;
            Debug.DrawRay(playerCollider.bounds.center, Vector2.down * 0.51f, Color.red);
        }

        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && grounded)
        {
            Jump();
        }

        if (movement.x != 0f)
            transform.localScale = new Vector3(movement.x / 2, 1f, 1f);
    }

    private void Jump()
    {
        playerBody.velocity = new Vector2(playerBody.velocity.x, 0f);
        playerBody.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
    }
}
