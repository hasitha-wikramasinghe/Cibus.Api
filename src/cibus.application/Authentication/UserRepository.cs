#nullable enable

using AutoMapper.Configuration;
using Dapper;
using cibus.application.DTOs;
using cibus.application.Interfaces.Context;
using cibus.application.Interfaces.Repositories;
using cibus.domain.Entities;
using cibus.domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cibus.application.Authentication
{
    public class UserRepository : IUserRepository
    {
        private readonly IDapperContext _context;
        public UserRepository(
            IDapperContext context
            )
        {
            _context = context;
        }

        public async Task<ApplicationUser?> Get(int id)
        {
            var getApplicationUserByIdQuery = AuthenticationScripts.GetApplicationUserByIdQuery;
            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<ApplicationUser>(getApplicationUserByIdQuery,
                    new
                    {
                        id
                    });
            }
        }

        public async Task<List<ApplicationUser>?> Get()
        {
            var getAllApplicationUsersQuery = AuthenticationScripts.GetAllApplicationUsersQuery;
            using (var connection = _context.CreateConnection())
            {
                var applicationUsers = await connection.QueryAsync<ApplicationUser>(getAllApplicationUsersQuery);
                return applicationUsers.ToList();
            }
        }

        public async Task<ApplicationUser?> Get(string email)
        {
            var getApplicationUserByEmailQuery = AuthenticationScripts.GetApplicationUserByEmailQuery;
            using (var connection = _context.CreateConnection())
            {
                var ApplicationUser = await connection.QueryFirstOrDefaultAsync<ApplicationUser>(getApplicationUserByEmailQuery,
                    new
                    {
                        email
                    });
                return ApplicationUser;
            }
        }

        public async Task<int> CreateUser(ApplicationUser appUser)
        {
            var createUserCommand = AuthenticationScripts.CreateUserCommand;
            using (var connection = _context.CreateConnection())
            {
                return await connection.QuerySingleAsync<int>(createUserCommand, appUser);
            }
        }

        public async Task<bool> IsEmailAlreadyExists(string email)
        {
            var getApplicationUserByEmailQuery = AuthenticationScripts.GetApplicationUserByEmailQuery;
            using (var connection = _context.CreateConnection())
            {
                var applicationUser = await connection.QueryFirstOrDefaultAsync<ApplicationUser>(
                    getApplicationUserByEmailQuery,
                    new
                    {
                        email
                    });

                if (applicationUser is not null)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
