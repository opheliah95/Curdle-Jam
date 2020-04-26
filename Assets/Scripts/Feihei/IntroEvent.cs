using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroEvent : MonoBehaviour
{
    public Camera cam1;
    public Camera cam2;
    public Texture2D t1;
    public Texture2D t2;

    public Image transition;
    public GameObject garbageDischarge;
    public GameObject garbageobjects;
    Coroutine curEvent;
    // Start is called before the first frame update
    void Start()
    {
        curEvent = StartCoroutine(Event1());
        transition.material.SetFloat("_Cutoff", 0);
        transitioningOverlay = transition.StartCoroutine(TransitioningOverlay(true, 2,t1));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   
    IEnumerator Event1()
    {

        cam1.enabled = true;
        cam2.enabled = false;

        bool done = false;
        float time = 0;
        while (!done)
        {
            time += Time.deltaTime;
            print("1");
            if(time >= 2)
            {
                done = true;
            }
            yield return null;
        }

        done = false;
        time = 0;
        GameObject bear = Instantiate(garbageobjects);
        bear.transform.position = garbageDischarge.transform.position;
        bear.GetComponent<Rigidbody2D>().velocity = new Vector2(-1,0) * 10;

        while (!done)
        {
            print("2");
            time += Time.deltaTime;

            if (time >= 1.5)
            {
                transitioningOverlay = transition.StartCoroutine(TransitioningOverlay(false, 2,t2));
        
            }
            if (time >= 2)
            {
                done = true;
            }
            yield return null;
        }

        cam2.enabled = true;
        cam1.enabled = false;
        transitioningOverlay = transition.StartCoroutine(TransitioningOverlay(true, 2,t1));


        done = false;
        time = 0;

        while (!done)
        {
            print("2");
            time += Time.deltaTime;

            if (time >= 3)
            {
                transitioningOverlay = transition.StartCoroutine(TransitioningOverlay(false, 2, t2));
            }
            if (time >= 4)
            {
                done = true;
            }
            yield return null;
        }


        //to next Scene
        curEvent = null;
    }

    Coroutine transitioningOverlay = null;
    IEnumerator TransitioningOverlay(bool show,float speed, Texture2D transitionEffect)
    {
        float targVal = show ? 1 : 0;
        float curVal = transition.material.GetFloat("_Cutoff");
        transition.material.SetTexture("_AlphaTex", transitionEffect);
        while (curVal != targVal)
        {
            curVal = Mathf.MoveTowards(curVal, targVal, speed * Time.deltaTime);
            transition.material.SetFloat("_Cutoff", curVal);
            yield return new WaitForEndOfFrame();
        }

        transitioningOverlay = null;
    }
}
