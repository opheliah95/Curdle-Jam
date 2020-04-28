using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleUI : MonoBehaviour
{
    public Text levelText;
    public Text scoreText;
    public Text timer;


    public Image transition; //image you want to use for transition effect
    public Texture2D t1; //this is a bit abstract i'll explain in the next picture
    Coroutine transitioningOverlay = null;

    void Start()
    {
        transition.gameObject.SetActive(true);
        transition.material.SetFloat("_Cutoff", 0);

        transitioningOverlay = transition.StartCoroutine(TransitioningOverlay(true, 2, t1));
    }


    void Update()
    {
        levelText.text = LevelBuilder.levels.ToString();
        scoreText.text = LevelBuilder.score.ToString();
    }

    
    //You need to use this Corutine 
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
