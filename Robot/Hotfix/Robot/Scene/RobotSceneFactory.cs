namespace ET
{
    public static class RobotSceneFactory
    {
        public static async ETTask<Scene> Create(
            Entity parent,
            long id,
            long instanceId,
            int zone,
            string name,
            cfg.Enum.SceneType sceneType,
            cfg.StartSceneConfig startSceneConfig = null
        )
        {
            await ETTask.CompletedTask;
            Log.Info($"create scene: {sceneType} {name} {zone}");
            Scene scene = EntitySceneFactory.CreateScene(id, instanceId, zone, sceneType, name, parent);

            scene.AddComponent<MailBoxComponent, MailboxType>(MailboxType.UnOrderMessageDispatcher);

            switch (scene.SceneType)
            {
                case cfg.Enum.SceneType.Robot:
                    scene.AddComponent<RobotManagerComponent>();
                    break;
            }

            return scene;
        }
    }
}