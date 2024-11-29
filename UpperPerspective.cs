using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpperPerspective : MonoBehaviour
{
    public GameObject CameraL;
    public GameObject MainCamera;
    public GameObject Transform123;
    // Start is called before the first frame update

    // Update is called once per frame

    public void OnMouseDown()
    {
        Invoke("AwaitOnMouseDown", 0.1f);
    }

    public void AwaitOnMouseDown()
    {
        if(!MainCamera.GetComponent<CameraMove>().isMoving) //对isMoving进行判断，使其移动完成才进行赋值
        {
            if (MainCamera.transform.position.y <= 120)     //如果摄像机所在高度＜120
            {
                Transform123.transform.position = MainCamera.transform.position;    //将摄像机的高度传给transform123
                Transform123.transform.rotation = MainCamera.transform.rotation;    //将摄像机的旋转传给transform123
                MainCamera.SendMessage("OnMouseMove", CameraL.transform);   //将CameraL的位置信息发送给OnMouseMove方法
            }
            else
            {
                MainCamera.SendMessage("OnMouseMove", Transform123.transform);  //将transform123对位置信息发给OnMouseMove，传送到存储的位置上
            }
        }
        
    }
}
