using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{

    public GameObject[] spawnable;
    public float chance;
    // Start is called before the first frame update
    void Start()
    {
        float roll = Random.Range(0, 100);

        if(roll < chance)
        {
            GameObject obj = Instantiate(spawnable[Random.Range(0, spawnable.Length)]);
            obj.transform.position = transform.position;
        }



    }

    // Update is called once per frame
    void Update()
    {
        
    }
}




