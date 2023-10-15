using server.Model.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace server.DataBaseMigrator
{
    public class DatabaseMigrator
    {
        public static void Migrate(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                try
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
                    dbContext.Database.Migrate();
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}
