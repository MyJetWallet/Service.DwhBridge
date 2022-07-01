using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Service.DwhBridge.Database.DesignTime;

public class ContextFactory : IDesignTimeDbContextFactory<DwhContext>
{
    public ContextFactory()
    {
    }
    
    public DwhContext CreateDbContext(string[] args)
    {
        var optionBuilder = new DbContextOptionsBuilder();
        optionBuilder.UseSqlServer();

        return new DwhContext(optionBuilder.Options);
    }
}