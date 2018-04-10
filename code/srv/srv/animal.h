#pragma once

#include "base_role.h"
#include "role_ctrl.h"

class Animal : public BaseRole {
public:
    Animal(Tool::Struct::Vector3 * position);
    ~Animal();

    // ͨ�� BaseRole �̳�
    virtual void UPSDo(void * voidRoleCtrl) override;

    // ͨ�� BaseRole �̳�
    virtual const std::map<std::string, std::string> GetInfo() override;

    // ͨ�� BaseRole �̳�
    virtual void SetInfo(std::map<std::string, std::string> & info) override;
    virtual const std::string GetSpecialShowData() override;
};

