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
        MainCamera = GameObject.Find("MainCamera"); //Ѱ�ҽ�MainCamera�����
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.LookAt(MainCamera.transform.position);     //ʹ���ʼ�����������
    }

    public void OnMouseDown()
    {
        MainCamera.SendMessage("OnMouseMove", CameraL.transform);   //��CameraL��λ����Ϣ���͸�OnMouseMove����
    }

}
