using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleUI : MonoBehaviour
{
    public Text levelText;
    public Text scoreText;
    public Text timer;


    public Image transition;
    public Texture2D t1;
    Coroutine transitioningOverlay = null;
    // Start is called before the first frame update
    void Start()
    {
        transition.gameObject.SetActive(true);
        //curEvent = StartCoroutine(Event1());
        transition.material.SetFloat("_Cutoff", 0);
        transitioningOverlay = transition.StartCoroutine(TransitioningOverlay(true, 2, t1));
    }

    // Update is called once per frame
    void Update()
    {
        levelText.text = LevelBuilder.levels.ToString();
        scoreText.text = LevelBuilder.score.ToString();
    }
    IEnumerator TransitioningOverlay(bool show, float speed, Texture2D transitionEffect)
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
