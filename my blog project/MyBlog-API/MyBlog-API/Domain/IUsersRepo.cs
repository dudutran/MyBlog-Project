using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog_API.Domain
{
    public interface IUsersRepo
    {
        Task<List<AppUser>> GetAllUsers();
        Task<AppUser> GetUserById(int id);
        Task<AppUser> Register(AppUser user);
    }
}
