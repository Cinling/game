using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guy : Animal
{
	const string preb = "Animal/Persion/Guy";

	protected override void AI()
	{
		Move( Animal.DIRECTION_FORWARD );
	}

	protected override void Death()
	{
		
	}

	protected override void InitAnimal()
	{
		
	}
}
