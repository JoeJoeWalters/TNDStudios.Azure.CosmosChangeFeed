using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(FunctionApp.Startup))]

namespace FunctionApp
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            CosmosClient cosmosClient = new CosmosClient(
                        "AccountEndpoint=https://localhost:8081/;AccountKey=C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==",
                        new CosmosClientOptions()
                        {
                        });

            ContainerProperties containerProperties = new ContainerProperties()
            {
                Id = "Container",
                PartitionKeyPath = "/pk",
                IndexingPolicy = new IndexingPolicy()
                {
                    Automatic = false,
                    IndexingMode = IndexingMode.Consistent                     
                }
            };

            Database db = cosmosClient.CreateDatabaseIfNotExistsAsync("Database").Result.Database;
            Container container = db.CreateContainerIfNotExistsAsync(containerProperties).Result.Container;
        }
    }
}
