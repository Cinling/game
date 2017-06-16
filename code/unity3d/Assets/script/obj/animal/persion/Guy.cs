using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guy : Animal
{
	public const string prefab = "Animal/Persion/Guy";


	public Guy(float x, float y, float z) : base(x, y, z, Guy.prefab)
	{

	}

	protected override void AI()
	{
		this.Move(Animal.DIRECTION_FORWARD);
	}

	protected override void Death()
	{
		
	}
}
