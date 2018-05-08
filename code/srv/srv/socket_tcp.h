#pragma once

#include "socket_num_manager.h"
#include "tool.h"
#include "game_ctrl.h"

#include <iostream>
#include <string>
#include <winsock2.h>

#pragma comment(lib, "ws2_32.lib")

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

    // 用于客户端连接时，创建新线程使用
    friend void Friend_Client(SocketTcp * socketTcp, SOCKET client, sockaddr_in remoteAddr);

private:
    // 初始化服务端套接字
    void InitServerSocket(unsigned short port);

    // 将 int32 的IP数据转为可读形式的ip
    std::string GetIp(sockaddr_in socketAddrIn);

private:
    // 根据客户端返回的数据，执行相应的处理
    // clientData 客户端发送过来的数据
    // return 返回给客户端的数据
    std::string DoBySocketAction(std::string clientData);
};


// 用于客户端连接时，创建新线程使用
void Friend_Client(SocketTcp * socketTcp, SOCKET client, sockaddr_in remoteAddr);

// 开启服务端主逻辑线程，加载配置进入内存
void StartServer();