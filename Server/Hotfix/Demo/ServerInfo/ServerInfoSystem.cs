using System.Collections.Generic;

namespace ET
{
    [FriendClass(typeof (ServerInfoComponent))]
    [FriendClass(typeof (ServerInfo))]
    public static class ServerInfoSystem
    {
        public class OnAwake: AwakeSystem<ServerInfoComponent>
        {
            public override void Awake(ServerInfoComponent self)
            {
                self.Initialize().Coroutine();
            }
        }

        public class OnLoad: LoadSystem<ServerInfoComponent>
        {
            public override void Load(ServerInfoComponent self)
            {
                self.Initialize().Coroutine();
            }
        }

        public class OnDestroy: DestroySystem<ServerInfoComponent>
        {
            public override void Destroy(ServerInfoComponent self)
            {
                foreach (var serverInfo in self.serverInfos)
                {
                    serverInfo?.Dispose();
                }

                self.serverInfos.Clear();
            }
        }

        public static async ETTask Initialize(this ServerInfoComponent self)
        {
            var serverInfoList = await DBManagerComponent.Instance.GetZoneDB(self.DomainZone()).Query<ServerInfo>(_ => true);

            if (serverInfoList == null || serverInfoList.Count <= 0) 
            {
                Log.Error("No Server Exist");
                return;
            }

            foreach (var serverInfo in serverInfoList)
            {
                self.AddChild<ServerInfo>();
                self.serverInfos.Add(serverInfo);
            }
            
            await ETTask.CompletedTask;
        }

        public static List<ServerInfo> GetAllServerInfo(this ServerInfoComponent self)
        {
            return self.serverInfos;
        }

        public static void FromMessage(this ServerInfo self, ServerInfoProto proto)
        {
            self.Id = proto.Id;
            self.status = proto.Status;
            self.serverName = proto.ServerName;
        }

        public static ServerInfoProto ToMessage(this ServerInfo self)
        {
            return new ServerInfoProto() { Id = (int)self.Id, Status = self.status, ServerName = self.serverName };
        }
    }
}