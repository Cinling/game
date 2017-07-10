using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
	private float speed = 0.5f;
	private float rotation_x;

	// 全局初始化方法
	void Start()
	{
		// 初始化镜头的角度x值
		rotation_x = transform.localEulerAngles.x;
		//Global.Start();
	}
	
	void Update()
	{
		// 镜头控制的方法
		this.moveCamera();
	}

	/// <summary>
	/// 2017年6月1日 22:50:59
	/// 全局控制方法
	/// </summary>
	private void moveCamera()
	{
		// 方向键控制摄像头移动
		if (Input.GetKey( "d" ))
		{
			Vector3 movePoistion = new Vector3( speed, 0, 0 );
			transform.Translate( movePoistion );
		}
		if (Input.GetKey( "a" ))
		{
			Vector3 movePoistion = new Vector3( -speed, 0, 0 );
			transform.Translate( movePoistion );
		}
		if (Input.GetKey( "w" ))
		{
			Vector3 movePoistion = new Vector3( 0, speed * Mathf.Sin( rotation_x ), speed * Mathf.Sin( rotation_x ) );
			transform.Translate( movePoistion );
		}
		if (Input.GetKey( "s" ))
		{
			Vector3 movePoistion = new Vector3( 0, -speed * Mathf.Sin( rotation_x ), -speed * Mathf.Sin( rotation_x ) );
			transform.Translate( movePoistion );
		}

		// 镜头放大
		if (Input.GetAxis( "Mouse ScrollWheel" ) > 0)
		{
			Vector3 movePoistion = new Vector3( 0, 0, speed * 5 );
			transform.Translate( movePoistion );
		}
		// 镜头缩小
		if (Input.GetAxis( "Mouse ScrollWheel" ) < 0)
		{
			Vector3 movePoistion = new Vector3( 0, 0, -speed * 5 );
			transform.Translate( movePoistion );
		}


		if (Input.GetKeyDown("q"))
		{

			Quaternion rotation = this.gameObject.transform.rotation;

			Debug.Log( "x:" + rotation.x + ", y:" + rotation.y + ", z:" + rotation.z );
			rotation.eulerAngles = new Vector3( 0.0f, 10.0f, 0.0f );
			this.gameObject.transform.rotation = rotation;
			//gameObject.transform.SetPositionAndRotation(this.gameObject.transform.position, rotation);



			Debug.Log( "x:" + rotation.x + ", y:" + rotation.y + ", z:" + rotation.z );
		}
		if (Input.GetKeyDown("e"))
		{
			this.gameObject.transform.Rotate( new Vector3( 0, 0, -10f ) );
		}
		if (Input.GetKeyDown( "r" ))
		{
			this.gameObject.transform.Rotate( new Vector3( 10f, 0, 0 ) );
		}
		if (Input.GetKeyDown( "f" ))
		{
			this.gameObject.transform.Rotate( new Vector3( -10f, 0, 0 ) );
		}


		// 鼠标中键 控制镜头旋转的方法
		if (Input.GetMouseButton(2))
		{
			float mouse_x = Input.GetAxis( "Mouse X" ) * 1f;
			float mouse_y = Input.GetAxis( "Mouse Y" ) * 1f;


			this.transform.Rotate( new Vector3( mouse_y, mouse_x, 0 ) );

			Quaternion rotation = this.transform.rotation;

			Debug.Log( "(" + rotation.x + ", " + rotation.y + ", " + rotation.z + "), (" + mouse_x + ", " + mouse_y + ")" );
		}



		if (Input.GetKey("n"))
		{
			//Object spherePreb = Resources.Load( "Animal/Persion/Guy", typeof( GameObject ) );
			//GameObject sphere = Instantiate( spherePreb ) as GameObject;
			//sphere.transform.position = new Vector3( 5, 1, 3 );
			GameObject gameObject = Global.CreateBaseRole( "Animal/Persion/Guy", 5, 1, 3 );
			Global.AddBaseRole( 0, gameObject );

			Global.Start();
		}

		if (Input.GetKey("m"))
		{
			Global.Pause();
		}
	}
}
