using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour
{
    [Header("Inscribed")]

    private Vector3 ogPos;
    private Vector3 pos;
    private Vector3 bouncePos;
    private bool bounced = false;
    private bool isMouseOver = false;
    //private Rigidbody rigidBody;

    public float bounceHeight = 0.25f;
    public int num;
    public int suit;
    public bool isPlayableCard = true;
    public bool faceUp = true;

    void Start()
    {
        //ogPos = new Vector3(-50, -50, 0);
        pos = this.transform.position;
        bouncePos = pos;
        bouncePos.y += bounceHeight;
        //rigidBody = gameObject.GetComponent<Rigidbody>();//rigidbody get
    }

    public void setOgPosAtCurPos()
    {
        ogPos = this.transform.position;
    }

    public bool wasMouseOver()
    {
        return isMouseOver;
    }

    public void moveToOgPos()
    {
        this.transform.position = ogPos;
    }

    public void move(Vector3 inMovePos)
    {
        this.transform.position = inMovePos;
        pos = inMovePos;
        bouncePos = pos;
        bouncePos.y += bounceHeight;
    }

    public void flip()//not working in realtime gameplay
    {
        if (faceUp)
        {
            this.transform.RotateAround(transform.position, Vector3.up, 180);
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
