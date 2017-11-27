using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCtrl {

    public static TimeCtrl share_instance = null;
    public static TimeCtrl GetInstance() {
        
        if (share_instance == null) {
            share_instance = new TimeCtrl();
        }

        return share_instance;
    }

    public class GameTime {
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

        // 规定配置相关的参数：一年有多少天，每个季节有多少天
        private short CNF_DAYS_PER_YEAR;
        private short CNF_SPRING_DAYS;
        private short CNF_SUMMER_DAYS;
        private short CNF_AUTUMN_DAYS;
        private short CNF_WINTER_DAYS;

        // 默认配置
        public GameTime() {
            CNF_DAYS_PER_YEAR = 6;
            CNF_SPRING_DAYS = 5;
            CNF_SUMMER_DAYS = 5;
            CNF_AUTUMN_DAYS = 5;
            CNF_WINTER_DAYS = 5;
        }

        // 制定配置
        public GameTime(short daysPerYear, short springDays, short summerDays, short autumnDays, short winterDays) {
            CNF_DAYS_PER_YEAR = daysPerYear;
            CNF_SPRING_DAYS = springDays;
            CNF_SUMMER_DAYS = summerDays;
            CNF_AUTUMN_DAYS = autumnDays;
            CNF_WINTER_DAYS = winterDays;
        }

        // 新一天触发的方法
        public void NextDay() {

            // 更新季节
            NextDaySeasonChange();
            // 更新星期
            NextDayWeekChange();
        }


        // 进入下一天季节的变化
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

        // 进入下一天周数的变化
        private void NextDayWeekChange() {
            ++week;

            if (week > 6) {
                week = 0;
            }
        }
    }

    public GameTime gameTime;
}
