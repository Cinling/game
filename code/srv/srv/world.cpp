#include "world.h"



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

void World::Init(int width, int length) {
    this->worldWidth = width;
    this->worldLength = length;
    this->id = this->CreateMap(this->worldWidth, this->worldLength);
}

void World::Start() {
    this->Load();
}

void World::Pause() {
}

void World::Resume() {
}

void World::Exit() {
}

void World::Save() {
    MapDB * mapDB = MapDB::GetInstance();
    mapDB->Insert(id, worldWidth, worldLength);
}

void World::Load() {
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
    mapDB->Insert(maxId, this->worldWidth, this->worldLength);
    return maxId;
}
