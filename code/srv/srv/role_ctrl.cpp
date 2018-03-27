#include "role_ctrl.h"


RoleCtrl * RoleCtrl::shareInstance = nullptr;


RoleCtrl::RoleCtrl() {
    this->roleList = std::map<int, BaseRole *>();
}

RoleCtrl * RoleCtrl::GetInstance() {
    if (shareInstance == nullptr) {
        shareInstance = new RoleCtrl();
    }
    return shareInstance;
}
