using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Canvas canvas;
    public string sceneName;
    GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").gameObject;
    }

    public void GameOver()
    {


        Destroy(player);
        canvas.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneName);
    }
}
