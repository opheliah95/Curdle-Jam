using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum PlayerState
    {
        Idle,
        Walking,
        Jumping,
        Falling
    }

    public PlayerState currentState;

    public Rigidbody2D rBody;

    public float moveSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentState)
        {
            case PlayerState.Idle:
                if(Input.GetAxis("Horizontal") != 0)
                    currentState = PlayerState.Walking;

                if (Input.GetAxis("Vertical") > 0)
                    currentState = PlayerState.Jumping;
                break;

            case PlayerState.Walking:
                transform.Translate(new Vector3(Input.GetAxis("Horizontal") * moveSpeed, 0, 0));

                if (Input.GetAxis("Horizontal") == 0)
                    currentState = PlayerState.Idle;

                if (Input.GetAxis("Vertical") > 0)
                    currentState = PlayerState.Jumping;
                break;

            case PlayerState.Jumping:
                transform.Translate(new Vector3(Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime, Input.GetAxis("Vertical") * moveSpeed * 100 * Time.deltaTime, 0));

                currentState = PlayerState.Falling;
                break;

            case PlayerState.Falling:

                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(currentState == PlayerState.Falling)
        {
            //TODO: check collision beneath
            currentState = PlayerState.Idle;
        }
    }
}
