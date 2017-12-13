using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pumpkin : Plant {
    const string prebPlant = "Plant/Pumpkin/Pumpkin_Plant";
    const string prebFruit = "Plant/Pumpkin/Pumpkin_Fruit";

    public static GameObject CreatePumpkin(float x, float y, float z) {
        return CreatePlant(x, y, z, "Plant/Pumpkin/Pumpkin_Plant", "Plant/Pumpkin/Pumpkin_Fruit");
    }

    int intTmp = 0;
    public override void DailyGrow() {
        Log.PrintLog("Pumpkin", "DailyGrow", "grow:"+ ++intTmp, Log.LOG_LEVEL.DEBUG);
        ChangePreb(prebFruit);
    }

    protected override void Grow() {
        
    }

}
