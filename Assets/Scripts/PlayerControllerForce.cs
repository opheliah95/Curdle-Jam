using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerForce : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;

    public Transform groundCheck;
    public LayerMask whatIsGround;
    public bool isGrounded;
    public float groundedRadius;

    public float startSpeed;
    public float moveSpeed;
    public float stickyTimer;
    public float stickySlow;
    public float jumpPower;
    public bool preciseMove;
    // public float airSpeed;

    public float moveHorizontal;
    public float moveVertical;

    public StickyState stickyState;

    public enum StickyState
    {
        Sticky,
        Clean
    }

    /*
    public GameObject magnetFist;
    private Vector3 mousePos;
    private Vector2 relPos;
    private float rot_z;
    public float magnetStrength;
    */

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        moveSpeed = startSpeed;
        stickyState = StickyState.Clean;
    }

    void FixedUpdate()
    {
        // Stickiness handling
        switch (stickyState)
        {
            case StickyState.Clean:
                moveSpeed = startSpeed;
                break;
            case StickyState.Sticky:
                moveSpeed = startSpeed / stickySlow;
                Invoke("Clean", stickyTimer);
                break;
        }

        // Grounded check
        bool wasGrounded = isGrounded;
        isGrounded = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, whatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                isGrounded = true;

                if (!wasGrounded)
                {
                    anim.SetBool("Jumping", false);
                }
                break;
            }
        }


        // Walking
        moveHorizontal = Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime;

        anim.SetFloat("WalkingLeft", moveHorizontal);
        anim.SetFloat("WalkingRight", moveHorizontal);

        // Precision stopping
        if (moveHorizontal == 0 && isGrounded && preciseMove)
            rb.velocity = new Vector2(0, rb.velocity.y);


        // Jumping
        if (isGrounded && Input.GetAxisRaw("Vertical") > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);  // Reset vert movement so there's no carry-over
            moveVertical = jumpPower;
            anim.SetBool("Jumping", true);
        }
        else
        {
            moveVertical = 0;
        }

        /*
        // Falling
        {
            if (!isGrounded)
            {
                // TODO: If falling and has umbrella
                // TODO: If falling without umbrella
                // TODO: If rising...taken care of by built-in physics?
            }
        }
        */

        /*
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
        */

        // Apply determined forces
        if (!preciseMove)
        {
            rb.AddForce(new Vector2(moveHorizontal, 0));
        } else
        {
            rb.velocity = new Vector2(moveHorizontal, rb.velocity.y);
        }
        rb.AddForce(new Vector2(0, moveVertical), ForceMode2D.Impulse);
    }

    public void Sticky()
    {
        stickyState = StickyState.Sticky;
    }

    public void Clean()
    {
        stickyState = StickyState.Clean;
    }

    /*
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
    */
}
