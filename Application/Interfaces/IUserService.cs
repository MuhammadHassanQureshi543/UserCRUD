using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<List<UserVM>> GetAll();
        Task<string> RegisterUser(RegisterUser model);
        Task<string> UpdateUser(int id,RegisterUser model);
        Task<string> DelteUser(int id);
    }
}
