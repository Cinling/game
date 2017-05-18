using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class base_ai : MonoBehaviour
{
	
	void Start ()
	{
		
	}
	
	/**
	 * @version 2017年5月18日 00:23:40
	 */
	void Update ()
	{
		//ObjectMoveFunc.move(this.gameObject, new Vector3(0, -0.1f, 0));
		ObjectMoveFunc.turn(this.gameObject, 1.0f, ObjectMoveFunc.CLOCK );
	}
}
