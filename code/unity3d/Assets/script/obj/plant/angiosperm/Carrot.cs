using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : Plant {
    protected override void InitPlant() {
        Log.PrintLog("Carrot", "InitPlant", "init", Log.LOG_LEVEL.INFO);
    }

    protected override void OnLifeCycleChange(byte life_cycle) {
        switch (life_cycle) {
            // 发芽
            case Plant.LIFE_CYCLE_SPROUT:
                break;

            // 出芽
            case Plant.LIFE_CYCLE_BUD:
                break;

            // 成熟
            case Plant.LIFE_CYCLE_MATURE:
                break;

            // 结果
            case Plant.LIFE_CYCLE_BEAR:
                break;

            // 死亡
            case Plant.LIFE_CYCLE_DEATH:
                break;

            // 未知状态
            default:
                break;
        }
    }
}
