using System.Collections.Generic;
using System.Threading.Tasks;
using Users.Data.Entity.Command;
using Users.Data.Entity.Query;

namespace Users.Data.Repository
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserQuery>> GetAll();
        Task<UserQuery> Get(string guid);
        Task Delete(string guid);
        Task<UserQuery> Update(UserCommand userCommand);
        Task<UserQuery> Save(UserCommand userCommand);
        Task<string> GetPassword(string guid);
    }
}
