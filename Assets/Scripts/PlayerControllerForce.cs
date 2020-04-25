using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerForce : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;
    public float moveSpeed;
    // public float airSpeed;
    public float jumpPower;
    public float fallSpeed; // 'brellas.
    public float moveHorizontal;
    private float moveVertical;
    public bool isGrounded;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        moveHorizontal = Input.GetAxis("Horizontal") * moveSpeed;

        anim.SetFloat("Speed", Mathf.Abs(moveHorizontal));

        if (moveHorizontal > 0)
            transform.rotation = Quaternion.Euler(0, 0, 0);
        else if (moveHorizontal < 0)
            transform.rotation = Quaternion.Euler(0, 180, 0);

        if (isGrounded && Input.GetAxis("Vertical") > 0 && moveVertical == 0)
        {
            moveVertical = jumpPower; // Input.GetAxis("Vertical") * jumpPower;
            anim.SetBool("isJumping", true);
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
            anim.SetBool("isJumping", false);
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
