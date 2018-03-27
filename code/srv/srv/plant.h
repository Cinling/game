#pragma once
#include "base_role.h"

class Plant : public BaseRole {
public:
    Plant(Tool::Struct::Vector3 * vector3);
    ~Plant();

    // ͨ�� BaseRole �̳�
    virtual void UPSDo(void * voidRoleCtrl) override;
    virtual const std::map<std::string, std::string> GetInfo() override;
};

