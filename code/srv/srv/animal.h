#pragma once

#include "base_role.h"
#include "role_ctrl.h"

class Animal : public BaseRole {
public:
    Animal(Tool::Struct::Vector3 * vector3);
    ~Animal();

    // ͨ�� BaseRole �̳�
    virtual void UPSDo(void * voidRoleCtrl) override;
};

