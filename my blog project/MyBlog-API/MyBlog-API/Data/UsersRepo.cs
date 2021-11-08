using Microsoft.EntityFrameworkCore;
using MyBlog_API.Domain;
using MyBlog_API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AppUser = MyBlog_API.Domain.AppUser;

namespace MyBlog_API.Data
{
    class UsersRepo : IUsersRepo
    {
        private readonly DataContext _context;
        public UsersRepo(DataContext context)
        {
            _context = context;
        }

        public async Task<List<AppUser>> GetAllUsers()
        {
            return await _context.Users.Select(u => new AppUser
                (u.Id, u.UserName)).ToListAsync();
        }
        public async Task<AppUser> GetUserById(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                return new AppUser(user.Id, user.UserName);
            }
            return null;
        }
        
        //register
        public async Task<AppUser> Register(AppUser user)
        {
            using var hmac = new HMACSHA512();
            var newuser = new Entities.AppUser
            {
                UserName = user.UserName,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(user.Password)),
                PasswordSalt = hmac.Key
            };

            await _context.Users.AddAsync(newuser);
            await _context.SaveChangesAsync();
            
            return new Domain.AppUser(newuser.Id, newuser.UserName);
        }
    }
}
