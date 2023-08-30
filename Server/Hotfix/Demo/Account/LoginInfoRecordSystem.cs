namespace ET
{
    [FriendClass(typeof (LoginInfoRecordComponent))]
    public static class LoginInfoRecordSystem
    {
        public class OnDestroy: DestroySystem<LoginInfoRecordComponent>
        {
            public override void Destroy(LoginInfoRecordComponent self)
            {
                self.accountLoginInfoDic.Clear();
            }
        }

        public static void Add(this LoginInfoRecordComponent self, long key, int value)
        {
            if (self.accountLoginInfoDic.ContainsKey(key))
            {
                self.accountLoginInfoDic[key] = value;
                return;
            }

            self.accountLoginInfoDic.Add(key, value);
        }

        public static void Remove(this LoginInfoRecordComponent self, long key)
        {
            self.accountLoginInfoDic.Remove(key);
        }
        
        public static int Get(this LoginInfoRecordComponent self, long key)
        {
            if (self.accountLoginInfoDic.TryGetValue(key,out var value))
            {
                return value;
            }

            return -1;
        }

        public static bool Exist(this LoginInfoRecordComponent self, long key)
        {
            return self.accountLoginInfoDic.ContainsKey(key);
        }
    }
}