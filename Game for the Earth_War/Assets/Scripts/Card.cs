using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour
{
    [Header("Inscribed")]

    private Vector3 pos;
    private Vector3 bouncePos;
    private bool bounced = false;
    private bool isMouseOver = false;
    private float volume = 1f;
    
    public AudioSource audioSource;

    public float bounceHeight = 0.25f;
    public int num;
    public int suit;
    public bool isPlayableCard = true;
    public bool faceUp = true;
    public AudioClip cardFlip;

    void Start()
    {
        pos = this.transform.position;
        bouncePos = pos;
        bouncePos.y += bounceHeight;
    }

    public void setVolume(float inVol)
    {
        volume = inVol;
    }

    public bool wasMouseOver()
    {
        return isMouseOver;
    }

    public void move(Vector3 inMovePos)
    {
        this.transform.position = inMovePos;
        pos = inMovePos;
        bouncePos = pos;
        bouncePos.y += bounceHeight;
    }

    public void flip()
    {
        if (faceUp)
        {
            this.transform.RotateAround(transform.position, Vector3.up, 180);
            audioSource.PlayOneShot(cardFlip, volume);
        }
        else
        {
            this.transform.RotateAround(transform.position, Vector3.up, -180);
            
        }
        faceUp = !faceUp;
    }

    void OnMouseOver()
    {
        if (isPlayableCard)
        {
            //bounce on hover            
            this.transform.position = bouncePos;
            bounced = true;

            isMouseOver = true;
        }
    }

    void OnMouseExit()
    {
        if (isPlayableCard)
        {
            if (bounced)
            {
                this.transform.position = pos;
                bounced = false;
            }

            isMouseOver = false;
        }
    }
}
