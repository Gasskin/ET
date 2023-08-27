using System.Collections.Generic;

namespace ET
{
    [ComponentOf(typeof(Scene))]
    public class AccountSessionsComponent: Entity,IAwake,IDestroy
    {
        public Dictionary<long, long> AccountSessionsDic { get; } = new Dictionary<long, long>();
    }
}