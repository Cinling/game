#include "world_db.h"

WorldDB * WorldDB::shareInstance = nullptr;

WorldDB::WorldDB() {

}


WorldDB * WorldDB::GetInstance() {
    return nullptr;
}

WorldDB::~WorldDB() {
}

std::list<std::map<std::string, std::string>> WorldDB::Save(std::list<std::map<std::string, std::string>>) {
    return std::list<std::map<std::string, std::string>>();
}

std::list<std::map<std::string, std::string>> WorldDB::Load(std::list<std::map<std::string, std::string>>) {
    return std::list<std::map<std::string, std::string>>();
}
