using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerForce : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;

    public GameObject magnetFist;
    // public float armSpeed;
    public float moveSpeed;
    // public float airSpeed;
    public float jumpPower;
    public float fallSpeed; // 'brellas.
    public float moveHorizontal;
    private float moveVertical;
    public bool isGrounded;
    public Vector3 mousePos; // Target
    public Vector2 relPos;
    public float rot_z;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Walking
        moveHorizontal = Input.GetAxis("Horizontal") * moveSpeed;

        anim.SetFloat("Speed", Mathf.Abs(moveHorizontal));

        if (moveHorizontal > 0)
            transform.rotation = Quaternion.Euler(0, 0, 0);
        else if (moveHorizontal < 0)
            transform.rotation = Quaternion.Euler(0, 180, 0);

        // Jumping
        if (isGrounded && Input.GetAxis("Vertical") > 0 && moveVertical == 0)
        {
            moveVertical = jumpPower; // Input.GetAxis("Vertical") * jumpPower;
            anim.SetBool("isJumping", true);
        } else
        {
            moveVertical = 0;
        }

        // Falling
        if (!isGrounded)
        {
            // TODO: If falling and has umbrella
            // TODO: If falling without umbrella
            // TODO: If rising...taken care of by built-in physics?
        }

        // Moving the arm.
        mousePos = Camera.main.ScreenToWorldPoint(
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z)
            //Input.mousePosition
        );
        // Literally stolen from Reddit. What is a Quaternion, even?
        // magnetFist.transform.rotation = Quaternion.LookRotation(new Vector3(0, 0, 1), (mousePos - transform.position));
        relPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        relPos.Normalize();

        rot_z = Mathf.Atan2(relPos.y, relPos.x) * Mathf.Rad2Deg;
        magnetFist.transform.rotation = Quaternion.Euler(0f, 0f, rot_z);



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
