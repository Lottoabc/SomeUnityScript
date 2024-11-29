using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float SpeedR = 1.5f; //��ת�ı���
    public float SpeedM = 5f;   //�ƶ����ٶ�
    public float SpeedA = 1f;   //���ٵı���
    public float MoveSpeed = 10.0f;
    private float z = 0f;       //����һ������������������������½�
    public bool isMoving = false ;

    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void Move()
    {
        if (Input.GetKey(KeyCode.LeftShift)) { SpeedA = 3.0f; }     //�Ƿ���м��ٵ��ж�
        else { SpeedA = 1.0f; }

        if (Input.GetKey(KeyCode.Q)) { z = -1; }        //�������ж�
        else if (Input.GetKey(KeyCode.E)) { z = 1; }    //�½����ж�
        else { z = 0; }

        if (Input.GetMouseButton(1))    //���յ�����Ҽ�������ʱ
        {
            Cursor.visible = false;     //���������Խ��п���
            Cursor.lockState = CursorLockMode.Locked;   //�����Ļ����������

            transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * SpeedR, Space.World);   //����X����x�����ת
            transform.Rotate(Vector3.left, Input.GetAxis("Mouse Y") * SpeedR, Space.Self);  //����Y����y�����ת

            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            transform.Translate(new Vector3(h, z, v) * SpeedM * SpeedA * Time.deltaTime);   //����һ��Vector3������λ��
        }
        else        //δ�յ��Ҽ������뼴��������
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void OnMouseMove(Transform CT)       //��������LookSlogan�е�transform��Ϣ
    {
        if (!isMoving )    ////�ж��Ƿ�ﵽ�ܹ�ִ�е�����
        {
            isMoving = true;
            StartCoroutine(MoveCamera(CT));     //�������Э��
        }
    }

    IEnumerator MoveCamera(Transform CT)
    {
        while (Vector3.Distance(transform.position, CT.transform.position) > 0.1f   //�ж���������ƶ������λ����Ϣ
            && Quaternion.Angle(transform.rotation, CT.transform.rotation) > 0.1f)  //�ж���������ƶ��������ת��Ϣ
        {
            transform.position = Vector3.Lerp(transform.position, CT.transform.position,    //��Vector3.Lerp��õĲ�ֵ��ֵ��λ��
                MoveSpeed * Time.deltaTime);                                            
            transform.rotation = Quaternion.Lerp(transform.rotation, CT.transform.rotation, //��Quaterion.Lerp��õĲ�ֵ��ֵ����ת
                MoveSpeed * Time.deltaTime);

            yield return null;
        }
        
        isMoving = false ;
    }
}
