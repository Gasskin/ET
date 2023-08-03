namespace ET
{
    [MessageHandler]
    public class R2C_SayGoodbyeHandler: AMHandler<R2C_SayGoodbye>
    {
        protected override void Run(Session session, R2C_SayGoodbye message)
        {
            Log.Debug(message.Goodbye);
        }
    }
}