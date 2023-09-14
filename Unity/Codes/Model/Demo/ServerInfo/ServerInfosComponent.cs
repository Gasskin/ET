using System.Collections.Generic;

namespace ET
{
    [ComponentOf(typeof(Scene))]
    [ChildType(typeof(ServerInfo))]
    public class ServerInfosComponent: Entity,IAwake,IDestroy
    {
        public List<ServerInfo> serverInfos = new List<ServerInfo>();
    }
}