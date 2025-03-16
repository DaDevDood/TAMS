using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class PlayerController : NetworkBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;

    private Rigidbody2D rb;
    private bool isGrounded;

    private Animator playerAnim;
    bool notMoving;
    bool notJumping;

    public GameObject explosion;
    bool facingRight = true;

    public GameObject Gun;

    public AudioSource Die;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
        Instantiate(Gun, transform.position, Quaternion.identity);
    }

    void Update()
    {
        if (!IsOwner) return;
        
        Move();
        //Jump();

        if (Input.GetKeyDown(KeyCode.F3)){
            Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
            Die.Play();
            Destroy(gameObject);

        }
    }

    IEnumerator PlayerDie()
    {

        
        yield return new WaitForSeconds(.3f);

        


    }
    void Move()
    {
        float moveInput = Input.GetKey(KeyCode.D) ? 1 : Input.GetKey(KeyCode.A) ? -1 : 0;
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        if(moveInput != 0) playerAnim.SetBool("Moving", true);
        else playerAnim.SetBool("Moving", false);

        if (moveInput > 0 && !facingRight)
        {
            // ... flip the player.
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (moveInput < 0 && facingRight)
        {
            // ... flip the player.
            Flip();
        }
    }
    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    void Jump()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }
}
