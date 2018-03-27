#include "world_manager.h"



World * World::shareInstance = nullptr;

World * World::GetInstance() {
    if (shareInstance == nullptr) {
        shareInstance = new World();
    }
    return shareInstance;
}

World::World() {
    this->map = nullptr;
}


World::~World() {
    if (shareInstance != nullptr) {
        delete shareInstance;
        shareInstance = nullptr;
    }

    if (this->map != nullptr) {
        delete this->map;
        this->map = nullptr;
    }
}

bool World::InitMap(Json::Map * map) {
    this->map = map;
    return true;
}

bool World::Start() {
    this->Load();
    return false;
}

bool World::Pause() {
    return false;
}

bool World::Resume() {
    return false;
}

bool World::Exit() {
    return false;
}

bool World::Save() {
    MapDB * mapDB = MapDB::GetInstance();
    //mapDB->Insert(id, (int)width, (int)length);
    return false;
}

bool World::Load() {
    return false;
}

Json::Map World::GetMapInfo() {
    return (*this->map);
}

bool World::CreateTree() {
    return false;
}


void World::SaveWorld() {

}

//int World::CreateMap(int width, int length) {
//    MapDB *mapDB = MapDB::GetInstance();
//    int maxId = mapDB->GetMaxId();
//    mapDB->Insert(maxId, (int)this->width, (int)this->length);
//    return maxId;
//}
