#include "map_db.h"

MapDB * MapDB::shareInstance = nullptr;

const std::string MapDB::TABLE_NAME = "map";
const std::string MapDB::FIELD_ID = "id";
const std::string MapDB::FIELD_INFO = "info";
const std::string MapDB::FIELD_CONFIG = "config";


MapDB::MapDB() {

}


MapDB * MapDB::GetInstance() {
    return nullptr;
}

MapDB::~MapDB() {
}

std::list<std::map<std::string, std::string>> MapDB::Save(std::list<std::map<std::string, std::string>>) {
    return std::list<std::map<std::string, std::string>>();
}

std::list<std::map<std::string, std::string>> MapDB::Load(std::list<std::map<std::string, std::string>>) {
    return std::list<std::map<std::string, std::string>>();
}
