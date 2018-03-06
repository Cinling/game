#pragma once

#include <winsock2.h>
#include <iostream>

#pragma comment(lib, "ws2_32.lib")

using namespace std;

// tcp ͨ����
class SocketTcp {
private:
    // ������׽���
    SOCKET socketServer;
    // �ж��Ƿ���ͣsocket��״̬
    bool isPause;

private:
    SocketTcp();

public:
    SocketTcp(unsigned short port);
    ~SocketTcp();

    // ����socket����
    void StartSocket();
    // ��ͣsocket����
    void PauseSocket();
    // �ر�socket
    void DestroySocket();

private:
    // ��ʼ��������׽���
    void InitServerSocket(unsigned short port);

    // �� int32 ��IP����תΪ�ɶ���ʽ��ip
    string GetIp(sockaddr_in socketAddrIn);
};