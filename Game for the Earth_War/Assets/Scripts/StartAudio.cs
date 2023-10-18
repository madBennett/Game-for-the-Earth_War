using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAudio : MonoBehaviour
{
    //Object Reference
    public AudioSource audioSource;

    //Audio
    public bool isStart = false;
    public bool isMuted = false;

    void Start()
    {
        if (isStart)
        {
            audioSource.Play();
        }
    }

    void Update()
    {
        //mute background if space pressed
        if (isStart && Input.GetKeyDown(KeyCode.Space))
        {
            isMuted = !isMuted;
            audioSource.mute = isMuted;
        }
    }
}
