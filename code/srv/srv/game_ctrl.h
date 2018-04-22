#pragma once

#include "config.h"
#include "role_ctrl.h"

#include <Windows.h>
#include <thread>
#include <queue>

class GameCtrl {
private:
    static GameCtrl * shareInstance;
    GameCtrl();

private:
    // 角色控制器的指针
    std::queue<BaseRole *> * execRoleQueue;
    // LPS 进度，表示逻辑的执行的顺序
    int lpsProgress;
    // 暂停标记
    bool isPause;
    // 当前逻辑帧数目
    int currUpsNum;
    // 逻辑帧
    int lps;

private:
    // 逻辑帧进度：基本的角色处理
    static const int LPS_PROGRESS_ROLE_EXEC = 10;

public:
    static GameCtrl * GetInstance();
    ~GameCtrl();
    // 用户点击启动游戏时执行的方法，在启动 socket 之前执行
    void Init();

public:
    // 开始游戏，指用户载入游戏或新建游戏后执行的方法
    void Start();
    // 暂停游戏
    void Pause();
    // 继续暂停了的游戏
    void Resume();
    // 设置游戏的逻辑帧
    void SetUPS(int lps);

private:
    // 逻辑线程管理线程 执行的方法
    // 逻辑线程管理线程 管理整个游戏的逻辑进度
    friend void Friend_LogicManager(GameCtrl * gameCtrl);
    // 逻辑线程 执行的方法
    friend void Friend_Logic(GameCtrl * gameCtrl, int threadNum);
};

void Friend_Logic(GameCtrl * gameCtrl, int threadNum);
void Friend_LogicManager(GameCtrl * gameCtrl);

