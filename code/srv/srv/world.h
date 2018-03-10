#pragma once

#include "sqlite_tool.h"
#include "world_db.h"
#include "db_manager.h";

class World {
private:
    static World *shareInstance;
    World();

    // ������
    float worldWidth;
    // ���糤��
    float worldLength;

public:
    static World * GetInstance();
    ~World();

    // ��ʼ�����磨�״ν�����Ϸ���������磩
    void Init(float width, float height);

    // ��ʼ��Ϸ
    void Start();

    // ��ͣ��Ϸ
    void Pause();

    // ��������ͣ����Ϸ
    void Resume();

    // �˳���Ϸ���رշ���ˣ�
    void Exit();

    // ������Ϸ����
    void Save();

    // ������Ϸ����
    void Load();

    // sqlite ���Է���
    void SqliteTest();

private:
    // ������������
    void SaveWorld();
};