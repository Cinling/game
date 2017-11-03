﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guy : Animal
{
    const string preb = "Animal/Persion/Guy";

    public static GameObject CreateGuy(float x, float y, float z, uint speed, uint health, uint max_health)
    {
        return CreateAnimal(x, y, z, preb, speed, health, max_health);
    }

    protected override void AI()
    {
        Move( Animal.DIRECTION_FORWARD );
    }

    protected override void Death()
    {
        
    }
}
