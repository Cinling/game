#pragma once
#include "base_role.h"

class Plant : public BaseRole {
public:
    Plant(Tool::Struct::Vector3 * vector3);
    ~Plant();

    // 通过 BaseRole 继承
    virtual void UPSDo(void * voidRoleCtrl) override;
    virtual const std::map<std::string, std::string> GetInfo() override;
    virtual const std::string GetSpecialShowData() override;

    // 通过 BaseRole 继承
    virtual void SetInfo(std::map<std::string, std::string>) override;
};

