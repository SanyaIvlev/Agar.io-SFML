namespace Agar.io_SFML.Engine;

public static class Service<T>
{
    public static T _service;
    
    public static T Get => _service;

    public static void Set(T service) 
        => _service = service;
}