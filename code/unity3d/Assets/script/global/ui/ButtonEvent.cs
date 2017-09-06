using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEvent : MonoBehaviour
{
	void Start()
	{
		AddOnClickListenerWithBtnName("Canvas/btnStartGame");
	}

	/// <summary>
	/// 2017-09-03 16:54:49
	/// </summary>
	/// <param name="btnName"></param>
	void AddOnClickListenerWithBtnName(string btnName)
	{
		// 获取按钮 GameObject
		GameObject btnGameObject = GameObject.Find(btnName);
		if (btnGameObject == null)
		{
			Debug.Log("Error [SceneCtrl AddOnClickListenerWithBtnName] not find btn with name:[" + btnName + "]");
			return;
		}

		// 获取按钮脚本组件
		UnityEngine.UI.Button btnComponent = btnGameObject.GetComponent<UnityEngine.UI.Button>();
		// 添加点击监听
		btnComponent.onClick.AddListener(delegate() {
			OnClick(btnName);
		});
	}


	/// <summary>
	/// 2017-09-03 16:54:57
	/// 监听到按钮的功能实现
	/// </summary>
	/// <param name="btnName"></param>
	void OnClick(string btnName)
	{
		switch (btnName)
		{
			case "Canvas/btnStartGame":
				UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("World");
				break;
		}
	}
}
