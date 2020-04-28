using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class TitleFunctions : MonoBehaviour
{
    public Image transition;
    public Texture2D t1;

    Coroutine transitioningOverlay = null;
    // Start is called before the first frame update
    void Awake()
    {
        //curEvent = StartCoroutine(Event1());
        transition.gameObject.SetActive(true);
        transition.material.SetFloat("_Cutoff", 0);
        transition.StartCoroutine(TransitioningOverlay(true, 2, t1));
    }

    // Update is called once per frame

    public void startGame()
    {
        AudioManager.playSound("Menu_Sound_01");
        starting = StartCoroutine(startingGame());
    }
    Coroutine starting = null;
    IEnumerator startingGame()
    {
        transitioningOverlay = transition.StartCoroutine(TransitioningOverlay(false, 2, t1));
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Intro");
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
