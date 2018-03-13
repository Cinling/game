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

bool MapDB::Insert(int worldWidth, int worldHeight) {
    std::string config = this->GetConfigJsonStr(worldWidth, worldHeight);

    SqliteTool *sqliteTool = SqliteTool::GetInstance();
    std::string sql = "INSERT INTO `" + MapDB::TABLE_NAME + "` (`" + MapDB::FIELD_INFO + "`, `" + MapDB::FIELD_CONFIG + "`) VALUES ('', '" + config + "')";
    return sqliteTool->ExecSql(sql.c_str());
}

int MapDB::GetLastInsertId() {
    SqliteTool *sqliteTool = SqliteTool::GetInstance();
    std::string sql = "SELECT `" + MapDB::FIELD_ID + "` FROM `" + MapDB::TABLE_NAME + "` ORDER BY `"+MapDB::FIELD_ID+"` DESC LIMIT 0,1";

    std::list<std::map<std::string, std::string>> res = sqliteTool->Query(sql.c_str());

    int lastInsertId = 0;

    if (res.size() != 0) {
        lastInsertId = std::stoi((*res.begin())[MapDB::FIELD_ID].c_str());
    }

    return lastInsertId;
}



std::string MapDB::GetConfigJsonStr(int worldWidth, int worldHeight) {
    // 创建JSON对象
    rapidjson::Document document;
    rapidjson::Value configValue;
    configValue.SetObject();
    configValue.AddMember("worldWidth", worldWidth, document.GetAllocator());
    configValue.AddMember("worldHeight", worldHeight, document.GetAllocator());

    // 把JSON对象转为json串
    rapidjson::StringBuffer sb;
    rapidjson::PrettyWriter<rapidjson::StringBuffer> writer(sb);
    configValue.Accept(writer);
    std::string config = sb.GetString();

    return config;
}
