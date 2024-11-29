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
        if(!MainCamera.GetComponent<CameraMove>().isMoving) //��isMoving�����жϣ�ʹ���ƶ���ɲŽ��и�ֵ
        {
            if (MainCamera.transform.position.y <= 120)     //�����������ڸ߶ȣ�120
            {
                Transform123.transform.position = MainCamera.transform.position;    //��������ĸ߶ȴ���transform123
                Transform123.transform.rotation = MainCamera.transform.rotation;    //�����������ת����transform123
                MainCamera.SendMessage("OnMouseMove", CameraL.transform);   //��CameraL��λ����Ϣ���͸�OnMouseMove����
            }
            else
            {
                MainCamera.SendMessage("OnMouseMove", Transform123.transform);  //��transform123��λ����Ϣ����OnMouseMove�����͵��洢��λ����
            }
        }
        
    }
}
