using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : Plant {
    const string preb = "Foot/Fruits/Carrot/Carrot_Fruit";

    public static GameObject CreateCarrot(float x, float y, float z) {
        return CreatePlant(x, y, z, preb);
    }

    protected override void Grow() {
        throw new NotImplementedException();
    }
}
