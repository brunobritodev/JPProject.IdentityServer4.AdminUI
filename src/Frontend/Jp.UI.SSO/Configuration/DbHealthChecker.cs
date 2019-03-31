using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Jp.UI.SSO.Configuration
{
    public class DbHealthChecker
    {
        public bool TestConnection(DbContext context)
        {

            try
            {
                Log.Information(context.Database.GetDbConnection().ConnectionString);
                context.Database.GetPendingMigrations();   // Check the database connection

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
