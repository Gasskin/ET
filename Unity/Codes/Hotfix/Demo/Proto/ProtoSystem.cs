namespace ET
{
    [FriendClass(typeof(RoleInfo))]
    [FriendClass(typeof(ServerInfo))]
    public static class ProtoSystem
    {
    #region ServerInfo
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
    #endregion

    #region RoleInfo
        public static void FromMessage(this RoleInfo self, RoleInfoProto proto)
        {
            self.Id = proto.Id;
            self.name = proto.Name;
            self.state = proto.State;
            self.accountId = proto.AccountId;
            self.createTime = proto.CreateTime;
            self.serverId = proto.ServerId;
            self.lastLoginTime = proto.LastLoginTime;
        }

        public static RoleInfoProto ToMessage(this RoleInfo self)
        {
            return new RoleInfoProto()
            {
                Name = self.name,
                AccountId = self.accountId,
                CreateTime = self.createTime,
                Id = self.accountId,
                LastLoginTime = self.lastLoginTime,
                ServerId = self.serverId,
                State = self.state
            };
        }
    #endregion
        
        
        
    }
}