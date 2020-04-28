using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class test : MonoBehaviour
{
    public GameObject cam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            AudioManager.playSound("Hit_01"); //You can call a sound just by using the file name. can be call anywhere : AudioManager.playSound(SoundName,volume);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            //CameraEffect.camshaking = StartCoroutine(CameraEffect.Shake(cam)); //shake effect : Shake(CameraObject,duration,speed,strength) : technically you can use it to shake any object but there might be a problem if the object is moving so main camera in my LevelTestScene now is in the holder -by feihei
        

        }

    }
}
