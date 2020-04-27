using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    private Rigidbody2D rb;
    //public float fallSpeed;
    public BlockProperty blockProperty;
    public bool makeKinematic;

    public LayerMask orgLayer;

    public enum BlockProperty
    {
        Default,
        Sticky,
        Stuck
    }

    // Start is called before the first frame update
    void Start()
    {
  
        //gameObject.layer; // Already exists, also probably not useful?
        rb = GetComponent<Rigidbody2D>();
        //fallSpeed = 10.0f;
        rb.gravityScale = 15.0f;


        orgLayer = gameObject.layer;

        makeKinematic = true;

    }

    // Update is called once per frame

    void FixedUpdate()
    {


        //rb.velocity = new Vector3(0, -fallSpeed * Time.deltaTime, 0);

        // This is only necessary in collisions...
        switch (blockProperty)
        {
            case BlockProperty.Default:
                // Regular physics. If not attached, fall. If attached, don't.
                // Debug
                GetComponent<SpriteRenderer>().color = Color.white;
                break;
            case BlockProperty.Sticky:
                // Stick to other blocks
                // Debug
                GetComponent<SpriteRenderer>().color = Color.red;
                break;
            case BlockProperty.Stuck:
                // Can no longer be magnetised.
                // Also sticky (If not, use a new state, StuckAndSticky, but eh.
                // Debug
                GetComponent<SpriteRenderer>().color = Color.magenta;
                break;
        }

     

        
   
    }

    // TODO: Should stickiness be a separate script? It does work differently from box to player, but still...
    void StickTo(GameObject other)
    {
        SetState(BlockProperty.Stuck);
        other.GetComponent<BlockController>().SetState(BlockProperty.Stuck);
        // TODO: If stuck and on magnet, also detach.

        if (makeKinematic)
        {
            // Freeze; kinematic
            rb.isKinematic = true;
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0;
        }
        else
        {
            // Join; adds a joint to tie them together instead.
            FixedJoint2D joint = gameObject.AddComponent<FixedJoint2D>();
            joint.connectedBody = other.GetComponent<Rigidbody2D>();
        }

    }

    void Sticky() // Just to shorthand some stuff
    {
        SetState(BlockProperty.Sticky);
    }

    void Stuck()
    {
        SetState(BlockProperty.Stuck);
    }

    void SetState(BlockProperty state)
    {
        blockProperty = state;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        
     
        if (other.gameObject.GetComponent<BlockController>()
            && (blockProperty == BlockProperty.Sticky || blockProperty == BlockProperty.Stuck))

        if (blockProperty == BlockProperty.Sticky || blockProperty == BlockProperty.Stuck)

        {
            if (other.gameObject.GetComponent<BlockController>())
            //&& (blockProperty == BlockProperty.Sticky || blockProperty == BlockProperty.Stuck))
            {
                // Other blocks, wood or metal
                StickTo(other.gameObject);
                //SetState(BlockProperty.Stuck);
                //other.gameObject.GetComponent<BlockController>().SetState(BlockProperty.Stuck);


            }
            else if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))     //CompareTag("Ground"))
            // TODO: Give things correct tags? Or just use above line.
            // TODO: D R Y
            {
                // Floors
                SetState(BlockProperty.Stuck);
                rb.isKinematic = true;
                rb.velocity = Vector2.zero;
                rb.angularVelocity = 0;

            }
            else if (other.gameObject.layer == LayerMask.NameToLayer("Player"))    //CompareTag("Player"))
            {
                // Player
                other.gameObject.GetComponent<PlayerControllerForce>().Sticky();
            }
        }
        
    }
}
