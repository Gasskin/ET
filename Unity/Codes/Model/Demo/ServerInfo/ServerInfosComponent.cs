using System.Collections.Generic;

namespace ET
{
    [ComponentOf(typeof(Scene))]
    [ChildType(typeof(ServerInfo))]
    public class ServerInfosComponent: Entity,IAwake,IDestroy,ILoad
    {
        public List<ServerInfo> serverInfos = new List<ServerInfo>();
    }
}