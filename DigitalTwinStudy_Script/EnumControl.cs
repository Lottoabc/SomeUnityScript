using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnumControl : MonoBehaviour
{
    public Animation CanvasAnimation;
    private bool value = true;
    // Start is called before the first frame update
    public void LaunchAnimation()
    {
        if (value)
        {
            CanvasAnimation.Play("enumShow");
        }
        else
        {
            CanvasAnimation.Play("enumFade");
        }

        value = !value;     //!value表示为取反
    }
}
