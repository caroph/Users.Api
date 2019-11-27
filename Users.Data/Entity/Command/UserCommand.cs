using System;
using System.Collections.Generic;
using System.Text;

namespace Users.Data.Entity.Command
{
    public class UserCommand
    {
        public UserCommand(string name, string email, string password, string guid)
        {
            Name = name;
            Email = email;
            Password = password;
            Guid = guid;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Guid { get; set; }
    }
}
