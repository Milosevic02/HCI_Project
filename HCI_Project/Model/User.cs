using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Project.Model
{
    public enum UserRole
    {
        Admin,
        Visitor
    }
    [Serializable]
    public class User
    {
        private string username;
        private string password;
        private UserRole role;

        public User(string username, string password, UserRole role)
        {
            this.username = username;
            this.password = password;
            this.role = role;
        }

        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public UserRole Role { get => role; set => role = value; }


    }
}
