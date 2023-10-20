using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button_Behavior : MonoBehaviour
{
    public void changeSceneTo(int sceneId)
    {
        SceneManager.LoadScene(sceneId);
    }
}
