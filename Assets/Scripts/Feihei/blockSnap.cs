using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockSnap : MonoBehaviour
{
    Rigidbody2D rb;
    public Coroutine snapGrid;
    bool snapAble = true;
    bool overlapOtherbox = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        if (!overlapOtherbox)
        {
            if (rb.velocity.magnitude < 0.05f)
            {
                if (snapAble)
                {
                    if (snapGrid == null)
                    {
                        print("snap");
                        snapGrid = StartCoroutine(snap(transform.position, this.gameObject));
                    }
                }
            }
            else
            {
                snapAble = true;
            }


        }
      

        overlapOtherbox = false;

        

    }

    public void manualSnap(Vector2 toPos, GameObject objToSnap)
    {
       
         
            
                    if (snapGrid == null)
                    {
                        print("snap");
                        snapGrid = StartCoroutine(snap(toPos, objToSnap));
                }
               
               
    }

    public IEnumerator snap(Vector2 toPos, GameObject objToSnap)
    {
        float ToPosX;
        ToPosX = Mathf.Round(toPos.x - 0.5f);

        bool move = true;
        while (move)
        {
            objToSnap.transform.position = Vector2.MoveTowards(objToSnap.transform.position, new Vector2(ToPosX + 0.5f, objToSnap.transform.position.y), 0.2f);

            if (objToSnap.transform.position.x == ToPosX + 0.5f)
            {
                move = false;
            }
            else
            {
                yield return null;
            }

        }
        print("s");
        snapAble = false;
        snapGrid = null;


    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<BlockController>())
        {
            if (rb.Distance(other.collider).isOverlapped)
            {
                overlapOtherbox = true;
            }
            
        }


    }
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<BlockController>())
        {
            snapAble = true;
           overlapOtherbox = false;
            
        }


    }

}
