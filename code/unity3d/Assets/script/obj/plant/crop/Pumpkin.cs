using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pumpkin : Plant {
    const string preb = "Plant/Pumpkin/Pumpkin_Plant";

    public static GameObject CreatePumpkin(float x, float y, float z) {
        return CreatePlant(x, y, z, preb);
    }

    public override void DailyGrow() {
        Log.PrintLog("Pumpkin", "DailyGrow", "grow", Log.LOG_LEVEL.DEBUG);
    }

    protected override void Grow() {
        
    }

}
