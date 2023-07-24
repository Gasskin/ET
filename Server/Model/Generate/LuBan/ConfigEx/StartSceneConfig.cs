using System.Net;
using ET;

namespace cfg
{
    public partial class StartSceneConfig
    {
        // 唯一ID
        private long instanceID;
        public long InstanceID
        {
            get
            {
                if (this.instanceID == default) 
                {
                    InstanceIdStruct instanceIdStruct = new InstanceIdStruct(this.StartProcessConfig, (uint) this.Id);
                    instanceID = instanceIdStruct.ToLong();
                }
                return this.instanceID;
            }
        }

        // 内网地址外网端口，通过防火墙映射端口过来
        private IPEndPoint innerIPOutPort;
        public IPEndPoint InnerIPOutPort
        {
            get
            {
                return innerIPOutPort ??= NetworkHelper.ToIPEndPoint($"{StartProcessConfig_Ref.InnerIP}:{OuterPort}");
            }
        }
        
        
        private IPEndPoint outerIPPort;
        // 外网地址外网端口
        public IPEndPoint OuterIPPort
        {
            get
            {
                return outerIPPort ??= NetworkHelper.ToIPEndPoint($"{StartProcessConfig_Ref.OuterIP}:{OuterPort}");
            }
        }
    }
}