using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class honey : MonoBehaviour
{
    bool picked;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (!picked)
            {
                LevelBuilder.score += 50;
                AudioManager.playSound("PickUp");
                picked = true;
                Destroy(gameObject);
            }

        }
    }
}
