using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookSlogan : MonoBehaviour
{
    public GameObject MainCamera;
    public GameObject CameraL;
    // Start is called before the first frame update
    private void Awake()
    {
        MainCamera = GameObject.Find("MainCamera"); //寻找叫MainCamera的组件
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.LookAt(MainCamera.transform.position);     //使组件始终面向摄像机
    }

    public void OnMouseDown()
    {
        MainCamera.SendMessage("OnMouseMove", CameraL.transform);   //将CameraL的位置信息发送给OnMouseMove方法
    }

}
