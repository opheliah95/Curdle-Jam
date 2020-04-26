using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField]
    int totalTime = 50; // how long you want each level to last, you can edit it in editor btw...

    [SerializeField]
    private int currentTime;

    [SerializeField]
    private float tempTime; // need to convert to seconds...
    [SerializeField]
    private Text uiText;

    [SerializeField]
    private int secondsPassed;


    // Start is called before the first frame update
    void Start()
    {
        currentTime = totalTime; // set timer at the start of the level
        uiText = GetComponent<Text>();
        uiText.text = currentTime.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTime > 0)
            countDown();
        else
        {
            currentTime = 0;
            uiText.text = "Game over...";
            HealthVisual.heartHealthSystem.Damage(10);/// you are definitely dead...
        }
            
    }


    void countDown()
    {
        tempTime += Time.deltaTime;
        secondsPassed = (int)(tempTime % 60);

        if(secondsPassed != 0)
        {
            currentTime -= 1;
            tempTime = 0;
        }

        uiText.text = currentTime.ToString();
    }
}
