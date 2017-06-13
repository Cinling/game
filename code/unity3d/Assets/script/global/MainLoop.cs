using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 2017年6月8日 22:55:04
/// 游戏的全局循环类
/// </summary>
public class MainLoop : MonoBehaviour 
{
	// 存储游戏对象
	private static List<GameObject> gameObjectArrayList;

	// 食物刷新的帧数间隔
	private int foot_flush_frames_cell = 5 * 1;

	// 记录游戏运行的帧数
	private int game_frames_num = 0;

	// Use this for initialization
	void Start () 
	{
		// 创建一个Guy角色
		this.createGuy( 2, 0, 2 );

		MainLoop.gameObjectArrayList = new List<GameObject> { };
	}
	

	void Update () 
	{

		++game_frames_num;
		if (game_frames_num % foot_flush_frames_cell == 0)
		{
			if (MainLoop.gameObjectArrayList.Count < 20)
			{
				// 随机创建菠萝
				GameObject pineapple = this.createPineapple( Random.Range( 0f, 20f ), 0, Random.Range( 0f, 20f ) );
				gameObjectArrayList.Add( pineapple );
			}

			if (MainLoop.gameObjectArrayList.Count > 0)
			{
				foreach (GameObject gameObject in MainLoop.gameObjectArrayList)
				{
					gameObject.transform.position = new Vector3( gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + 0.1f );
				}
			}
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
	private GameObject CreatePrefabs(string prebPath, float x, float y, float z)
	{
		Object spherePreb = Resources.Load( prebPath, typeof( GameObject ) );
		GameObject sphere = Instantiate( spherePreb ) as GameObject;
		sphere.transform.position = new Vector3( x, y, z );

		return sphere;
	}

	// 创建一个Guy
	private GameObject createGuy(float x, float y, float z)
	{
		return this.CreatePrefabs( "Animal/Persion/Guy", x, y, z );
	}

	private GameObject createPineapple(float x, float y, float z)
	{
		return this.CreatePrefabs( "Foot/Fruits/Pineapple", x, y, z );
	}
}
