#include "db_manager.h"

DBManager * DBManager::shareInstance = nullptr;

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
    unsigned int dbVersion = this->GetDBVersion();
    bool returnBool = false;

    std::string execSql = "";

    // 根据数据库版本进行逐条sql语句更新
    switch (dbVersion) {
    case 0:
        if (this->UpdateOneVersion(0, []() -> std::string {return DBManager::GetInstance()->DB_000000_CreateManagerTable(SqliteTool::GetInstance()); })) {
            break;
        }
    case 1:
    default:
        returnBool = true;
        break;
    }

    return returnBool;
}

unsigned int DBManager::GetDBVersion() {

    unsigned int version = 0;

    SqliteTool * sqliteTool = SqliteTool::GetInstance();

    if (sqliteTool->IsTableExists(DBManager::TABLE_NAME.c_str())) {
        std::string sql = "SELECT " + DBManager::FIELD_VERSION + " FROM " + DBManager::TABLE_NAME + " GROUP BY " + DBManager::FIELD_VERSION + " DESC LIMIT 0.1";
        std::list<std::map<std::string, std::string>> res = sqliteTool->Query(sql.c_str());

        if (res.size() == 0) {
            version = 0;
        } else {
            std::map<std::string, std::string> rowMap = *(res.begin());
            std::string versionString = rowMap[DBManager::FIELD_VERSION];
            version = std::atoi(versionString.c_str());
        }
    }
    return version;
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

std::string DBManager::DB_000000_CreateManagerTable(SqliteTool * sqliteTool) {
    std::string sql = "CREATE TABLE `" + DBManager::TABLE_NAME + "` ("
        + "`" + DBManager::FIELD_ID + "`    INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,"
        + "`" + DBManager::FIELD_VERSION + "` INTEGER NOT NULL,"
        + "`" + DBManager::FIELD_EXEC_SQL + "`TEXT    NOT NULL,"
        + "`" + DBManager::FIELD_RUN_TIME + "`INTEGER NOT NULL"
        + ");";

    if (sqliteTool->ExecSql(sql.c_str())) {
        return sql;
    }

    return "";
}
