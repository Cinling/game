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

void World::Init() {
}

void World::Start() {
}

void World::Pause() {
}

void World::Resume() {
}

void World::Save() {
}

void World::Exit() {
}

void World::SqliteTest() {
    char sqlfilename[] = "test";

    SqliteTool *sqliteTool = SqliteTool::GetInstance(sqlfilename);

    const char * sql_create = "CREATE TABLE `test` (age INTEGER, name VARCHAR(20))";
    sqliteTool->Query(sql_create);

    const char * sql_insert = "INSERT INTO `test` (age, name) VALUES (10, 'aa'), (20, 'bb')";
    sqliteTool->Query(sql_insert);

    const char * sql_select = "SELECT * FROM `test`";
    sqliteTool->Query(sql_select);
}
