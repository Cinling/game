#include "tool.h"

long long Tool::GetTimeSecond() {

    time_t t = time(NULL);
    tm *ptm = new tm;
    localtime_s(ptm, &t);

    int time = mktime(ptm);
    delete ptm;
    ptm = nullptr;

    return time;
}

