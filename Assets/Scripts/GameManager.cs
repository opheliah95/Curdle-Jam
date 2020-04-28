using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Canvas canvas;
    public string sceneName;

   
    public void GameOver()
    {
        Time.timeScale = 0;
        canvas.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(sceneName);
    }
}
