using Users.Domain.ObjectValue;
using Users.Infra.DefaultObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Users.Domain.Services
{
    public interface IUserService
    {
        Task<ResultObject<IEnumerable<UserResponse>>> GetAll();
        Task<ResultObject<UserResponse>> Get(string guid);
        Task<ResultObject<UserResponse>> Save(UserRequest userRequest);
        Task<ResultObject<UserResponse>> Update(UserRequest userRequest, string guid);
        Task<ResultObject<int>> Delete(string guid);
    }
}
