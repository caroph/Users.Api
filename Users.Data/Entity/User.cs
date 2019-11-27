using System;
using System.Collections.Generic;
using System.Text;
using Users.Data.Entity.Command;

namespace Users.Data.Entity
{
    public class User
    {
        public User() { }

        public User(UserCommand userCommand)
        {
            Name = userCommand.Name;
            Password = userCommand.Password;
            Email = userCommand.Email;
            Guid = userCommand.Guid;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Guid { get; set; }
    }
}
