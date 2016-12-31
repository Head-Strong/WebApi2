namespace DependencyRegisterResolver
{
    internal interface IDependencyRegister<out T>
    {
        T Register();

        T GetContainer { get; }
    }
}