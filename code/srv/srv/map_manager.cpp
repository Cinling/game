#include "map_manager.h"



MapManager * MapManager::shareInstance = nullptr;


MapManager::MapManager() {
}

MapManager * MapManager::GetInstance() {
    if (MapManager::shareInstance == nullptr) {
        MapManager::shareInstance = new MapManager();
    }
    return MapManager::shareInstance;
}


MapManager::~MapManager() {
    if (MapManager::shareInstance != nullptr) {
        delete MapManager::shareInstance;
        MapManager::shareInstance = nullptr;
    }
}
