using cfg;

namespace ET
{
    [ComponentOf(typeof(Scene))]
    public class LuBanComponent: Entity,IAwake,IDestroy
    {
        public static LuBanComponent Instance;
        public Tables tables;
    }
}