using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleCtrl {

    private static ulong gameTime;
    public static ulong GameTime {
        get { return gameTime; }
    }

    private static RoleCtrl roleCtrl = null;
    private RoleCtrl() {
        // 基本角色对象字典
        baseRoleDict = new Dictionary<int, List<BaseRole>>();
        baseRoleDict[BASE_ROLE_DICT_COMMON] = new List<BaseRole>();

        // 初始化游戏时间
        gameTime = 0;
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
    private const int BASE_ROLE_DICT_COMMON = BASE_ROLE_DICT_KEY + 1;






    // 开启角色进程
    public void StartRoleThread(short lps) {
        ThreadCtrl.GetInstance().AddThead(ThreadCtrl.THREAD_NUM.LOGIC_ROLE, () => {

            // 计算下一次逻辑帧需要经过多少个渲染帧
            MainUICtrl.ReSetNextLpsNeedFps();

            // 遍历角色进行相关逻辑运算
            lock (baseRoleDict) {

                if (baseRoleDict[BASE_ROLE_DICT_COMMON] != null) {

                    foreach (BaseRole baseRole in baseRoleDict[BASE_ROLE_DICT_COMMON]) {
                        baseRole.UpdatePerLps();
                    }
                }
            }
            ++gameTime;
            return 0;
        }, 1000 / lps + 1);
    }





    // 创建测试的游戏对象
    public void CreateTestGuy() {
        GameObject gameObject = Guy.CreateGuy(10f, 0f, 0f, 100, 10, 10);
        BaseRole baseRole = gameObject.GetComponent<Guy>();
        baseRoleDict[BASE_ROLE_DICT_COMMON].Add(baseRole);
    }

    public void CreatePumpkin() {
        GameObject gameObject = Pumpkin.CreatePumpkin(10f, 0f, 0f);
        BaseRole baseRole = gameObject.GetComponent<Pumpkin>();
        baseRoleDict[BASE_ROLE_DICT_COMMON].Add(baseRole);
    }
}
