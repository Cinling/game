#include "game_ctrl.h"

#include <Windows.h>


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
    this->mainLogicThread = new std::thread(Friend_LogicManager);
    // 初始化子逻辑线程数组
    this->logicThreadList = std::vector<std::thread*>();

    // 根据核心数创建对应数量的逻辑线程。逻辑线程数 = CPU核心数 - 1
    int coreNum = std::thread::hardware_concurrency();
    coreNum = (coreNum != 0) ? coreNum : 2; // 默认双核，双核只会创建一个逻辑线程
    for (int i = 1; i < coreNum; ++i) {
        std::thread * logicThread = new std::thread(Friend_Logic, i);
        logicThread->detach();

        this->logicThreadList.push_back(logicThread);
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

bool GameCtrl::IsPause() {
    bool retBool = false;
    if (this->isPause) {
        retBool = true;
    }
    return retBool;
}
 
void Friend_LogicManager() {
    GameCtrl * gameCtrl = GameCtrl::GetInstance();
    int sleepMS = 1000 / gameCtrl->lps;

    while (true) {
        std::this_thread::sleep_for(std::chrono::milliseconds(sleepMS));
        ++gameCtrl->currUpsNum;
    }
}

void Friend_Logic(int threadNum) {
    try {

        GameCtrl * gameCtrl = GameCtrl::GetInstance();

        int currLpsNum = gameCtrl->currUpsNum;

        while (true) {
            if (gameCtrl->IsPause()) {
                std::this_thread::sleep_for(std::chrono::milliseconds(100));
                std::cout << "Sleep " << threadNum << std::endl;
                continue;
            }

            if (currLpsNum > gameCtrl->currUpsNum) {
                std::this_thread::sleep_for(std::chrono::milliseconds(1));
                std::cout << "waiting next lps" << std::endl;
                continue;
            }

            std::cout << "当前逻辑帧：" << currLpsNum << "，逻辑进程号：" << threadNum <<  std::endl;

            ++currLpsNum;
        }
    } catch (...) {
        std::cout << "异常" << std::endl;
    }
}
