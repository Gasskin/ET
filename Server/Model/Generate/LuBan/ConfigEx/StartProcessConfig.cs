using System.Net;
using ET;

namespace cfg
{
    public partial class StartProcessConfig
    {
        private long sceneId;
        public long SceneId
        {
            get
            {
                if (this.sceneId == default)
                {
                    InstanceIdStruct instanceIdStruct = new InstanceIdStruct(Id, 0);
                    sceneId = instanceIdStruct.ToLong();
                }

                return this.sceneId;
            }
        }
        
        public string InnerIP => StartMachineConfig_Ref.InnerIP;
        public string OuterIP => StartMachineConfig_Ref.OuterIP;
        
        private IPEndPoint innerIPPort;
        public IPEndPoint InnerIPPort
        {
            get
            {
                return innerIPPort ??= NetworkHelper.ToIPEndPoint($"{StartMachineConfig_Ref.InnerIP}:{InnerPort}");
            }
        }
    }
}