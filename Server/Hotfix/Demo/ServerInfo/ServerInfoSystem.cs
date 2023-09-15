using System.Collections.Generic;

namespace ET
{
    [FriendClass(typeof (ServerInfosComponent))]
    [FriendClass(typeof (ServerInfo))]
    public static class ServerInfoSystem
    {
        public class OnAwake: AwakeSystem<ServerInfosComponent>
        {
            public override void Awake(ServerInfosComponent self)
            {
                self.Initialize().Coroutine();
            }
        }

        public class OnLoad: LoadSystem<ServerInfosComponent>
        {
            public override void Load(ServerInfosComponent self)
            {
                self.Initialize().Coroutine();
            }
        }

        public class OnDestroy: DestroySystem<ServerInfosComponent>
        {
            public override void Destroy(ServerInfosComponent self)
            {
                foreach (var serverInfo in self.serverInfos)
                {
                    serverInfo?.Dispose();
                }

                self.serverInfos.Clear();
            }
        }

        public static async ETTask Initialize(this ServerInfosComponent self)
        {
            var serverInfoList = await DBManagerComponent.Instance.GetZoneDB(self.DomainZone()).Query<ServerInfo>(_ => true);

            if (serverInfoList == null || serverInfoList.Count <= 0)
            {
                Log.Warning("No ServerInfo Exist,Start Create...");

                self.serverInfos.Clear();
                var serverInfoConfigs = LuBanComponent.Instance.GetAllTable().ServerInfoTable.DataList;
                foreach (var config in serverInfoConfigs)
                {
                    var serverInfo = self.AddChildWithId<ServerInfo>(config.Id);
                    serverInfo.serverName = config.ServerName;
                    serverInfo.status = (int)ServerStatus.Normal;
                    self.serverInfos.Add(serverInfo);
                    await DBManagerComponent.Instance.GetZoneDB(self.DomainZone()).Save(serverInfo);
                }
            }

            foreach (var serverInfo in serverInfoList)
            {
                self.AddChild<ServerInfo>();
                self.serverInfos.Add(serverInfo);
            }
        }

        public static List<ServerInfo> GetAllServerInfo(this ServerInfosComponent self)
        {
            return self.serverInfos;
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
    }
}