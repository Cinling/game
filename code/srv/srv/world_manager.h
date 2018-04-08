#pragma once

#include "map_manager.h"
#include "sqlite_tool.h"
#include "db_manager.h"
#include "role_ctrl.h"
#include "json.h"

class WorldManager {
private:
    static WorldManager *shareInstance;
    WorldManager();

    // ��ǰ��ͼ
    Json::Map * map;

public:
    static WorldManager * GetInstance();
    ~WorldManager();

    // ��ʼ�����磨�״ν�����Ϸ���������磩
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
    // savesName �浵����
    bool Load();

    // ��ȡ��ͼ����
    Json::Map GetMapInfo();

private:
    // ���������
    // num ������������
    bool RandomCreateTree(int num);

private:
    // ������������
    void SaveWorld();

    // ������ͼ
    // width ��ͼ���
    // length ��ͼ����
    // return �����ͼ��id
    //int CreateMap(int width, int length);
};