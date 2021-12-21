using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace CareSiteGrievanceTracker_API.Data
{
    public class DSN
    {
        public static IDbConnection AlgarinSandbox
        {
            get { return new SqlConnection(Startup.StaticConfig.GetConnectionString("Algarin-sandbox")); }

        }

        public static IDbConnection AnalyticsWeb
        {
            get { return new SqlConnection(Startup.StaticConfig.GetConnectionString("AnalyticsWeb")); }
        }
    }
}
