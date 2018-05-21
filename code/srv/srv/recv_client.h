#pragma once

#include "socket_listener.h"
#include "socket_num_manager.h"
#include "tool.h"

// 监听客户端发送的操作指令参数
class RecvClient {
private:
    RecvClient();
    static RecvClient * shareInstance;

public:
    static RecvClient * GetInstance();
    ~RecvClient();
    
    // 开始监听端口
    static void StartLintener(unsigned short port);
public:

private:
    // 根据客户端返回的数据，执行相应的处理
    // clientData 客户端发送过来的数据
    // return 返回给客户端的数据
    std::string DoBySocketAction(std::string clientData);
};

