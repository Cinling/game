using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTime {

    public static GameTime share_instance = null;
    private GameTime() {
        InitTime();
        this.nextDayNeedUps = CNF_DAYS_UPS;
    }
    public static GameTime GetInstance() {

        if (share_instance == null) {
            share_instance = new GameTime();
        }

        return share_instance;
    }
    public const short realUpsPerSecond = 6;



    /// 规定配置相关的参数：一年有多少天，每个季节有多少天
    private int CNF_DAYS_UPS; /// 一天相当于真实时间的多少帧
    private short CNF_SPRING_DAYS; /// 春季多少天
    private short CNF_SUMMER_DAYS; /// 夏季多少天
    private short CNF_AUTUMN_DAYS; /// 秋季多少天
    private short CNF_WINTER_DAYS; /// 冬季多少天
    private short CNF_DAYS_PER_YEAR; /// 一年有多少天

    // 年数(ulong做逻辑帧数，如果每秒6帧，每天十分钟，每年500天，大概有10万亿年)
    private int year;
    /// <summary>
    /// 10000-19999：春季、 20000-29999：夏季、30000-39999：秋季、40000-49999：冬季
    /// 具体一个季节多少天根据配置来决定
    /// </summary>
    private int season;
    /// <summary>
    /// 0：周日、 1：周一、 2：周二、 3：周三、 4：周四、 5：周五、 6：周六
    /// </summary>
    private byte week;

    private int nextDayNeedUps;



    /// <summary>
    /// 默认配置
    /// </summary>
    private void InitTime() {
        LoadDefaultConfig();
    }

    /// <summary>
    /// 自定义配置
    /// </summary>
    /// <param name="daysSceond">每天相对现实的秒数</param>
    /// <param name="springDays">春天的天数</param>
    /// <param name="summerDays">夏天的天数</param>
    /// <param name="autumnDays">秋天的天数</param>
    /// <param name="winterDays">冬天的天数</param>
    private void InitTime(short daysSceond, short springDays, short summerDays, short autumnDays, short winterDays) {
        // 如果单个机械的天数大于10000或者出现负数，则使用默认配置
        if (springDays > 10000 || summerDays > 10000 || autumnDays > 10000 || winterDays > 10000
            || springDays < 0 || summerDays < 0 || autumnDays < 0 || winterDays < 0) {
            Log.PrintLog("TimeCtrl.GameTime", "GameTime", "", Log.LOG_LEVEL.ERROR);
            LoadDefaultConfig();

        } else {
            CNF_DAYS_UPS = daysSceond * CanvasGame.CNF_REAL_UPS;
            CNF_SPRING_DAYS = springDays;
            CNF_SUMMER_DAYS = summerDays;
            CNF_AUTUMN_DAYS = autumnDays;
            CNF_WINTER_DAYS = winterDays;
            CNF_DAYS_PER_YEAR = (short)( CNF_SPRING_DAYS + CNF_SUMMER_DAYS + CNF_AUTUMN_DAYS + CNF_WINTER_DAYS );
        }
    }

    private void LoadDefaultConfig() {
        CNF_DAYS_UPS = 5 * CanvasGame.CNF_REAL_UPS;
        CNF_SPRING_DAYS = 10;
        CNF_SUMMER_DAYS = 10;
        CNF_AUTUMN_DAYS = 10;
        CNF_WINTER_DAYS = 10;
        CNF_DAYS_PER_YEAR = (short)( CNF_SPRING_DAYS + CNF_SUMMER_DAYS + CNF_AUTUMN_DAYS + CNF_WINTER_DAYS );
    }


    /// <summary>
    /// 每个逻辑帧执行的方法
    /// </summary>
    public void NextUps() {

        if (--nextDayNeedUps == 0) {

            NextDay();

            nextDayNeedUps = CNF_DAYS_UPS;
        }
    }


    /// <summary>
    /// 判断是否进入下一天
    /// </summary>
    /// <returns></returns>
    public bool IdNextDay() {
        return (nextDayNeedUps == CNF_DAYS_UPS );
    }


    /// 新一天触发的方法
    public void NextDay() {

        // 更新季节
        NextDaySeasonChange();
        // 更新星期
        NextDayWeekChange();
        // 更新年份
        NextDayYearChange();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="upsSum">游戏进行的总逻辑帧</param>
    /// <returns></returns>
    public bool IsNextDay(ulong upsSum) {

        //int gameSecond = (int)(upsSum % (ulong)ups);
        //if (true) {

        //}

        return false;
    }




    /// 进入下一天季节的变化
    private void NextDaySeasonChange() {
        ++season;

        // 春季转夏季
        if (season >= 10000 + CNF_SPRING_DAYS && season < 20000) {
            season = 20000;
            // 夏季转秋季
        } else if (season >= 20000 + CNF_SUMMER_DAYS && season < 30000) {
            season = 30000;
            // 秋季转冬季
        } else if (season >= 30000 + CNF_AUTUMN_DAYS && season < 40000) {
            season = 40000;
            // 冬季转春季
        } else if (season >= 40000 + CNF_WINTER_DAYS) {
            season = 10000;
        }
    }

    /// 进入下一天周数的变化
    private void NextDayWeekChange() {
        ++week;

        if (week > 6) {
            week = 0;
        }
    }

    private void NextDayYearChange() {

    }
}
