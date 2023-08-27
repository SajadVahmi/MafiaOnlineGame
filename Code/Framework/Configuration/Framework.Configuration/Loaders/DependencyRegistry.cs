namespace Framework.Configuration.Loaders
{
    internal static class DependencyRegistry
    {
        public static IDependencyRegister Current { get; private set; } = null!;

        public static void SetCurrent(IDependencyRegister registry)
        {
            Current = registry;
        }
    }
}