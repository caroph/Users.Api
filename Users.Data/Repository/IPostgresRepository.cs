using System;
using System.Data;

namespace Users.Data.Repository
{
    public interface IPostgresRepository : IDisposable
    {
        IDbConnection Connection { get; }
    }
}
