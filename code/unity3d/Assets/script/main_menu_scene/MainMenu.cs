using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu
{
	private static MainMenu share_instance = null;

	public static MainMenu GetInstance()
	{
		if (share_instance == null)
		{
			share_instance = new MainMenu();
		}
		return share_instance;
	}

	public void InitButtonEvent()
	{
		AddOnClickListenerWithBtnName("MainCanvas/BtnStartGame");
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
			Debug.Log("Error [ButtonEvent AddOnClickListenerWithBtnName] not find button with name:[" + btnName + "]");
			return;
		}

		// 获取按钮脚本组件
		UnityEngine.UI.Button btnComponent = btnGameObject.GetComponent<UnityEngine.UI.Button>();
		// 添加点击监听
		btnComponent.onClick.AddListener(delegate () {
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
			case "MainCanvas/BtnStartGame":
				World.Init();
				break;

			default:
				Debug.Log("Unkonw button event.");
				break;
		}
	}
}
