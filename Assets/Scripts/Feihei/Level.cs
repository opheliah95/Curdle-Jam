using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public Transform bottomMark;
    public GameObject platform;

    public float levelHeight;

    bool cleared;
    GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (!cleared)
        {
            if (player.transform.position.y > bottomMark.position.y)
            {
                LevelBuilder.GetLevel(this.gameObject);
                print("clear");
                platform.layer = LayerMask.NameToLayer("Default");
                LevelBuilder.clearLevel = true;
                cleared = true;
            }
        }
       
    }
}
