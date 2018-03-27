using System;
using System.Reflection;

class RoleType {
    static BaseRole GetPrebPathByRoleType(int roleType) {
        string className = "Tree";
        switch (roleType) {
            case 21000:
                className = "Tree";
                break;

        }

        Type type = Type.GetType(className);
        BaseRole baseRole = (BaseRole) type.Assembly.CreateInstance(type.Name);

        return baseRole;
    }
}
