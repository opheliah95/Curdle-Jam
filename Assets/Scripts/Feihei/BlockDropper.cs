using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDropper : MonoBehaviour
{
    public float maxX;
    public float minX;

    public float timeCooldown = 2;

    public List<GameObject> objToSpawn;
    float time;
    // Start is called before the first frame update
    void Start()
    {
        time = timeCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;

        if(time <= 0)
        {
            GameObject obj = Instantiate(objToSpawn[Random.Range(0, objToSpawn.Count)]);
            obj.transform.position = new Vector2(Random.Range(minX, maxX), (LevelBuilder.levels * 6)+10);
            if (obj.GetComponent<blockSnap>())
            {
                obj.GetComponent<blockSnap>().manualSnap(obj.transform.position,obj);
            }
            time = timeCooldown;
        }
    }
}
