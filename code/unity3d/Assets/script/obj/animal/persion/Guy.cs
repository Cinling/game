using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guy : Animal
{

	protected override void AI()
	{
		Move( Animal.DIRECTION_FORWARD );
		Debug.Log( "move" );
	}

	protected override void Death()
	{
		
	}

	protected override void InitAnimalChild()
	{
		
	}
}
