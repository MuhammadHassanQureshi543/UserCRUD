using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Application.DTO;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class UserServices : IUserService
    {
        private readonly AppDbContext _dbContext;
        public UserServices(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<UserVM>> GetAll()
        {
            var data = await _dbContext.Users.ToListAsync();

            var userData = data.Select(user => new UserVM
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                phone = user.phone,
            }).ToList();

            return userData;
        }

        public async Task<string> RegisterUser(RegisterUser model)
        {
            if (model == null)
                return "Bad Request";

            var user = new User()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Password = model.Password,
                phone = model.phone,
                Address = model.Address,
            };

            await _dbContext.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return "User Register Successful";
        }

        public async Task<string> UpdateUser(int id,RegisterUser model)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);

                user.Id = id;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.phone = model.phone;

            await _dbContext.SaveChangesAsync();

            return "Update Successful";
        }

        public async Task<string> DelteUser(int id)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x=>x.Id== id);
            if (user == null) return "User Not Found";

            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();

            return "User is Delted Successful";
        }
    }
}
