using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Users.Data.Entity.Command;
using Users.Data.Repository;
using Users.Domain.ObjectValue;
using Users.Infra;
using Users.Infra.DefaultObjects;

namespace Users.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository) =>
            _userRepository = userRepository;

        public async Task<ResultObject<IEnumerable<UserResponse>>> GetAll()
        {
            var usersQuery = await _userRepository.GetAll();
            var users = usersQuery.Select(x => new UserResponse(x));

            return new ResultObject<IEnumerable<UserResponse>>(true, users);
        }

        public async Task<ResultObject<UserResponse>> Get(string guid)
        {
            ResultObject<UserResponse> response;

            var userQuery = await _userRepository.Get(guid);
            if (userQuery != null)
            {
                var user = new UserResponse(userQuery);

                response = new ResultObject<UserResponse>(true, user);
            }
            else response = new ResultObject<UserResponse>(false);

            return response;
        }

        public async Task<ResultObject<UserResponse>> Save(UserRequest userRequest)
        {
            ResultObject<UserResponse> result;
            if (userRequest.IsValid)
            {
                var guid = Guid.NewGuid().ToString();
                var userCommand = new UserCommand(userRequest.Name, userRequest.Email,
                    Criptography.Generate(userRequest.Password, guid), guid);

                var userQuery = await _userRepository.Save(userCommand);
                var user = new UserResponse(userQuery);

                result = new ResultObject<UserResponse>(true, user);
            }
            else result = new ResultObject<UserResponse>(false);

            return result;
        }

        public async Task<ResultObject<UserResponse>> Update(UserRequest userRequest, string guid)
        {
            ResultObject<UserResponse> result;
            if (userRequest.IsValid && !string.IsNullOrEmpty(userRequest.OldPassword)
                && await PasswordIsValid(userRequest, guid))
            {
                var userCommand = new UserCommand(userRequest.Name, userRequest.Email,
                Criptography.Generate(userRequest.Password, guid), guid);
                var userQuery = await _userRepository.Update(userCommand);
                var user = new UserResponse(userQuery);

                result = new ResultObject<UserResponse>(true, user);
            }
            else result = new ResultObject<UserResponse>(false);

            return result;
        }

        private async Task<bool> PasswordIsValid(UserRequest userRequest, string guid)
        {
            var password = await _userRepository.GetPassword(guid);

            return !string.IsNullOrEmpty(password) && password
                .Equals(Criptography.Generate(userRequest.OldPassword, guid));
        }

        public async Task<ResultObject<int>> Delete(string guid)
        {
            await _userRepository.Delete(guid);
            return new ResultObject<int>(true);
        }
    }
}
