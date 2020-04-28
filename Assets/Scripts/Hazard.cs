using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    // using protected here just in case you want to do inheritance...
    [SerializeField]
    protected int damage = 1;

    protected bool hasDamaged;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && !hasDamaged)
        {
            HealthVisual.heartHealthSystem.Damage(damage);
            hasDamaged = true;
        }
    }

    

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
          
            hasDamaged = false;
        }
    }
}
