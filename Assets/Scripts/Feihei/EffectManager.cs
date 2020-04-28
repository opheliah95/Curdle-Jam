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
        audiosource.volume = vol;
        audiosource.PlayOneShot(sfx);
                
    }



    //don't use this
    public static IEnumerator loopSound(string sfxName, float duration = 5f, float vol = 1 )
    {
        AudioClip sfxl = Resources.Load("Audio/" + sfxName) as AudioClip;

        AudioSource audiosourcel = new GameObject().AddComponent<AudioSource>();
        audiosourcel.name = "soundplayerloop";
        audiosourcel.loop = true;
        audiosourcel.clip = sfxl;
        audiosourcel.volume = vol;
        audiosourcel.Play();
        yield return new WaitForSeconds(duration);
        audiosourcel.Stop();
        audiosourcel.loop = false;
        
    }



}




    

