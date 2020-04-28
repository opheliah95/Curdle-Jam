using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistonManager : MonoBehaviour
{
    public GameObject leftPiston;
    public GameObject rightPiston;

    public float maxTime;
    public float timer;
    public float speed = 1;

    public float timeLeft;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    
        timeLeft = Mathf.Round(maxTime - timer);
        


        if(GetComponent<Level>().floorCount == LevelBuilder.levels)
            timer += Time.deltaTime;

        if (timer >= maxTime)
        {
            leftPiston.GetComponent<PistonController>().isCrushing = true;
            rightPiston.GetComponent<PistonController>().isCrushing = true;

            leftPiston.GetComponent<Rigidbody2D>().velocity = new Vector3(2, 0, 0) * speed;
            rightPiston.GetComponent<Rigidbody2D>().velocity = new Vector3(-2, 0, 0) *  speed;
        }
    }
}
