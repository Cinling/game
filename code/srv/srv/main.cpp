#include "main.h"

int main(void) {
    World *w = World::GetInstance();
    w->SqliteTest();

    SocketTcp socketTcp(6000);
    socketTcp.StartSocket();
}