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

namespace cibus.application.Repositories
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

        public async Task<ApplicationUser> Get(int id)
        {
            var query = "select * from dbo.ApplicationUser where Id = @Id";
            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<ApplicationUser>(query, new { id }) 
                    ?? throw new ArgumentOutOfRangeException($"We couldn't find a user for given Id: {id}");
            }
        }

        public async Task<List<ApplicationUser>> Get()
        {
            var query = "select * from dbo.ApplicationUser";
            using (var connection = _context.CreateConnection())
            {
                var applicationUsers = await connection.QueryAsync<ApplicationUser>(query);
                return applicationUsers.ToList();
            }
        }

        public async Task<ApplicationUser> Get(string email)
        {
            var query = "SELECT * FROM dbo.ApplicationUser WHERE Email = @Email";
            using (var connection = _context.CreateConnection())
            {
                var ApplicationUser = await connection.QueryFirstOrDefaultAsync<ApplicationUser>(query, new { email });
                return ApplicationUser;
            }
        }

        public async Task<int> CreateUser(ApplicationUser appUser)
        {
            var query = "insert into dbo.ApplicationUser (Email, HashedPassword, FirstName, LastName, NIC, DOB, CreatedBy, CreatedOn, ModifiedBy, ModifiedOn, ClientId, PasswordSalt) " +
                "values (@Email, @HashedPassword, @FirstName, @LastName, @NIC, @DOB, @CreatedBy, @CreatedOn, @ModifiedBy, @ModifiedOn, @ClientId, @PasswordSalt)" +
                "select cast(scope_identity() as int)";

            //var parameters = new DynamicParameters();
            //parameters.Add("Email", appUser.Email, DbType.String);
            //parameters.Add("HashedPassword", appUser.HashedPassword, DbType.String);
            //parameters.Add("FirstName", appUser.FirstName, DbType.String);
            //parameters.Add("LastName", appUser.LastName, DbType.String);
            //parameters.Add("NIC", appUser.NIC, DbType.String);
            //parameters.Add("DOB", appUser.DOB, DbType.DateTime);
            //parameters.Add("CreatedBy", appUser.CreatedBy, DbType.Int32);
            //parameters.Add("CreatedOn", appUser.CreatedOn, DbType.DateTime2);
            //parameters.Add("ModifiedBy", appUser.ModifiedBy, DbType.Int32);
            //parameters.Add("ModifiedOn", appUser.ModifiedOn, DbType.DateTime2);
            //parameters.Add("ClientId", appUser.ClientId, DbType.Int32);

            var parameters = new
            {
                Email = appUser.Email,
                FirstName = appUser.FirstName,
                LastName = appUser.LastName,
                NIC = appUser.NIC,
                DOB = appUser.DOB,
                CreatedBy = appUser.CreatedBy,
                CreatedOn = appUser.CreatedOn,
                ModifiedBy = appUser.ModifiedBy,
                ModifiedOn = appUser.ModifiedOn,
                ClientId = appUser.ClientId,
                HashedPassword = appUser.HashedPassword,
                PasswordSalt = appUser.PasswordSalt
            };

            using (var connection = _context.CreateConnection())
            {
                return await connection.QuerySingleAsync<int>(query, parameters);
            }
        }

        public async Task<int> IsEmailAlreadyExists(string email)
        {
            int appUserCount = 0;
            var query = "select * from dbo.ApplicationUser where Email = @Email";
            using (var connection = _context.CreateConnection())
            {
                var applicationUser = await connection.QueryFirstOrDefaultAsync<ApplicationUser>(query, new { email });
                appUserCount = applicationUser != null ? 1 : 0; 
            }
            return appUserCount;
        }

        //public async Task<int> ValidatePassword(SigninDto signinDto)
        //{
        //    int isCorrect = 0;
        //    var user = await Get(signinDto.Email);
        //    if (user == null) throw new ArgumentNullException($"There is no user account created for {signinDto.Email}.");

        //    if (user.HashedPassword == signinDto.Password)
        //    {
        //        isCorrect = 1;
        //    }
        //    return isCorrect;
        //}

        public async Task<List<UserRolesViewModel>> GetUserRoles(int userId)
        {
            var query = "SELECT * FROM dbo.VwUserRoles WHERE UserId = @UserId";
            using (var connection = _context.CreateConnection())
            {
                var userRoles = await connection.QueryAsync<UserRolesViewModel>(query, new { userId });
                return userRoles.ToList();
            }
        }
    }
}
