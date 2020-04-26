using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    private Rigidbody2D rb;
    //public float fallSpeed;
    public BlockProperty blockProperty;

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
                break;
            case BlockProperty.Stuck:
                // Can no longer be magnetised.
                // Also sticky (If not, use a new state, StuckAndSticky, but eh.
                break;
        }
    }

    void StickTo(GameObject other)
    {

    }

    void SetState(BlockProperty state)
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
