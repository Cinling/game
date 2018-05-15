#include "role_db.h"


RoleDB * RoleDB::shareInstance = nullptr;

const std::string RoleDB::TABLE_NAME = "role";
const std::string RoleDB::FIELD_ID = "id";
const std::string RoleDB::FIELD_TYPE = "type";
const std::string RoleDB::FIELD_X = "x";
const std::string RoleDB::FIELD_Y = "y";
const std::string RoleDB::FIELD_Z = "z";
const std::string RoleDB::FIELD_ROTATION = "rotation";
const std::string RoleDB::FIELD_INFO = "info";

RoleDB::RoleDB() {
}

RoleDB * RoleDB::GetInstance() {

    if (shareInstance == nullptr) {
        shareInstance = new RoleDB();
    }
    return shareInstance;
}


RoleDB::~RoleDB() {
}

bool RoleDB::CreateTable() {
    SqliteTool * sqliteTool = SqliteTool::GetInstance();
    std::string sql = "CREATE TABLE IF NOT EXISTS `" + RoleDB::TABLE_NAME + "` ("
        + "`" + RoleDB::FIELD_ID + "` INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL"
        + ",`" + RoleDB::FIELD_TYPE + "` INTEGER NOT NULL"
        + ",`" + RoleDB::FIELD_X + "` FLOAT NOT NULL"
        + ",`" + RoleDB::FIELD_Y + "` FLOAT NOT NULL"
        + ",`" + RoleDB::FIELD_Z + "` FLOAT NOT NULL"
        + ",`" + RoleDB::FIELD_ROTATION + "` FLOAT NOT NULL"
        + ",`" + RoleDB::FIELD_INFO + "` TEXT NOT NULL"
        + ");";

    return sqliteTool->ExecSql(sql.c_str());
}

bool RoleDB::InsertOnce(int id, int type, Tool::Struct::Vector3 position, float rotation, std::string info) {
    std::string str_id = std::to_string(id);
    std::string str_type = std::to_string(type);
    std::string str_x = std::to_string(position.x);
    std::string str_y = std::to_string(position.y);
    std::string str_z = std::to_string(position.z);
    std::string str_rotation = std::to_string(rotation);

    std::string sql = "INSERT INTO `" + RoleDB::TABLE_NAME + "`("
        + RoleDB::FIELD_ID + "," + RoleDB::FIELD_TYPE + "," + RoleDB::FIELD_X + "," + RoleDB::FIELD_Y + "," + RoleDB::FIELD_Z + "," + RoleDB::FIELD_ROTATION + "," + RoleDB::FIELD_INFO + ") VALUES"
        + "(" + str_id + ", " + str_type + ", " + str_x + ", " + str_y + ", " + str_z + ", " + str_rotation + ", '" + info + "')";

    SqliteTool * sqlite = SqliteTool::GetInstance();
    return sqlite->ExecSql(sql.c_str());
}

bool RoleDB::InsertOnce(Row row) {
    return this->InsertOnce(row.id, row.type, row.position, row.rotation, row.info);
}

bool RoleDB::InsertMultiple(std::vector<Row> &rowList) {
    std::string sql = "INSERT INTO `" + RoleDB::TABLE_NAME + "`("
        + RoleDB::FIELD_ID + "," + RoleDB::FIELD_TYPE + "," + RoleDB::FIELD_X + "," + RoleDB::FIELD_Y + "," + RoleDB::FIELD_Z + "," + RoleDB::FIELD_ROTATION + "," + RoleDB::FIELD_INFO + ") VALUES ";

    for (std::vector<Row>::iterator it = rowList.begin(); it != rowList.end(); ++ it) {
        Row row = *it;

        std::string str_id = std::to_string(row.id);
        std::string str_type = std::to_string(row.type);
        std::string str_x = std::to_string(row.position.x);
        std::string str_y = std::to_string(row.position.y);
        std::string str_z = std::to_string(row.position.z);
        std::string str_rotation = std::to_string(row.rotation);
        std::string info = row.info;

        if (it != rowList.begin()) {
            sql += ",";
        }
        sql += "(" + str_id + ", " + str_type + ", " + str_x + ", " + str_y + ", " + str_z + ", " + str_rotation + ", '" + info + "')";
        
    }
    SqliteTool * sqlite = SqliteTool::GetInstance();
    return sqlite->ExecSql(sql.c_str());
}

std::list<std::map<std::string, std::string>> RoleDB::SelectAll() {
    SqliteTool * sqliteTool = SqliteTool::GetInstance();

    std::string sql = "SELECT * FROM `" + RoleDB::TABLE_NAME + "`";
    return sqliteTool->Query(sql.c_str());
}
