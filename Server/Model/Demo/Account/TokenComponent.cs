using System.Collections.Generic;

namespace ET
{
    [ComponentOf(typeof(Scene))]
    public class TokenComponent: Entity, IAwake
    {
        public Dictionary<long, string> TokenDic { get; private set; } = new Dictionary<long, string>();
    }
}