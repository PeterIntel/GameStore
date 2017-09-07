namespace GameStore.Logging.Loggers
{
    public interface IMongoLogger<TDomain> where TDomain : class
    {
        void Write(Operation operation, TDomain currentEntity);
        void Write(Operation operation, TDomain currentEntity, TDomain updatedEntity);
    }
}
