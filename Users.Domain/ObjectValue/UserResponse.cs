using System;
using Users.Data.Entity.Query;

namespace Users.Domain.ObjectValue
{
    public class UserResponse
    {
        public UserResponse(UserQuery user)
        {
            Name = user.Name;
            Email = user.Email;
            Guid = user.Guid;
        }

        public string Guid { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
