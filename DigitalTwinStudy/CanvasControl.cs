using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasControl : MonoBehaviour
{
    public Animation CanvasAnimation;
    private bool value = false;
    // Start is called before the first frame update
    public void LaunchAnimation()
    {
        Debug.Log("受到点击");
        if (value)
        {
            CanvasAnimation.Play("CanvasAnimation1");
        }
        else
        {
            CanvasAnimation.Play("CanvasAnimation");
        }

        value = !value;     //!value表示为取反
    }
}
