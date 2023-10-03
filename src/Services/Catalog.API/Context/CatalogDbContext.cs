using MongoRepo.Context;

namespace Catalog.API.Context
{
    public class CatalogDbContext : ApplicationDbContext
    {
        static IConfiguration configurationLocal = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();
        static IConfiguration configurationSecret = new ConfigurationBuilder()
            .AddUserSecrets<CatalogDbContext>()
            .Build();
        static string connectionString = configurationSecret.GetConnectionString("Catalog.API");
        static string databaseName = configurationLocal.GetValue<string>("DatabaseName");

        public CatalogDbContext()
            : base(connectionString, databaseName)
        {
            Console.WriteLine(connectionString);
            Console.WriteLine(databaseName);
        }
    }
}
