using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Persistence
{
    public class StoredProcedureQurey
    {
        private readonly string _connectionString;
        public StoredProcedureQurey(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("abc");
        }

        public async Task<List<UserVM>> StoreProcedureGetAllUser()
        {
            var users = new List<UserVM>();

            using (var conntection = new SqlConnection(_connectionString))
            {
                await conntection.OpenAsync();

                using (var command = new SqlCommand("GetAllUserData", conntection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            users.Add(new UserVM
                            {
                                Id = reader.GetInt32(0),
                                FirstName = reader.GetString(1),
                                LastName = reader.GetString(2),
                                Email = reader.GetString(3),
                                phone = reader.GetString(4)
                            });
                        }
                    }
                }
            }
            return users;
        }

        public async Task<UserVM> StoredProcedureGetUserbyName(string name)
        {
            var user = new UserVM();
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("GetUserByName", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Name", name);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            user.Id = reader.GetInt32(0);
                            user.FirstName = reader.GetString(1);
                            user.LastName = reader.GetString(2);
                            user.Email = reader.GetString(3);
                            user.phone = reader.GetString(4);
                        };
                    }
                }
            }
            return user;
        }
    }
}
