#include "main.h"

int main(void) {
    RecvClient::StartLintener(6000);

    while (true) {
        Sleep(5000);
    }
}