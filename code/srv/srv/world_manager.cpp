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

bool World::InitMap(float width, float length, float height) {
    this->map = new Json::Map(width, length, height);
    return true;
}

bool World::InitMap(Json::Map * map) {
    this->map = map;
    return false;
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

void World::SqliteTest() {

    //SqliteTool *sqliteTool = SqliteTool::GetInstance(sqlfilename);

    //const char * sql_create = "CREATE TABLE `test` (age INTEGER, name VARCHAR(20))";
    //sqliteTool->Query(sql_create);

    //const char * sql_insert = "INSERT INTO `test` (age, name) VALUES (10, 'aa'), (20, 'bb')";
    //sqliteTool->Query(sql_insert);

    //const char * sql_select = "SELECT * FROM `test`";
    //sqliteTool->Query(sql_select);

    //WorldDB *db = WorldDB::GetInstance();

    DBManager * dbManager = DBManager::GetInstance();
    SqliteTool::UseDB("test");
    //dbManager->DBUpdate();


}

void World::SaveWorld() {

}

//int World::CreateMap(int width, int length) {
//    MapDB *mapDB = MapDB::GetInstance();
//    int maxId = mapDB->GetMaxId();
//    mapDB->Insert(maxId, (int)this->width, (int)this->length);
//    return maxId;
//}
