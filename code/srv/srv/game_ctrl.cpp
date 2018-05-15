#include "game_ctrl.h"

#include <Windows.h>


GameCtrl * GameCtrl::shareInstance = nullptr;


GameCtrl::GameCtrl() {
    this->execRoleQueue = new std::queue<BaseRole *>();
    this->lpsProgress = 0;
    this->isPause = false;
    this->currUpsNum = 0;
    this->currUpsNum = 0;
    this->isStop = false;
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

    // 结束所有线程，并释放内存
    this->isPause = true;
    this->isStop = true;
    std::this_thread::sleep_for(std::chrono::milliseconds(1000));   // 等待一秒，让所有进程结束
    if (this->mainLogicThread != nullptr) {
        delete this->mainLogicThread;
        this->mainLogicThread = nullptr;
    }
    for (std::vector<std::thread*>::iterator it = this->logicThreadList.begin(); it != this->logicThreadList.end(); ++it) {
        std::thread * thread = *it;
        delete thread;
        thread = nullptr;
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

void Friend_LogicManager() {
    GameCtrl * gameCtrl = GameCtrl::GetInstance();
    int sleepMS = 1000 / gameCtrl->lps;

    while (true) {
        std::this_thread::sleep_for(std::chrono::milliseconds(sleepMS));
        if (!gameCtrl->isPause) {
            // 设置

            ++gameCtrl->currUpsNum;
        } else if (gameCtrl->isStop) {
            break;
        }
    }
}

void Friend_Logic(int threadNum) {

    GameCtrl * gameCtrl = GameCtrl::GetInstance();

    int currLpsNum = gameCtrl->currUpsNum;

    while (true) {
        try {
            // 游戏暂停
            if (gameCtrl->isPause) {
                if (gameCtrl->isStop) {
                    break;
                }
                std::this_thread::sleep_for(std::chrono::milliseconds(100));
                continue;
            }

            // 当前线程逻辑帧数
            if (currLpsNum > gameCtrl->currUpsNum) {
                std::this_thread::sleep_for(std::chrono::milliseconds(1));
                continue;
            }

            //printf_s("当前逻辑帧：%d，逻辑线程号：%d\n", currLpsNum, threadNum);

            ++currLpsNum;
        } catch (...) {
            int exceptionSleepMS = 5000;
            //printf_s("逻辑线程异常，休眠 %dms 后重启", exceptionSleepMS);
        }
    }
}
