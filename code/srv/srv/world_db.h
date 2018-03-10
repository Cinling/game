#pragma once

#include "db_interface.h"

class WorldDB : public DBInterface {
private:
    static WorldDB * shareInstance;
    WorldDB();

public:
    static WorldDB * GetInstance();
    ~WorldDB();

    // �洢����
    virtual std::list<std::map<std::string, std::string>> Save(std::list<std::map<std::string, std::string>>) override;
    // ��ȡ����
    virtual std::list<std::map<std::string, std::string>> Load(std::list<std::map<std::string, std::string>>) override;
};

