using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instance : MonoBehaviour
{
	[RuntimeInitializeOnLoadMethod]
	static void Initialize()
	{
		GameObject.DontDestroyOnLoad(new GameObject("Instance", typeof(Instance))
		{
			hideFlags = HideFlags.HideInHierarchy
		});

		// 初始化主页面的按钮事件
		MainMenu.GetInstance().InitButtonEvent();
	}
}
