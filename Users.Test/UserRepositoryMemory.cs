using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Users.Data.Entity;
using Users.Data.Entity.Command;
using Users.Data.Entity.Query;
using Users.Data.Repository;
using Users.Infra;

namespace Users.Test
{
    public class UserRepositoryMemory : IUserRepository
    {
        private readonly List<User> usersList = Singleton<List<User>>.Instance();

        public async Task Delete(string guid)
        {
            await Task.CompletedTask;

            usersList.RemoveAll(x => x.Guid.Equals(guid));
            //usersList = usersList.Where(x => !x.Guid.Equals(guid));
        }

        public async Task<UserQuery> Get(string guid)
        {
            await Task.CompletedTask;
            var user = usersList.FirstOrDefault(x => x.Guid.Equals(guid));

            return user != null ? new UserQuery(user) : null;
        }

        public async Task<IEnumerable<UserQuery>> GetAll()
        {
            await Task.CompletedTask;
            var users = usersList.ToList();

            return users?.Select(user => new UserQuery(user));
        }

        public async Task<string> GetPassword(string guid)
        {
            await Task.CompletedTask;
            var user = usersList.FirstOrDefault(x => x.Guid.Equals(guid));
            return user != null ? user.Password : string.Empty;
        }

        public async Task<UserQuery> Save(UserCommand userCommand)
        {
            var user = new User(userCommand);

            usersList.Add(user);

            return await Get(userCommand.Guid);
        }

        public async Task<UserQuery> Update(UserCommand userCommand)
        {
            await Task.CompletedTask;
            var user = usersList.FirstOrDefault(x => x.Guid.Equals(userCommand.Guid));

            user.Email = userCommand.Email;
            user.Name = userCommand.Name;
            user.Password = userCommand.Password;

            return new UserQuery(user);
        }
    }
}
