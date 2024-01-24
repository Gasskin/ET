using System.Collections.Generic;

namespace ET
{
    [ChildType(typeof(RoleInfo))]
    [ComponentOf(typeof(Scene))]
    public class RoleInfosComponent: Entity,IAwake,IDestroy
    {
        public List<RoleInfo> roleInfos = new();
        public long currentRoleId = 0;
    }
}