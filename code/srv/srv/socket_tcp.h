#pragma once

#include <winsock2.h>
#include <iostream>

#pragma comment(lib, "ws2_32.lib")

using namespace std;

// tcp 通信类
class SocketTcp {
private:
    // 服务端套接字
    SOCKET socketServer;
    // 判断是否暂停socket的状态
    bool isPause;

private:
    SocketTcp();

public:
    SocketTcp(unsigned short port);
    ~SocketTcp();

    // 开启socket监听
    void StartSocket();
    // 暂停socket监听
    void PauseSocket();
    // 关闭socket
    void DestroySocket();

private:
    // 初始化服务端套接字
    void InitServerSocket(unsigned short port);

    // 将 int32 的IP数据转为可读形式的ip
    string GetIp(sockaddr_in socketAddrIn);
};