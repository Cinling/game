#include "world_manager.h"

#include "time.h"


WorldManager * WorldManager::shareInstance = nullptr;

WorldManager * WorldManager::GetInstance() {
    if (shareInstance == nullptr) {
        shareInstance = new WorldManager();
    }
    return shareInstance;
}

WorldManager::WorldManager() {
    this->map = nullptr;
}


WorldManager::~WorldManager() {
    if (shareInstance != nullptr) {
        delete shareInstance;
        shareInstance = nullptr;
    }

    if (this->map != nullptr) {
        delete this->map;
        this->map = nullptr;
    }
}

bool WorldManager::InitMap(Json::Map * map) {
    this->map = map;
    RoleCtrl::GetInstance()->Clear();// 清空所有角色
    this->RandomCreateTree(500);
    return true;
}

bool WorldManager::Start() {
    return false;
}

bool WorldManager::Pause() {
    return false;
}

bool WorldManager::Resume() {
    return false;
}

bool WorldManager::Exit() {
    return false;
}

bool WorldManager::Save() {
    bool retBool = false;

    MapDB * mapDB = MapDB::GetInstance();

    int mapId = 1;

    std::string config = this->map->ToJsonStr();

    retBool = mapDB->Insert(mapId, config, std::string());

    return retBool;
}

bool WorldManager::Load() {
    bool retBool = false;

    // 载入地图数据
    MapDB * mapDB = MapDB::GetInstance();
    std::list<std::map<std::string, std::string>> mapInfoList = mapDB->SelectAll();
    std::list<std::map<std::string, std::string>>::iterator it = mapInfoList.begin();
    if (it != mapInfoList.end()) {
        std::map<std::string, std::string> mapInfo = *it;
        std::string config = mapInfo[MapDB::FIELD_CONFIG];
        this->map = new Json::Map(config);
        retBool = true;
    }

    return retBool;
}

Json::Map WorldManager::GetMapInfo() {
    return (*this->map);
}

bool WorldManager::RandomCreateTree(int num) {
    int minX = 0;
    int minZ = 0;
    int maxX = (int) this->map->width;
    int maxZ = (int) this->map->length;

    RoleCtrl * roleCtrl = RoleCtrl::GetInstance();
    MapManager * mapManager = MapManager::GetInstance();

    // 设置随机种子
    srand((int) time(0));

    for (int i = 0; i < num; ++i) {
        int x = Tool::Math::Random(minX, maxX);
        int z = Tool::Math::Random(minZ, maxZ);

        roleCtrl->CreateRole<Tree>(new Tool::Struct::Vector3(x, 0, z));
    }

    return true;
}


void WorldManager::SaveWorld() {

}

//int World::CreateMap(int width, int length) {
//    MapDB *mapDB = MapDB::GetInstance();
//    int maxId = mapDB->GetMaxId();
//    mapDB->Insert(maxId, (int)this->width, (int)this->length);
//    return maxId;
//}
