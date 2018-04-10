#include "map_db.h"

MapDB * MapDB::shareInstance = nullptr;

const std::string MapDB::TABLE_NAME = "map";
const std::string MapDB::FIELD_ID = "id";
const std::string MapDB::FIELD_INFO = "info";
const std::string MapDB::FIELD_CONFIG = "config";


MapDB::MapDB() {

}


MapDB * MapDB::GetInstance() {

    if (shareInstance == nullptr) {
        shareInstance = new MapDB();
    }
    return shareInstance;
}

MapDB::~MapDB() {
}

bool MapDB::CreateTable() {
    SqliteTool * sqliteTool = SqliteTool::GetInstance();
    std::string sql = "CREATE TABLE IF NOT EXISTS `" + MapDB::TABLE_NAME + "` ("
        + "`" + MapDB::FIELD_ID + "` INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,"
        + "`" + MapDB::FIELD_INFO + "` TEXT NOT NULL,"
        + "`" + MapDB::FIELD_CONFIG + "` TEXT NOT NULL"
        + ");";

    return sqliteTool->ExecSql(sql.c_str());
}

bool MapDB::Insert(int id, int worldWidth, int worldLength) {
    std::string config = this->GetConfigJsonStr(worldWidth, worldLength);

    SqliteTool *sqliteTool = SqliteTool::GetInstance();
    std::string sql = "INSERT INTO `" + MapDB::TABLE_NAME
        + "` (`" + MapDB::FIELD_ID + "`, `" + MapDB::FIELD_INFO + "`, `" + MapDB::FIELD_CONFIG + "`)"
        + "VALUES (" + std::to_string(id) + ", '', '" + config + "')";
    return sqliteTool->ExecSql(sql.c_str());
}

bool MapDB::Insert(int id, std::string config, std::string info) {
    SqliteTool *sqliteTool = SqliteTool::GetInstance();
    std::string sql = "INSERT INTO `" + MapDB::TABLE_NAME
        + "` (`" + MapDB::FIELD_ID + "`, `" + MapDB::FIELD_INFO + "`, `" + MapDB::FIELD_CONFIG + "`)"
        + "VALUES (" + std::to_string(id) + ", '" + config + "', '" + config + "')";
    return sqliteTool->ExecSql(sql.c_str());
}

int MapDB::GetMaxId() {
    SqliteTool *sqliteTool = SqliteTool::GetInstance();
    std::string sql = "SELECT `" + MapDB::FIELD_ID + "` FROM `" + MapDB::TABLE_NAME + "` ORDER BY `" + MapDB::FIELD_ID + "` DESC LIMIT 0,1";

    std::list<std::map<std::string, std::string>> res = sqliteTool->Query(sql.c_str());

    int lastInsertId = 0;

    if (res.size() != 0) {
        lastInsertId = std::stoi((*res.begin())[MapDB::FIELD_ID].c_str());
    }

    return lastInsertId;
}

std::list<std::map<std::string, std::string>> MapDB::SelectAll() {
    SqliteTool * sqliteTool = SqliteTool::GetInstance();
    std::string sql = "SELECT `" + MapDB::FIELD_ID + "`, `" + MapDB::FIELD_CONFIG + "`, `" + MapDB::FIELD_INFO + "` FROM `" + MapDB::TABLE_NAME + "`";
    return sqliteTool->Query(sql.c_str());
}



std::string MapDB::GetConfigJsonStr(int worldWidth, int worldLength) {
    // 创建JSON对象
    rapidjson::Document document;
    rapidjson::Value configValue;
    configValue.SetObject();
    configValue.AddMember("worldWidth", worldWidth, document.GetAllocator());
    configValue.AddMember("worldHeight", worldLength, document.GetAllocator());

    // 把JSON对象转为json串
    rapidjson::StringBuffer sb;
    rapidjson::PrettyWriter<rapidjson::StringBuffer> writer(sb);
    configValue.Accept(writer);
    std::string config = sb.GetString();

    return config;
}
