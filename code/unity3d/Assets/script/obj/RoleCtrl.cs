using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleCtrl {

    private static ulong upsSum; // 游戏总帧数
    public static ulong UpsSum {
        get { return upsSum; }
    }

    private static RoleCtrl roleCtrl = null;
    private RoleCtrl() {
        // 基本角色对象字典
        baseRoleDict = new Dictionary<int, List<BaseRole>>();
        baseRoleDict[BASE_ROLE_DICT_COMMON] = new List<BaseRole>();
        baseRoleDict[ANIMAL_ROLE_DICT] = new List<BaseRole>();
        baseRoleDict[PLANT_ROLE_DICT] = new List<BaseRole>();

        // 初始化游戏时间
        upsSum = 0;
    }
    public static RoleCtrl GetInstence() {

        if (roleCtrl == null) {
            roleCtrl = new RoleCtrl();
        }
        return roleCtrl;
    }

    // 游戏对象列表
    private Dictionary<int, List<BaseRole>> baseRoleDict;
    private const byte BASE_ROLE_DICT_KEY = 0;
    private const int BASE_ROLE_DICT_COMMON = 1; // 普通对象（UPS执行一次）
    private const int ANIMAL_ROLE_DICT = 2; // 动物对象（UPS执行一次）
    private const int PLANT_ROLE_DICT = 3; // 植物对象（每天执行一次，另外有其他的地方会执行到）






    // 开启角色进程
    public void StartRoleThread(short ups) {
        ThreadCtrl.GetInstance().AddThead(ThreadCtrl.THREAD_NUM.LOGIC_ROLE, () => {

            try {
                ++upsSum;

                // 计算下一次逻辑帧需要经过多少个渲染帧（每个逻辑帧都要调用，只有在MainUICtrl中使用到）
                MainUICtrl.ReSetNextUpsNeedFps();

                // 遍历角色进行相关逻辑运算
                lock (baseRoleDict) {

                    // 遍历普通角色
                    if (baseRoleDict[BASE_ROLE_DICT_COMMON] != null) {

                        foreach (BaseRole baseRole in baseRoleDict[BASE_ROLE_DICT_COMMON]) {
                            baseRole.UPS();
                        
                        }
                    }

                    // 遍历动物角色
                    if (baseRoleDict[ANIMAL_ROLE_DICT] != null) {

                        foreach (BaseRole animal in baseRoleDict[ANIMAL_ROLE_DICT]) {
                            animal.UPS();
                        }
                    }
                }

            } catch (System.Exception e) {  // 防止线程死掉
                Log.PrintLog("RoleCtrl", "StartRoleThread", e.Message, Log.LOG_LEVEL.ERROR);
                return -1;
            }
            
            return 0;
        }, 1000 / ups + 1);
    }





    // 创建测试的游戏对象
    public void CreateTestGuy() {
        GameObject gameObject = Guy.CreateGuy(10f, 0f, 0f, 100, 10, 10);
        BaseRole baseRole = gameObject.GetComponent<Guy>();
        baseRoleDict[ANIMAL_ROLE_DICT].Add(baseRole);
    }

    public void CreatePumpkin() {
        GameObject gameObject = Pumpkin.CreatePumpkin(10f, 0f, 0f);
        BaseRole baseRole = gameObject.GetComponent<Pumpkin>();
        baseRoleDict[PLANT_ROLE_DICT].Add(baseRole);
    }
}
