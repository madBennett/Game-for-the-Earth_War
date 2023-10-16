using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button_Behavior : MonoBehaviour
{
    //Object References
    private GameManager gm;
    private Played_Cards played_Cards;

    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        played_Cards = FindObjectOfType<Played_Cards>();
    }

    public void changeSceneTo(int sceneId)
    {
        SceneManager.LoadScene(sceneId);
    }

    public void simWinRound(bool isPlayer)
    {
        if (isPlayer)
        {
            //
        }
        else
        {
            //
        }
    }

    public void simWinOverall(bool isPlayer)
    {
        if (isPlayer)
        {
            gm.overAllPlayerWin();
        }
        else
        {
            Debug.Log("Called");
            gm.overAllAlienWin();
        }
    }
}
