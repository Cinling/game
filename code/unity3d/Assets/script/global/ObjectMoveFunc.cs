using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMoveFunc : MonoBehaviour
{
	/**
	 * @version 2017年5月17日 23:48:02
	 * 全局静态角色移动方法
	 */
	public static void move(GameObject gameObject, Vector3 vec)
	{
		gameObject.transform.Translate(vec);
	}
}
