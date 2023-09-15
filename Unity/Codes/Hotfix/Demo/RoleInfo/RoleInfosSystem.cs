using System;

namespace ET
{
    [FriendClass(typeof(RoleInfosComponent))]
    public static class RoleInfosSystem
    {
        public class OnAwake: AwakeSystem<RoleInfosComponent>
        {
            public override void Awake(RoleInfosComponent self)
            {
                foreach (RoleInfo roleInfo in self.roleInfos)
                {
                    roleInfo?.Dispose();
                }
                self.roleInfos.Clear();
                self.currentRoleId = 0;
            }
        }

        public static void Add(this RoleInfosComponent self, RoleInfo roleInfo)
        {
            self.roleInfos.Add(roleInfo);
        }
    }
}