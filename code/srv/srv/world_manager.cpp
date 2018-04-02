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
    this->RandomCreateTree(500);
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
    bool retBool = false;

    MapDB * mapDB = MapDB::GetInstance();
    int mapId = mapDB->GetMaxId();
    retBool = mapDB->Insert(mapId, this->map->height, this->map->length);

    return retBool;
}

bool World::Load() {
    return false;
}

Json::Map World::GetMapInfo() {
    return (*this->map);
}

bool World::RandomCreateTree(int num) {
    int minX = 0;
    int minZ = 0;
    int maxX = (int) this->map->width;
    int maxZ = (int) this->map->length;

    RoleCtrl * roleCtrl = RoleCtrl::GetInstance();
    MapManager * mapManager = MapManager::GetInstance();

    for (int i = 0; i < num; ++i) {
        int x = Tool::Math::Random(minX, maxX);
        int z = Tool::Math::Random(minZ, maxZ);

        roleCtrl->CreateRole<Tree>(new Tool::Struct::Vector3(x, 0, z));
    }

    //#ifdef DEBUG
    //    roleCtrl->PrintRoleMap();
    //#endif


    return true;
}


void World::SaveWorld() {

}

//int World::CreateMap(int width, int length) {
//    MapDB *mapDB = MapDB::GetInstance();
//    int maxId = mapDB->GetMaxId();
//    mapDB->Insert(maxId, (int)this->width, (int)this->length);
//    return maxId;
//}
