using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Users.Data.Entity.Command;
using Users.Data.Entity.Query;

namespace Users.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IPostgresRepository _postgresRepository;

        public UserRepository(IPostgresRepository postgresRepository) =>
            _postgresRepository = postgresRepository;

        public async Task Delete(string guid)
        {
            var sql = $"DELETE FROM public.\"User\" WHERE \"Guid\" like '{guid}'";

            await _postgresRepository.Connection.ExecuteAsync(sql);
        }

        public async Task<UserQuery> Get(string guid)
        {
            var sql = $"SELECT \"Id\", \"Name\", \"Email\", \"Password\", \"Guid\" FROM public.\"User\" WHERE \"Guid\" like '{guid}'";

            return await _postgresRepository.Connection.QueryFirstAsync<UserQuery>(sql);
        }

        public async Task<IEnumerable<UserQuery>> GetAll()
        {
            var sql = "SELECT \"Id\", \"Name\", \"Email\", \"Password\", \"Guid\" FROM public.\"User\";";

            return await _postgresRepository.Connection.QueryAsync<UserQuery>(sql);
        }

        public async Task<string> GetPassword(string guid)
        {
            var sql = $"SELECT \"Password\" FROM public.\"User\" WHERE \"Guid\" like '{guid}'";
            
            return await _postgresRepository.Connection.QueryFirstAsync<string>(sql);
        }

        public async Task<UserQuery> Save(UserCommand userCommand)
        {
            var sql = $"INSERT INTO public.\"User\" (\"Name\", \"Email\", \"Password\", \"Guid\") VALUES (@Name, @Email, @Password, @Guid)";

            await _postgresRepository.Connection.ExecuteAsync(sql, userCommand);

            return await Get(userCommand.Guid);
        }

        public async Task<UserQuery> Update(UserCommand userCommand)
        {
            var sql = $"UPDATE public.\"User\" SET \"Name\" = @Name, \"Password\" = @Password, \"Email\" = @Email WHERE \"Guid\" LIKE @Guid";

            await _postgresRepository.Connection.ExecuteAsync(sql, userCommand);

            return await Get(userCommand.Guid);
        }
    }
}
