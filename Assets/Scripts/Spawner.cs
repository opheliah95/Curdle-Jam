using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] prefabs;

    public float leftBounds;
    public float rightBounds;
    public float spawnHeight;

    public float spawnTime;
    public float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= spawnTime)
        {
            int prefabIndex = Random.Range(0, prefabs.Length);
            GameObject spawn = prefabs[prefabIndex];
            
            int x = (int)Random.Range(leftBounds + spawn.transform.lossyScale.x / 2, rightBounds - spawn.transform.lossyScale.x / 2);
            Vector3 pos = new Vector3(x, spawnHeight, 0);

            Quaternion rot = Quaternion.Euler(0, 0, 0);

            GameObject.Instantiate(spawn, pos, rot);

            timer = 0;
        }
    }
}
