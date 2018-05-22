#pragma once

#include "client.h"
#include "tool.h"
#include "socket_listener.h"

class ClientManager {
private:
    ClientManager();
    static ClientManager * shareInstance;

    // 客户端使用的线程map
    std::map<Client *, std::thread *> clientThreadMap;
public:
    static ClientManager * GetInstance();
    ~ClientManager();

    // 添加一个客户端
    void AddClient(Client * client, std::thread * sendDataThread);
    // 获取客户端列表
    std::map<Client *, std::thread *> GetClientList();

public:
    // 开启客户端长连接监听
    static void StartClientConnectListener(unsigned short port);
};

// 发送给客户端消息的线程
void LoopSendDataToClient(Client * client);

