using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

public class CommLifecycleApiContextFactory : IDesignTimeDbContextFactory<CommLifecycleApiContext>
{
    public CommLifecycleApiContext CreateDbContext(string[] args)
    {
        // Get the current directory (where the command is run)
        var basePath = Directory.GetCurrentDirectory();

        // Load configuration manually
        var config = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.Development.json", optional: false)
            .Build();

        var connectionString = config.GetConnectionString("DefaultConnection");

        var optionsBuilder = new DbContextOptionsBuilder<CommLifecycleApiContext>();
        optionsBuilder.UseSqlServer(connectionString);

        return new CommLifecycleApiContext(optionsBuilder.Options);
    }
}