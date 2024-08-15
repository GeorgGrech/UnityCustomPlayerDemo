using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private List<AudioSource> audioSource = new List<AudioSource>();
    [SerializeField] private AudioSource clickSound;


    public void Awake()
    {
        instance = this;
    }


    public void playSound(int index)
    {

        if (audioSource[index].isPlaying == false)
        {
            audioSource[index].Play();

        }

    }
    public void StopSound(AudioSource audioToStop)
    {
        if (audioToStop.isPlaying == true)
        {
            audioToStop.Stop();

        }

    }



    public void Update()
    {
        if (Input.GetMouseButton(0))
        {
            //clickSound.Play();

        }
    }
}
