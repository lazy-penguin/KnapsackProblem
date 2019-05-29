using DataManagers;
using Microsoft.EntityFrameworkCore;

namespace KnapsackProblem.Utils
{
    public class ContextFactoryService
    {
        public string ConnectionString { private get; set; }

        public ContextFactoryService(string connectionString)
        {
            ConnectionString = connectionString;
        }
        public KnapsackContext GetContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<KnapsackContext>();
            optionsBuilder.UseLazyLoadingProxies().UseNpgsql(ConnectionString);
            return new KnapsackContext(optionsBuilder.Options);
        }
        
    }
}
