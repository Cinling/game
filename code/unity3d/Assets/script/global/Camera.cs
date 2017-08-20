using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
	private float m_speed;
	private GameObject m_focus_gameobject;

	private float m_relative_x;			// 相对焦点的 x 值
	private float m_relative_y;			// 相对焦点的 y 值
	private float m_relative_z;			// 相对焦点的 z 值
	private float m_relative_leng;		// 相对焦点的距离

	// 主摄像机初始化方法
	void Start()
	{
		m_speed = 0.5f;
		m_focus_gameobject = GameObject.Find( "Main Camera Helper" );

		this.refreshCameraAndFocusRelative();
	}
	
	void Update()
	{
		// 镜头触发的方法
		this.controlEvent();
	}


	/// <summary>
	/// 2017年6月1日 22:50:59s
	/// 全局控制方法
	/// </summary>
	private void controlEvent()
	{
		Debug.Log(m_relative_leng);

		// 方向键控制摄像头移动
		if (Input.GetKey( "d" ))
		{
			this.moveFocus( m_speed * Mathf.Cos(transform.rotation.y), -m_speed * Mathf.Sin( transform.rotation.y ) );
		}
		if (Input.GetKey( "a" ))
		{
			this.moveFocus( -m_speed * Mathf.Cos( transform.rotation.y ), m_speed * Mathf.Sin( transform.rotation.y ) );
		}
		if (Input.GetKey( "w" ))
		{
			this.moveFocus( m_speed * Mathf.Sin( transform.rotation.y ), m_speed * Mathf.Cos( transform.rotation.y ) );
		}
		if (Input.GetKey( "s" ))
		{
			this.moveFocus( -m_speed * Mathf.Sin( transform.rotation.y ), -m_speed * Mathf.Cos( transform.rotation.y ) );
		}

		// 镜头放大
		if (Input.GetAxis( "Mouse ScrollWheel" ) > 0)
		{
			Vector3 movePoistion = new Vector3( 0, 0, m_speed * 5 );
			transform.Translate( movePoistion );

			this.refreshCameraAndFocusRelative();
		}
		// 镜头缩小
		if (Input.GetAxis( "Mouse ScrollWheel" ) < 0)
		{
			Vector3 movePoistion = new Vector3( 0, 0, -m_speed * 5 );
			transform.Translate( movePoistion );

			this.refreshCameraAndFocusRelative();
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
			this.resetFoucusByCamera();

			Vector3 position = m_focus_gameobject.transform.position;

			Debug.Log( "x:" + position.x + ", y:" + position.y + ", z:" + position.z );
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

			transform.RotateAround(m_focus_gameobject.transform.position, Vector3.up, -mouse_x);
			Quaternion rotation = this.transform.rotation;
			//transform.RotateAround(m_focus_gameobject.transform.position, new Vector3(Mathf.Cos( transform.rotation.y ), 0, -Mathf.Sin(transform.rotation.y) ), mouse_y);
			float w = this.transform.rotation.w;
			float x = this.transform.rotation.x;
			float y = this.transform.rotation.y;
			float z = this.transform.rotation.z;
			float oula_x = Mathf.Atan( (2 * (w * x + y * z)) / (1 - 2 * (Mathf.Pow(x, 2) + Mathf.Pow(y, 2))) );
			//float oula_y = Mathf.Asin( 2 * (w * y - z * x) );
			float oula_z = Mathf.Atan( (2 * (w * z + x * y)) / (1 - 2 * (Mathf.Pow(y, 2) + Mathf.Pow(z, 2))) );

			transform.RotateAround( m_focus_gameobject.transform.position, new Vector3( Mathf.Cos(oula_x), 0f, Mathf.Sin(oula_z) ), mouse_y );
		}



		if (Input.GetKey("n"))
		{
			GameObject gameObject = Global.CreateBaseRole( "Animal/Persion/Guy", 5, 1, 3 );
			Global.AddBaseRole( 0, gameObject );

			Global.Start();
		}

		if (Input.GetKey("m"))
		{
			Global.Pause();
		}


		if (true)
		{
			Vector3 r = this.transform.rotation.eulerAngles;
			Debug.Log("(" + r.x + "," + r.y + "," + r.z + ")");
		}
	}


	/// <summary>
	/// 2017-07-12 08:37:50
	/// 刷新镜头和焦点的相对距离
	/// </summary>
	private void refreshCameraAndFocusRelative()
	{
		m_relative_x = this.transform.position.x - m_focus_gameobject.transform.position.x;
		m_relative_y = this.transform.position.y - m_focus_gameobject.transform.position.y;
		m_relative_z = this.transform.position.z - m_focus_gameobject.transform.position.z;
		
		m_relative_leng = (float)System.Math.Sqrt( System.Math.Pow( m_relative_x, 2 ) + System.Math.Pow( m_relative_y, 2 ) + System.Math.Pow( m_relative_z, 2 ) );
	}


	/// <summary>
	/// 2017-07-25 08:27:59
	/// 移动焦点和摄像机
	/// </summary>
	public void moveFocus(float position_x, float position_z)
	{
		m_focus_gameobject.transform.Translate( new Vector3(position_x, 0, position_z) );
		this.transform.position = new Vector3( m_focus_gameobject.transform.position.x + m_relative_x, m_focus_gameobject.transform.position.y + m_relative_y, m_focus_gameobject.transform.position.z + m_relative_z );
	}


	/// <summary>
	/// 通过摄像机，重置焦点 与 焦点和摄像机相关的参数
	/// </summary>
	private void resetFoucusByCamera()
	{
		//float camera_rotation_y = this.transform.rotation.y;
		//m_focus_gameobject.transform.TransformPoint( new Vector3(Mathf.Cos(camera_rotation_y) * 30f, 0, /Mathf.Sin/(camera_rotation_y) * 30f) );
		//
		//m_relative_x = this.transform.position.x - m_focus_gameobject.transform.position.x;
		//m_relative_y = this.transform.position.y - m_focus_gameobject.transform.position.y;
		//m_relative_z = this.transform.position.z - m_focus_gameobject.transform.position.z;

		Vector3 rotation = this.transform.rotation.eulerAngles;

		// 平面上镜头和焦点的相对距离
		float plat_focus_to_camera_len = Mathf.Tan( rotation.x * Mathf.PI / 180 ) *  this.transform.position.y;
		float focus_z = this.transform.position.z + Mathf.Cos( rotation.y * Mathf.PI / 180 ) * plat_focus_to_camera_len;
		float focus_x = this.transform.position.x + Mathf.Sin( rotation.y * Mathf.PI / 180 ) * plat_focus_to_camera_len;

		m_focus_gameobject.transform.position = new Vector3( focus_x, 0f, focus_z );

		refreshCameraAndFocusRelative();
	}
}
