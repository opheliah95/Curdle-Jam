using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetController : MonoBehaviour
{
    private Vector3 mousePos;
    private Vector2 relPos;
    private float rot_z;

    public float magnetStrength; // Effective
    public float magnetSpeed; // Pull power...I might have fucked up the names.
    private MagnetState magnetState;
    public LayerMask magneticLayer;

    public bool snapWhenMagnet;

    private SpriteRenderer sr;
    public RaycastHit2D hit;

    public GameObject attached;
    public Transform tip;
    // public float distToTarget; // How far away target magnetized thing is
    public float threshold; // how far before we attach the object

    public enum MagnetState
    {
        On,
        Pulling,
        Attached,
        Off
    }

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        magnetState = MagnetState.Off;
    }

    GameObject lasthit = null; //last magnetizable object
    void FixedUpdate()
    {
        // Moving the arm.
        mousePos = Camera.main.ScreenToWorldPoint(
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z)
        );
        relPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        relPos.Normalize();

        rot_z = Mathf.Atan2(relPos.y, relPos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);

        // Magnetism! Magic!
        if (!Input.GetMouseButton(0))
            magnetState = MagnetState.Off;

        switch (magnetState)
        {
            // TODO: Switch the threshold logic to just adding a FixedJoint between gun and box on collision
            case MagnetState.On:
                hit = Physics2D.CircleCast(tip.position, 1, relPos, magnetStrength, magneticLayer);
                if (hit && hit.rigidbody.gameObject.GetComponent<BlockController>().blockProperty != BlockController.BlockProperty.Stuck)
                {    
                    //magnetState = MagnetState.Pulling;
                    if (hit.distance > threshold)
                    {
                        // Pull!
                        // TODO: Add pulling particles?
                        hit.rigidbody.velocity = -relPos * magnetSpeed; // magnetStrength;
                        hit.rigidbody.gravityScale = 0; // Gravity removal, so it keeps getting pulled. Physics be damned.
                    } else
                    {
                        // Sticky sticky!
                        attached = hit.rigidbody.gameObject;
                        magnetState = MagnetState.Attached;
                    }
                }
              
                break;
            //case MagnetState.Pulling:
                //break;
            case MagnetState.Attached:
                //hit.rigidbody.velocity = Vector2.zero;
                // TODO: Add attached particles to box?
                attached.transform.parent = transform.parent.transform; // ...just transform?
                attached.GetComponent<Rigidbody2D>().gravityScale = 0f; // block have stay float -by feihei
                //attached.transform.position = tip.position;

                //part of Snap code -by feihei
                attached.layer = LayerMask.NameToLayer("GrabbedBox"); //there's a lot of prlblem when player able to collide box when it's attached so I make it so it won't - by feihei
                attached.GetComponent<blockSnap>().snapGrid = null;
                if (attached.GetComponent<blockSnap>() && snapWhenMagnet)
                {
                   
                   attached.transform.position = new Vector2(attached.transform.position.x, tip.position.y);
                   attached.GetComponent<blockSnap>().manualSnap(tip.position, attached);
                                                    
                }
                else
                {
                    attached.transform.position = tip.position;
                }
                break;
         
            case MagnetState.Off:        
                    if (Input.GetMouseButton(0))
                    magnetState = MagnetState.On;
                if (attached)
                {
                    attached.layer = attached.GetComponent<BlockController>().orgLayer;
                    attached.transform.parent = null;
                    attached.GetComponent<Rigidbody2D>().gravityScale = 15f; // reset gravity. Hard coding is smart, yes? :(
                }
                break;
        }
    }
}
