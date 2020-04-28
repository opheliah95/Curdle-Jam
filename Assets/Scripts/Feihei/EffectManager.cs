using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager
{
    static AudioClip sfx;
    //AudioClip music;

    public static AudioSource audiosource;


    public static void playSound(string sfxName, float vol = 1)
    {
        sfx = Resources.Load("Audio/" + sfxName)as AudioClip;
        if(audiosource == null)
        {
            audiosource = new GameObject().AddComponent<AudioSource>();
            audiosource.name = "soundplayer";
        }    
             
            audiosource.PlayOneShot(sfx);
            audiosource.volume = vol;            
    }



    //don't use this
    public static IEnumerator loopSound(string sfxName, float duration = 5f, float vol = 1 )
    {
        AudioClip sfxl = Resources.Load("Audio/" + sfxName) as AudioClip;

        AudioSource audiosourcel = new GameObject().AddComponent<AudioSource>();
        audiosourcel.name = "soundplayerloop";
        audiosourcel.loop = true;
        audiosourcel.clip = sfxl;
        audiosourcel.Play();
        yield return new WaitForSeconds(duration);
        audiosourcel.Stop();
        audiosourcel.loop = false;
        
    }



}

public class CameraEffect
{
    public static Coroutine camshaking;
    //static Camera cam;


    public static IEnumerator Shake(GameObject cam,float duration = 1f, float speed = 0.05f,float strength = 0.3f)
    {            
        bool shaking = true;
        Vector3 orgPos = cam.transform.position;
        while(shaking == true)
        {
            bool move = true;

            while (move)
            {
                Vector3 ToPos = new Vector3(orgPos.x + Random.Range(-strength, strength), orgPos.y + Random.Range(-strength, strength), cam.transform.position.z);
                cam.transform.position = Vector3.MoveTowards(cam.transform.position, ToPos, 1f);
                if(cam.transform.position.x == ToPos.x && cam.transform.position.y == ToPos.y)
                {
                    move = false;
                }
            }
      
            duration -= speed;

            if(duration <= 0)
            {
                cam.transform.position = orgPos;
                shaking = false;
            }

            yield return null;
        }

        camshaking = null;


    }
}
