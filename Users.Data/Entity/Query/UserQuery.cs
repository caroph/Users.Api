using System;
using System.Collections.Generic;
using System.Text;

namespace Users.Data.Entity.Query
{
    public class UserQuery
    {
        public UserQuery() { }

        public UserQuery(User user)
        {
            Id = user.Id;
            Name = user.Name;
            Email = user.Email;
            Guid = user.Guid;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Guid { get; set; }
    }
}
