#include "main.h"

int main(void) {
    RecvClient::StartLintener(6000);
    ClientManager::StartClientConnectListener(6001);

    while (true) {
        Sleep(5000);
    }
}