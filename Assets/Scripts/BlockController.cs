using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    Rigidbody2D rb;

    float fallSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        fallSpeed = 100.0f;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector3(0, -fallSpeed * Time.deltaTime, 0);
    }
}
