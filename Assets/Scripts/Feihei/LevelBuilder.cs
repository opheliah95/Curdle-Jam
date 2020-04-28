using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    bool firstlevel = true;
    public static bool clearLevel;
    public Camera cam;
    float curHeight;
    public static int levels;
    public int levelCount;
    List<GameObject> allLevels = new List<GameObject>();

    public GameObject[] levelobj;
    public GameObject curHighestLevel;

    public float curlevelHeight; 

    Coroutine moveCam;
    // Start is called before the first frame update
    void Start()
    {
        // GameObject lv = Instantiate(levelobj);
        //lv.transform.position = Vector2.zero;

        //curLevel = lv;
        //levels = 1;
        curLevel = null;
        firstlevel = true;
        levels = 0;
        levelCount = 1;
        //curHeight = -6;
        buildNextLevel();
        buildNextLevel();
  
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            buildNextLevel();
        }

        if (clearLevel)
        {
            print("cl");
            levels += 1;
            buildNextLevel();
            clearLevel = false;
        }

        if(allLevels.Count > 4)
        {
            GameObject toDestroy = allLevels[0];
            allLevels.RemoveAt(0);
            Destroy(toDestroy);
        }
    }

    public static GameObject curLevel;
    public static void GetLevel(GameObject lvObj)
    {
        curLevel = lvObj;
    }
    void buildNextLevel()
    {
        //curLevel.GetComponent<>
       
        moveCam = StartCoroutine(moveCamera());
       
        curHeight += curlevelHeight;
        GameObject lv = Instantiate(levelobj[Random.Range(0,levelobj.Length)]);
        lv.gameObject.GetComponent<Level>().floorCount = levelCount;
        levelCount++;
        curHighestLevel = lv;
        curlevelHeight = curHighestLevel.GetComponent<Level>().levelHeight;
        lv.transform.position = new Vector2(0,curHeight);
        allLevels.Add(lv);
    }

    //float camHeight = 
    IEnumerator moveCamera()
    {
        bool move = true;

        while (move)
        {
            cam.transform.position = Vector3.MoveTowards(cam.transform.position, new Vector3(cam.transform.position.x, curLevel.transform.position.y, cam.transform.position.z), 0.1f);

            if(cam.transform.position.y >= curHeight)
            {
                move = false;
            }
            yield return null;
        }

        moveCam = null;
    }
}
