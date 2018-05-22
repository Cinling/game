#pragma once

#include "tool.h"

#include <thread>
#include <winsock2.h>

#pragma comment(lib, "ws2_32.lib")

class Client {
private:
    // 客户端 SOCKET
    SOCKET socket;
    // 需要发往该客户端的数据队列
    std::queue<std::string *> sendDataQueue;
public:
    Client(SOCKET socket);
    ~Client();

    // 添加一条需要发送到客户端的数据
    void AddSendData(std::string * data);

    // 发送数据到客户端
    // maxNum 调用一次发送的数据条数
    void Send(unsigned short maxNum);
};

