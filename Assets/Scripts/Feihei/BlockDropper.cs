using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDropper : MonoBehaviour
{
    public float maxX;
    public float minX;

    public float timeCooldown = 2;
    public float curTimeCooldown;

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
        curTimeCooldown = timeCooldown;

        //that's a lot of else if lol - by feihei
        if(LevelBuilder.levels >= 5)
        {
            curTimeCooldown = timeCooldown - (timeCooldown / 10);
        }
        else if (LevelBuilder.levels >= 10)
        {
            curTimeCooldown = timeCooldown - (timeCooldown / 9);
        }
        else if (LevelBuilder.levels >= 15)
        {
            curTimeCooldown = timeCooldown - (timeCooldown / 8);
        }
        else if (LevelBuilder.levels >= 20)
        {
            curTimeCooldown = timeCooldown - (timeCooldown / 6);
        }
        else if (LevelBuilder.levels >= 25)
        {
            curTimeCooldown = timeCooldown - (timeCooldown / 5);
        }


        time -= Time.deltaTime;

        if(time <= 0)
        {
            GameObject obj = Instantiate(objToSpawn[Random.Range(0, objToSpawn.Count)]);
            obj.transform.position = new Vector2(Random.Range(minX, maxX), LevelBuilder.curLevel.transform.position.y + 15);
            if (obj.GetComponent<blockSnap>())
            {
                obj.GetComponent<blockSnap>().manualSnap(obj.transform.position,obj);
            }
            time = curTimeCooldown;
        }
    }
}
