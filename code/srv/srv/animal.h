#pragma once

#include "base_role.h"
#include "role_ctrl.h"

class Animal : public BaseRole {
public:
    Animal(Tool::Struct::Vector3 * position);
    ~Animal();

    // 通过 BaseRole 继承
    virtual void UPSDo(void * voidRoleCtrl) override;

    // 通过 BaseRole 继承
    virtual const std::map<std::string, std::string> GetInfo() override;

    // 通过 BaseRole 继承
    virtual void SetInfo(std::map<std::string, std::string> & info) override;
    virtual const std::string GetSpecialShowData() override;
};

