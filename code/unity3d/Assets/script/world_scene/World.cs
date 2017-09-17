using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World
{
	public static void Init()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene("World");
		Log.PrintLog("World", "Init", "InitScene", Log.LOG_LEVEL.INFO);
	}
}
