#pragma once

#include "sqlite_tool.h"
#include "db_manager.h";

class World {
private:
    static World *shareInstance;
    World();

    // ��ǰ��ͼid
    int id;
    // ������
    int worldWidth;
    // ���糤��
    int worldLength;

public:
    static World * GetInstance();
    ~World();

    // ��ʼ�����磨�״ν�����Ϸ���������磩
    void Init(int width, int length);

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

    // ������ͼ
    // width ��ͼ���
    // length ��ͼ����
    // return �����ͼ��id
    int CreateMap(int width, int length);
};