namespace Munq.RedisClient
{
    public interface IRedisCommand
    {
        byte[] Name { get; }
        object[] Parameters { get; }
    }
}