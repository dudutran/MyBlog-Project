using Microsoft.EntityFrameworkCore;
using MyBlog_API.Domain;
using MyBlog_API.Entities;
using MyBlog_API.Interfaces;
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
        private readonly ITokenService _tokenService;
        public UsersRepo(DataContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
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
            if (await UniqueUsername(user.UserName)) throw new Exception(message: "Username is already used");
            else
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

                return new Domain.AppUser { 
                    Id = newuser.Id, 
                    UserName = newuser.UserName, 
                    Token = _tokenService.CreateToken(newuser)
                };
            }
        }

        //username is unique
        private async Task<bool> UniqueUsername(string username)
        {
            return ( await _context.Users.AnyAsync(x => (x.UserName).ToLower() == username.ToLower()));
            
        }

        //login
        public async Task<AppUser> Login(AppUser user)
        {
            var founduser = await _context.Users
                .SingleOrDefaultAsync(x => x.UserName == user.UserName);
            if (founduser == null) throw new UnauthorizedAccessException("Invalid username");
            else
            {
                using var hmac = new HMACSHA512(founduser.PasswordSalt);
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(user.Password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != founduser.PasswordHash[i]) throw new UnauthorizedAccessException("Invalid password!");
                }
                //var loginUser = await GetUserById(founduser.Id);
                return new Domain.AppUser
                {
                    UserName = founduser.UserName,
                    Token = _tokenService.CreateToken(founduser)
                };
            }
            
        }
    }
}
