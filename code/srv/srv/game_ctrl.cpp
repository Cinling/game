#include "game_ctrl.h"



GameCtrl * GameCtrl::shareInstance = nullptr;


GameCtrl::GameCtrl() {
    this->execRoleQueue = new std::queue<BaseRole *>();
    this->lpsProgress = 0;
    this->isPause = true;
    this->currUpsNum = 0;
    this->currUpsNum = 0;
}


GameCtrl * GameCtrl::GetInstance() {
    if (GameCtrl::shareInstance == nullptr) {
        GameCtrl::shareInstance = new GameCtrl();
    }
    return GameCtrl::shareInstance;
}

GameCtrl::~GameCtrl() {
    if (this->execRoleQueue) {
        delete this->execRoleQueue;
        this->execRoleQueue = nullptr;
    }
}

void GameCtrl::Init() {
    Config::GetInstance()->InitConfig();
}

void GameCtrl::Start() {
    Config * config = Config::GetInstance();
    config->InitConfig();

    // 设置默认的逻辑帧
    config->Set(Config::TYPE_GAME_SYS, "LPS", this->lps);

    // 创建逻辑管理线程
    std::thread logicManagerThread(Friend_LogicManager, this);

    // 根据核心数创建对应数量的逻辑线程。逻辑线程数 = CPU核心数 - 1
    int coreNum = 0;
    config->Set(Config::TYPE_ENV, "CORE_NUM", coreNum);
    coreNum = (coreNum != 0) ? coreNum : 2; // 默认双核，双核只会创建一个逻辑线程
    for (int i = 1; i < coreNum; ++i) {
        std::thread logicThread(Friend_Logic, this, i);
        logicThread.detach();
    }
    
}

void GameCtrl::Pause() {
    this->isPause = true;
}

void GameCtrl::Resume() {
    this->isPause = false;
}

void GameCtrl::SetUPS(int lps) {
    this->lps = lps;
}
 
void Friend_LogicManager(GameCtrl * gameCtrl) {

    while (true) {

    }
}

void Friend_Logic(GameCtrl * gameCtrl, int threadNum) {

    while (true) {
        if (gameCtrl->isPause) {
            Sleep(100);
        }


    }
}
