using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float SpeedR = 1.5f; //旋转的倍数
    public float SpeedM = 5f;   //移动的速度
    public float SpeedA = 1f;   //加速的倍数
    public float MoveSpeed = 10.0f;
    private float z = 0f;       //定义一个变量来控制摄像机的上升下降
    public bool isMoving = false ;

    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void Move()
    {
        if (Input.GetKey(KeyCode.LeftShift)) { SpeedA = 3.0f; }     //是否进行加速的判断
        else { SpeedA = 1.0f; }

        if (Input.GetKey(KeyCode.Q)) { z = -1; }        //上升的判断
        else if (Input.GetKey(KeyCode.E)) { z = 1; }    //下降的判断
        else { z = 0; }

        if (Input.GetMouseButton(1))    //当收到鼠标右键的输入时
        {
            Cursor.visible = false;     //对鼠标可视性进行控制
            Cursor.lockState = CursorLockMode.Locked;   //将中心画面进行锁定

            transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * SpeedR, Space.World);   //按下X控制x轴的旋转
            transform.Rotate(Vector3.left, Input.GetAxis("Mouse Y") * SpeedR, Space.Self);  //按下Y控制y轴的旋转

            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            transform.Translate(new Vector3(h, z, v) * SpeedM * SpeedA * Time.deltaTime);   //定义一个Vector3来控制位置
        }
        else        //未收到右键的输入即将鼠标解锁
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void OnMouseMove(Transform CT)       //接受来自LookSlogan中的transform信息
    {
        if (!isMoving )    ////判断是否达到能够执行的条件
        {
            isMoving = true;
            StartCoroutine(MoveCamera(CT));     //调用这个协程
        }
    }

    IEnumerator MoveCamera(Transform CT)
    {
        while (Vector3.Distance(transform.position, CT.transform.position) > 0.1f   //判断摄像机和移动载体的位置信息
            && Quaternion.Angle(transform.rotation, CT.transform.rotation) > 0.1f)  //判断摄像机和移动载体的旋转信息
        {
            transform.position = Vector3.Lerp(transform.position, CT.transform.position,    //将Vector3.Lerp获得的插值赋值给位置
                MoveSpeed * Time.deltaTime);                                            
            transform.rotation = Quaternion.Lerp(transform.rotation, CT.transform.rotation, //将Quaterion.Lerp获得的插值赋值给旋转
                MoveSpeed * Time.deltaTime);

            yield return null;
        }
        
        isMoving = false ;
    }
}
