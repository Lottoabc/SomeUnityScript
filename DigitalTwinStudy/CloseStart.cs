using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseStart : MonoBehaviour
{
    public Animation StartAnimation;
    public GameObject StartUI;
   
    public void CloseUI()
    {
        StartUI.SetActive(false);
        StartAnimation.Play("StartAnimation");
    }
}
