using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

class RoleType {
    private static Dictionary<int, string> typeIdNameDict = null; 

    public static string GetPrebPathByRoleType(int roleType) {

        Dictionary<int, string>  typeConfig = GetTypeIdNameDict();
        string retStr = PrefabPath._3D.EmptyGamObject;

        if (typeConfig.ContainsKey(roleType)) {
            retStr = typeConfig[roleType];
        }
        return retStr;
    }

    private static Dictionary<int, string> GetTypeIdNameDict() {

        if (typeIdNameDict == null) {
            typeIdNameDict = new Dictionary<int, string>();
            typeIdNameDict[21000] = PrefabPath._3D.Tree;
        }

        return typeIdNameDict;
    }
}
