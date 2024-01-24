using System;

namespace ET
{
    [FriendClass(typeof (ServerInfo))]
    [FriendClass(typeof(ServerInfosComponent))]
    public static class ServerInfoSystem
    {
        public class OnDestroy: DestroySystem<ServerInfosComponent>
        {
            public override void Destroy(ServerInfosComponent self)
            {
                foreach (ServerInfo serverInfo in self.serverInfos)
                {
                    serverInfo?.Dispose();
                }
                self.serverInfos.Clear();
            }
        }
        
        // public static void FromMessage(this ServerInfo self, ServerInfoProto proto)
        // {
        //     self.Id = proto.Id;
        //     self.status = proto.Status;
        //     self.serverName = proto.ServerName;
        // }
        //
        // public static ServerInfoProto ToMessage(this ServerInfo self)
        // {
        //     return new ServerInfoProto() { Id = (int)self.Id, Status = self.status, ServerName = self.serverName };
        // }

        public static void Add(this ServerInfosComponent self, ServerInfo serverInfo)
        {
            self.serverInfos.Add(serverInfo);
        }
    }
}