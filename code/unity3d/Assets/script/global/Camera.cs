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
		// 创建一个Guy角色
		createGuy();
	}

	// 全局循环
	void Update()
	{
		// 镜头控制的方法
		this.globalControl();

		// 随机创建菠萝
		this.createPineapple( Random.Range(0f, 20f), 0, Random.Range( 0f, 20f) );
	}

	/// <summary>
	/// 2017年6月1日 22:50:59
	/// 全局控制方法
	/// </summary>
	private void globalControl()
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
	}

	/// <summary>
	/// 2017年6月1日 22:51:09
	/// 基础创建角色方法
	/// </summary>
	/// <param name="prebPath">prefabs在Resourses下后面的路径，如："Animal/Persion/Guy"</param>
	/// <param name="x"></param>
	/// <param name="y"></param>
	/// <param name="y"></param>
	private void createPrefabs(string prebPath, float x, float y, float z)
	{
		Object spherePreb = Resources.Load( prebPath, typeof( GameObject ) );
		GameObject sphere = Instantiate( spherePreb ) as GameObject;
		sphere.transform.position = new Vector3( x, y, z );
	}

	// 创建一个Guy
	private void createGuy()
	{
		this.createPrefabs( "Animal/Persion/Guy", 1, 0, 2 );
	}

	private void createPineapple(float x, float y, float z)
	{
		this.createPrefabs( "Foot/Fruits/Pineapple", x, y, z );
	}
}
