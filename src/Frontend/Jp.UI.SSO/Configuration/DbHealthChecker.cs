using System;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace Jp.UI.SSO.Configuration
{
    public class DbHealthChecker
    {
        public bool TestConnection(DbContext context)
        {
            
            try
            {
                var testDb = context.Database.GetPendingMigrations();   // Check the database connection

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
