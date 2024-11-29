using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public Animation QuitAnimation;
    public GameObject EnumUI;

    public void QuitTheGame()
    {
        EnumUI.SetActive(false);
        QuitAnimation.Play("ExitGame");
        QuitThisGame();
       
    }

    public void QuitThisGame()
    {
        Invoke("QuitThisGame1", 1f);
    }

    public void QuitThisGame1()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;    //在Unity环境下关闭app
#else
        Application.Quit();
#endif
    }
}
