#pragma once

#include "tool.h"
#include "json.h"

class MapManager {
private:
    static MapManager * shareInstance;
    MapManager();

public:
    static MapManager * GetInstance();
    ~MapManager();
};

