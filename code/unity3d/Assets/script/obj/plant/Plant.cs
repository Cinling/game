using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Plant : BaseRole
{
	public override void Do()
	{
		
	}

	protected override void InitBaseRole()
	{
		InitPlant();
	}


	protected abstract void InitPlant();
}
