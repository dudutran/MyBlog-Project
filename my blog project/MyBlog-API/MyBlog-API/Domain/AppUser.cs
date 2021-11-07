using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog_API.Domain
{
    public class AppUser
    {
        public AppUser(int Id, string UserName)
        {
            this.Id = Id;
            this.UserName = UserName;
        }
        public int Id { get; set; }
        public string UserName { get; set; }
    }
}
