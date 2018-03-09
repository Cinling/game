#include "world.h"



World::World() {
}


World::~World() {
}

void World::SqliteTest() {
    char sqlfilename[] = "test.sqlite";
    sqlite3 *db;

    sqlite3_open(sqlfilename, &db);
}
