using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerForce : MonoBehaviour
{

    public Rigidbody2D rb;
    public float moveSpeed;
    // public float airSpeed;
    public float jumpPower;
    public float fallSpeed; // 'brellas.
    private float moveHorizontal;
    private float moveVertical;
    public bool isGrounded;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        moveHorizontal = Input.GetAxis("Horizontal") * moveSpeed;

        if (isGrounded && Input.GetAxis("Vertical") > 0 && moveVertical == 0)
        {
            moveVertical = jumpPower; // Input.GetAxis("Vertical") * jumpPower;
        } else
        {
            moveVertical = 0;
        }

        rb.AddForce(new Vector2(moveHorizontal, 0));
        rb.AddForce(new Vector2(0, moveVertical), ForceMode2D.Impulse);
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            isGrounded = false;
        }
    }
}
