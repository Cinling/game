#pragma once

#include "sqlite_tool.h"
#include "db_manager.h"
#include "role_ctrl.h"
#include "json.h"

class World {
private:
    static World *shareInstance;
    World();

    Json::Map * map;

public:
    static World * GetInstance();
    ~World();

    // ��ʼ�����磨�״ν�����Ϸ���������磩
    bool InitMap(float width, float length, float height);
    bool InitMap(Json::Map * map);

    // ��ʼ��Ϸ
    bool Start();

    // ��ͣ��Ϸ
    bool Pause();

    // ��������ͣ����Ϸ
    bool Resume();

    // �˳���Ϸ���رշ���ˣ�
    bool Exit();

    // ������Ϸ����
    bool Save();

    // ������Ϸ����
    bool Load();

    // ��ȡ��ͼ����
    Json::Map GetMapInfo();

    // sqlite ���Է���
    void SqliteTest();

private:
    // ������������
    void SaveWorld();

    // ������ͼ
    // width ��ͼ���
    // length ��ͼ����
    // return �����ͼ��id
    //int CreateMap(int width, int length);
};