using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace NetCoreIntro
{
    public class DbHealthCheck : IHealthCheck
    {
        private readonly string _connectionString;

        public DbHealthCheck(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
            CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                }
            }
            catch (SqlException)
            {
                return Task.FromResult(HealthCheckResult.Unhealthy());
            }

            return Task.FromResult(HealthCheckResult.Healthy());
        }
    }
}