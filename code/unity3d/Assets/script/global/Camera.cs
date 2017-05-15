using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour {

    private float speed = 0.5f;
    private float rotation_x;

    void Start () {
        rotation_x = transform.localEulerAngles.x;
    }
	
	void Update () {

        // 方向键控制摄像头移动
		if (Input.GetKey("d")) {
            Vector3 movePoistion = new Vector3(speed, 0, 0);
            transform.Translate(movePoistion);
        }
        if (Input.GetKey("a"))
        {
            Vector3 movePoistion = new Vector3(-speed, 0, 0);
            transform.Translate(movePoistion);
        }
        if (Input.GetKey("w"))
        {
            Vector3 movePoistion = new Vector3(0, speed*Mathf.Sin(rotation_x), speed*Mathf.Sin(rotation_x));
            transform.Translate(movePoistion);
        }
        if (Input.GetKey("s"))
        {
            Vector3 movePoistion = new Vector3(0, -speed*Mathf.Sin(rotation_x), -speed*Mathf.Sin(rotation_x));
            transform.Translate(movePoistion);
        }

        // 镜头放大
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            Vector3 movePoistion = new Vector3(0, 0, speed*5);
            transform.Translate(movePoistion);
        }
        // 镜头缩小
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            Vector3 movePoistion = new Vector3(0, 0, -speed*5);
            transform.Translate(movePoistion);
        }
    }
}
