using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverAudio : MonoBehaviour
{
    //Object Reference
    public AudioSource audioSource;

    //Audio
    public float volume = 1f;
    public AudioClip playerWin;
    public AudioClip alienWin;

    public bool isStart = false;
    public bool isPlayerWin = false;
    public bool isMuted = false;

    void Start()
    {
        if (isPlayerWin)
        {
            audioSource.PlayOneShot(playerWin, volume);
        }
        else
        {
            audioSource.PlayOneShot(alienWin, volume);
        }
    }
}
