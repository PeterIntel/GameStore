namespace GameStore.Security
{
    public interface IHashGenerator<in T>
    {
        string Generate(T input);
    }
}