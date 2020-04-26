using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class infiniteFallBackground : MonoBehaviour
{
    GameObject background;
    public float speed = 0.1f;
    Vector2 orgPos;
    // Start is called before the first frame update
    void Start()
    {
        background = this.gameObject;
        orgPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        background.transform.position = Vector2.MoveTowards(background.transform.position, new Vector2(background.transform.position.x, background.transform.position.y + 10), speed);

        if(background.transform.position.y >= 10)
        {
            background.transform.localPosition = orgPos;
        }
    }
}
