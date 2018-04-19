#pragma once

#include "config.h"
#include "role_ctrl.h"

#include <thread>

class GameCtrl {
private:
    static GameCtrl * shareInstance;
    GameCtrl();

private:
    std::list<std::thread> * threadList = nullptr;

public:
    static GameCtrl * GetInstance();
    ~GameCtrl();

public:
    void Start();
    void Pause();
    void SetUPS(int ups);
};

