#pragma once

#include "db_interface.h"
#include "tool.h"

class RoleDB : public DBInterface {
private:
    static RoleDB * shareInstance;
    RoleDB();
public:
    static RoleDB * GetInstance();
    ~RoleDB();

    // 通过 DBInterface 继承
    virtual bool CreateTable() override;
public:
    // 表明
    static const std::string TABLE_NAME;
    // INTEGER 主键，角色id
    static const std::string FIELD_ID;
    // INTEGER 角色类型
    static const std::string FIELD_TYPE;
    // FLOAT 角色位置x
    static const std::string FIELD_X;
    // FLOAT 角色位置y
    static const std::string FIELD_Y;
    // FLOAT 角色位置z
    static const std::string FIELD_Z;
    // FLOAT 角色面向角度
    static const std::string FIELD_ROTATION;
    // TEXT 其他特异性信息
    static const std::string FIELD_INFO;

public:
    // 【存储一条角色数据】
    // id 角色id
    // type 角色类型
    // position 角色位置
    // rotation 角色方向
    // info 特异性的信息
    bool InsertOnce(int id, int type, Tool::Struct::Vector3 position, float rotation, std::string info);

    // 查询所有数据
    std::list<std::map<std::string, std::string>> SelectAll();
};

