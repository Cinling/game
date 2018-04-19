#include "game_ctrl.h"



GameCtrl * GameCtrl::shareInstance = nullptr;


GameCtrl::GameCtrl() {
    this->threadList = new std::list<std::thread>();
}


GameCtrl * GameCtrl::GetInstance() {
    if (GameCtrl::shareInstance == nullptr) {
        GameCtrl::shareInstance = new GameCtrl();
    }
    return GameCtrl::shareInstance;
}

GameCtrl::~GameCtrl() {
    if (this->threadList != nullptr) {
        delete this->threadList;
        this->threadList = nullptr;
    }
}

void GameCtrl::Start() {
}

void GameCtrl::Pause() {
}

void GameCtrl::SetUPS(int ups) {
}
