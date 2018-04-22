#include "main.h"

int main(void) {
    // 初始化游戏，加载配置文件
    GameCtrl::GetInstance()->Init();

    SocketTcp socketTcp(6000);
    socketTcp.StartSocket();
}