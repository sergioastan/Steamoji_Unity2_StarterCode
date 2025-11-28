using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    private Rigidbody2D rigidBody;
    private float moveDir;
    public float moveSpeed;
    public float jumpForce;
    public Vector2 boxSize;
    public float boxDistance;
    public LayerMask groundLayer;
    private Animator playerAnimator;
    private bool facingRight = true;

    // Start is called before the first frame update
    void Start() {
        rigidBody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        moveDir = Input.GetAxisRaw("Horizontal");
        rigidBody.velocity = new Vector2(moveDir * moveSpeed, rigidBody.velocity.y);
        Debug.Log(isGrounded());

        if (Input.GetButtonDown("Jump") && isGrounded() ) {
            rigidBody.AddForce(new Vector2(rigidBody.velocity.x, jumpForce * 10));
        }

        // when if player is moving, change the animator variable to 1 to start moving animation
        if (Mathf.Abs(moveDir) > 0.1f) {
            playerAnimator.SetFloat("Player_Speed", 1);
        } else {
            playerAnimator.SetFloat("Player_Speed", 0);
        }

        // if facing right and the player is moving left, flip
        if (moveDir < -0.1f && facingRight) {
            Flip();
        // if facing left and the player is moving right, flip
        } else if (moveDir > 0.1f && !facingRight) {
            Flip();
        }
    }

    public bool isGrounded() {
        if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, boxDistance, groundLayer)) {
            return true;
        } else {
            return false;
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position-transform.up*boxDistance, boxSize);
    }

    private void Flip() {
        facingRight = !facingRight;
        Vector2 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;
    }
}
