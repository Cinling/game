#pragma once

#include <map>
#include <thread>
#include <functional>

#include <iostream>

#include <winsock2.h>

#pragma comment(lib, "ws2_32.lib")

class SocketListener {
private:
    static SocketListener * shareInstance;
    SocketListener();

private:
    std::map<unsigned short, SOCKET> portSocketMap;

public:
    static SocketListener * GetInstance();
    ~SocketListener();

    // 当接收到客户端连接时，触发的方法
    typedef std::function<bool(SOCKET clientSocket)> AcceptCallBack;
public:
    // 新建一个线程监听某端口的 TCP 连接
    void NewThreadListenPort(unsigned short port, AcceptCallBack listenCallback);

    // 新线程中执行的方法
    friend void ListenPort(SocketListener * socketListener, unsigned short port, SocketListener::AcceptCallBack listenCallback);
};

void ListenPort(SocketListener * socketListener, unsigned short port, SocketListener::AcceptCallBack listenCallback);



