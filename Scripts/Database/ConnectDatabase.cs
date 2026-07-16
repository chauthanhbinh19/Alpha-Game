public static class DatabaseConfig
{
    // public static string ConnectionString => "Server=localhost; Port=3306; Database=alpha; User=root; Password=binh123456; Pooling=true; Max Pool Size=100;";
    public static string ConnectionString => "Server=127.0.0.1; Port=3306; Database=alpha; User=root; Password=binh123456; Pooling=true; Min Pool Size=10; Max Pool Size=150;";
}
