using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strawberry : MonoBehaviour
{
    public List<GameObject> blocks;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (blocks.Count >= 2)
        {
            Debug.Log("will explide");
            anim.SetTrigger("Explode");
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject obj = collision.gameObject;

        if(obj.layer != 13)
        {
            gameObject.GetComponent<Rigidbody2D>().isKinematic = true; // when hit ground berry cannot be pushed
        }

        if(obj.tag == "Player")
        {
            anim.SetTrigger("Explode");
        }

        // check if they are blocks
        if(obj.tag == "Box")
        {
            if(!blocks.Contains(obj)){
                blocks.Add(obj);
            }
        }

        // if they are berries, both die!
        if (obj.tag == "Strawberry")
        {
            anim.SetTrigger("Explode");
            obj.GetComponent<Animator>().SetTrigger("Explode");
        }

        
    }

    public void BerryExplosion()
    {
        Destroy(gameObject);
    }

    
}
