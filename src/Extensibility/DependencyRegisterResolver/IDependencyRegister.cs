namespace DependencyRegisterResolver
{
    public interface IDependencyRegister<T>
    {
        T Register();
    }
}