#include "main.h"

#include "world.h"

int main(void) {


    World w;
    w.SqliteTest();


    SocketTcp socketTcp(6000);
    socketTcp.StartSocket();
}