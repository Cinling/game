#pragma once

#include "sqlite_tool.h"

class World {
private:
    static World *shareInstance;
    World();

public:
    static World * GetInstance();
    ~World();

    // ��ʼ�����磨�״ν�����Ϸ���������磩
    void Init();

    // ��ʼ��Ϸ
    void Start();

    // ��ͣ��Ϸ
    void Pause();

    // ��������ͣ����Ϸ
    void Resume();

    // ������Ϸ����
    void Save();

    // �˳���Ϸ���رշ���ˣ�
    void Exit();



    // sqlite ���Է���
    void SqliteTest();
};