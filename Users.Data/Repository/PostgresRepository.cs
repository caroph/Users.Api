using Microsoft.Extensions.Options;
using Npgsql;
using System;
using System.Data;
using Users.Infra.Options;

namespace Users.Data.Repository
{
    public class PostgresRepository : IPostgresRepository
    {
        private NpgsqlConnection _connection;

        public PostgresRepository(IOptions<AppSettings> options)
        {
            _connection = new NpgsqlConnection(options.Value.PostgresConnection);
            _connection.Open();
        }

        public IDbConnection Connection => _connection;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _connection?.Dispose();
                _connection = null;
            }
        }

        ~PostgresRepository() => Dispose(false);
    }
}
