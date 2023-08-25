namespace ET
{
    public static class TokenSystem
    {
        public static void Add(this TokenComponent self, long key, string token)
        {
            self.TokenDic.Add(key, token);
            self.TimeOutRemoveKey(key, token).Coroutine();
        }

        public static string Get(this TokenComponent self, long key)
        {
            if (self.TokenDic.TryGetValue(key, out var token))
                return token;
            return string.Empty;
        }

        public static void Remove(this TokenComponent self, long key)
        {
            self.TokenDic.Remove(key);
        }

        private static async ETTask TimeOutRemoveKey(this TokenComponent self, long key, string tokenKey)
        {
            await TimerComponent.Instance.WaitAsync(60000);
            var token = self.Get(key);
            if (token == tokenKey)
                self.TokenDic.Remove(key);
        }
    }
}