using System;
using System.Collections.Generic;
using System.Linq;

public static class ServiceContainer
{
    private static readonly Dictionary<Type, object> _singletons = new Dictionary<Type, object>();
    private static readonly Dictionary<Type, Type> _registrations = new Dictionary<Type, Type>();

    public static void RegisterInstance<TInterface>(TInterface instance)
    {
        _singletons[typeof(TInterface)] = instance;
    }

    // Chỉ lưu Registration Type vào Dictionary, KHÔNG tạo instance ngay
    public static void RegisterSingleton<TInterface, TImplementation>() 
        where TImplementation : TInterface
    {
        _registrations[typeof(TInterface)] = typeof(TImplementation);
    }

    // Khi GetService() mới bắt đầu Resolve các dependency
    public static TInterface GetService<TInterface>()
    {
        return (TInterface)Resolve(typeof(TInterface));
    }

    private static object Resolve(Type type)
    {
        // 1. Nếu đã tạo Instance rồi thì trả về ngay
        if (_singletons.TryGetValue(type, out var instance))
            return instance;

        // 2. Nếu chưa tạo, kiểm tra xem đã đăng ký chưa
        if (!_registrations.TryGetValue(type, out var implType))
            throw new Exception($"Chưa đăng ký service hoặc dependency: {type.Name}");

        // 3. Soi Constructor và đệ quy Resolve toàn bộ dependency
        var constructor = implType.GetConstructors().First();
        var parameters = constructor.GetParameters();
        var args = parameters.Select(p => Resolve(p.ParameterType)).ToArray();

        // 4. Khởi tạo instance, lưu vào cache Singleton và trả về
        var newInstance = constructor.Invoke(args);
        _singletons[type] = newInstance;
        return newInstance;
    }
}