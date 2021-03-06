using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog_API.Domain
{
    public class AppUser
    {
        public AppUser() { }
        public AppUser(int Id, string UserName, string Token)
        {
            this.Id = Id;
            this.UserName = UserName;
            this.Token = Token;
        }
        public AppUser(int Id, string UserName)
        {
            this.Id = Id;
            this.UserName = UserName;
        }
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
