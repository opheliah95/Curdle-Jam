using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistonController : MonoBehaviour
{
    public bool isLeftPiston;
    public bool isCrushing = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isLeftPiston)
        {
            if (transform.localPosition.x >= 9.25)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
                isCrushing = false;
            }
        }
        else
        {
            if (transform.localPosition.x <= -9.25)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
                isCrushing = false;
            }
        }
    }
}
