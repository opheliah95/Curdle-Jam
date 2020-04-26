using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetController : MonoBehaviour
{
    private Vector3 mousePos;
    private Vector2 relPos;
    private float rot_z;
    public float magnetStrength;

    public LayerMask magneticLayer;

    private SpriteRenderer sr;
    public bool ray;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
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
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);

        // Magnetism! Magic!
        // TODO: On button press, not just auto.
        ray = Physics2D.Raycast(transform.position, relPos, magnetStrength, magneticLayer);
        Debug.DrawRay(transform.position, relPos * magnetStrength, Color.white);

        if (ray)
        {
            sr.color = Color.yellow;
        } else
        {
            sr.color = Color.white;
        }

    }
}
