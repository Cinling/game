#include "db_manager.h"

DBManager * DBManager::shareInstance = nullptr;

const std::string DBManager::TABLE_NAME = "db_version_manager";
const std::string DBManager::FIELD_ID = "id";
const std::string DBManager::FIELD_VERSION = "version";
const std::string DBManager::FIELD_EXEC_SQL = "exec_sql";
const std::string DBManager::FIELD_RUN_TIME = "run_time";

DBManager::DBManager() {
}


DBManager * DBManager::GetInstance() {
    if (shareInstance == nullptr) {
        shareInstance = new DBManager();
    }
    return shareInstance;
}

DBManager::~DBManager() {
}

bool DBManager::DBUpdate() {

    SqliteTool * sqliteTool = SqliteTool::GetInstance();
    // 获取数据库版本号
    int nextDBVersion = this->GetNextDBVersion();
    bool returnBool = false;

    std::string execSql = "";

    // 根据数据库版本进行逐条sql语句更新
    switch (nextDBVersion) {
    case 0:
        if (!this->UpdateOneVersion(nextDBVersion++, []() -> std::string {return DBManager::GetInstance()->_000000_CreateDBVersionManagerTable(); })) {
            break;
        }
    case 1:
        if (!this->UpdateOneVersion(nextDBVersion++, []() -> std::string {return DBManager::GetInstance()->_000001_CreateMapTable(); })) {
            break;
        }
    default:
        returnBool = true;
        break;
    }

    return returnBool;
}

unsigned int DBManager::GetNextDBVersion() {

    unsigned int nextVersion = 0;

    SqliteTool * sqliteTool = SqliteTool::GetInstance();

    if (sqliteTool->IsTableExists(DBManager::TABLE_NAME.c_str())) {
        std::string sql = "SELECT `" + DBManager::FIELD_VERSION + "` FROM `" + DBManager::TABLE_NAME + "` ORDER BY `" + DBManager::FIELD_VERSION + "` DESC LIMIT 0,1";
        std::list<std::map<std::string, std::string>> res = sqliteTool->Query(sql.c_str());

        if (res.size() == 0) {
            nextVersion = 0;
        } else {
            std::map<std::string, std::string> rowMap = *(res.begin());
            std::string versionString = rowMap[DBManager::FIELD_VERSION];
            nextVersion = std::stoi(versionString.c_str()) + 1;
        }
    }
    return nextVersion;
}

bool DBManager::Insert(int version, std::string execSql) {

    long long time = Tool::GetTimeSecond();

    std::string sql = "INSERT INTO `" + DBManager::TABLE_NAME + "` (`" + DBManager::FIELD_VERSION + "`,`" + DBManager::FIELD_EXEC_SQL + "`,`" + DBManager::FIELD_RUN_TIME + "`)"
        + "VALUES (" + std::to_string(version) + ", '" + execSql + "',  " + std::to_string(time) + ")";

    SqliteTool::GetInstance()->ExecSql(sql.c_str());

    return false;
}



bool DBManager::UpdateOneVersion(int version, std::function<std::string()> updateSqlLambda) {
    std::string execSql = updateSqlLambda();

    if (execSql == "") {
        return false;
    }

    this->Insert(version, execSql);

    return true;
}

std::string DBManager::GetReturnByUpdateDB(std::string sql) {

    std::string ret = "";

    if (SqliteTool::GetInstance()->ExecSql(sql.c_str())) {
        ret = sql;
    }
    return ret;
}

std::string DBManager::_000000_CreateDBVersionManagerTable() {
    std::string sql = "CREATE TABLE `" + DBManager::TABLE_NAME + "` ("
        + "`" + DBManager::FIELD_ID + "` INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,"
        + "`" + DBManager::FIELD_VERSION + "` INTEGER NOT NULL,"
        + "`" + DBManager::FIELD_EXEC_SQL + "` TEXT NOT NULL,"
        + "`" + DBManager::FIELD_RUN_TIME + "` INTEGER NOT NULL"
        + ");";
    return this->GetReturnByUpdateDB(sql);
}

std::string DBManager::_000001_CreateMapTable() {
    std::string sql = "CREATE TABLE `" + MapDB::TABLE_NAME + "` ("
        + "`" + MapDB::FIELD_ID + "` INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,"
        + "`" + MapDB::FIELD_INFO + "` TEXT NOT NULL,"
        + "`" + MapDB::FIELD_CONFIG + "` TEXT NOT NULL"
        + ");";
    return this->GetReturnByUpdateDB(sql);
}
