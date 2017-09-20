using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : Plant
{
	protected override void InitPlant()
	{
		Log.PrintLog("Carrot", "InitPlant", "init", Log.LOG_LEVEL.INFO);
	}
}
