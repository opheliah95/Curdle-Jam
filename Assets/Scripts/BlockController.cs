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
    }

    // Update is called once per frame

    void FixedUpdate()
    {
        //rb.velocity = new Vector3(0, -fallSpeed * Time.deltaTime, 0);

        switch (blockProperty)
        {
            case BlockProperty.Default:
                // Regular physics. If not attached, fall. If attached, don't.
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

    IEnumerator delayedDestroy()
    {
        yield return new WaitForSeconds(0.5f);

        Destroy(gameObject);

        yield return null;
    }

    void StickTo(GameObject other)
    {

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
        {
            // Other blocks, wood or metal
            SetState(BlockProperty.Stuck);
            other.gameObject.GetComponent<BlockController>().SetState(BlockProperty.Stuck);
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
                gameObject.AddComponent<FixedJoint2D>();
            }

        } else if (true)
        {
            // Floors
        } else if (true)
        {
            // Player
        }

        if(other.gameObject.tag == "Piston" && other.gameObject.GetComponent<PistonController>().isCrushing)
            StartCoroutine(delayedDestroy());
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
