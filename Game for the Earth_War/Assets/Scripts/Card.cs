using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour
{
    [Header("Inscribed")]

    private Vector3 ogPos;
    private Vector3 bouncePos;
    private Vector3 movePos;//maybe make public???  for war occurance
    private bool bounced = false;
    private bool isMouseOver = false;
    
    public Vector3 startingPos;
    public float bounceHeight = 0.25f;
    public int num;
    public string suit;
    public bool isPlayableCard = true;
    public bool faceUp = false;
    
    void Knit()
    {
        startingPos = transform.position;
    }
    

    // Start is called before the first frame update
    void Start()
    {
        ogPos = transform.position;
        bouncePos = ogPos;
        bouncePos.y += bounceHeight;
    }

    public bool wasMouseOver()
    {
        return isMouseOver;
    }

    public void setPos(Vector3 inMovePos)
    {
        ogPos = inMovePos;
        bouncePos = ogPos;
        bouncePos.y += bounceHeight;
    }

    public void moveCard(Vector3 inMovePos)
    {
        this.transform.position = inMovePos;
    }
    public void flip()
    {
        Quaternion rotation = transform.rotation;
        rotation.y += 180f;

        transform.rotation = rotation;

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
                this.transform.position = ogPos;
                bounced = false;
            }

            isMouseOver = false;
        }
    }
}
