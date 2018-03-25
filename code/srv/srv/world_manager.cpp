#include "world_manager.h"



World * World::shareInstance = NULL;

World * World::GetInstance() {
    if (shareInstance == NULL) {
        shareInstance = new World();
    }
    return shareInstance;
}

World::World() {
}


World::~World() {
    if (shareInstance != NULL) {
        delete shareInstance;
        shareInstance = NULL;
    }
}

bool World::InitMap(float width, float length) {
    this->width = width;
    this->length = length;
    RoleCtrl * roleCtrl = RoleCtrl::GetInstance();
    Tool::Struct::Vector3 * position = new Tool::Struct::Vector3(1, 2, 3);
    Animal * role = roleCtrl->CreateRole<Animal>(position);
    
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
    mapDB->Insert(id, (int)width, (int)length);
    return false;
}

bool World::Load() {
    return false;
}

Json::Map World::GetMapInfo() {
    return Json::Map(this->width, this->length);
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

int World::CreateMap(int width, int length) {
    MapDB *mapDB = MapDB::GetInstance();
    int maxId = mapDB->GetMaxId();
    mapDB->Insert(maxId, (int)this->width, (int)this->length);
    return maxId;
}
