using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace Jp.UI.SSO.Configuration
{
    public class DbHealthChecker
    {
        public bool TestConnection(DbContext context)
        {
            DbConnection conn = context.Database.GetDbConnection();
            try
            {
                conn.Open();   // Check the database connection

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
