using AutoMapper;
using cibus.application.DTOs;
using cibus.application.Interfaces.BusinessLogics;
using cibus.application.Interfaces.Repositories;
using cibus.application.Interfaces.Services;
using cibus.domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace cibus.application.BusinessLogics
{
    public class UserBL : IUserBL
    {
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;
        private readonly IPasswordSecurityService _passwordSecurityService;
        private readonly ITokenService _tokenService;
        public UserBL(
            IUserRepository userRepo, 
            IMapper mapper,
            IPasswordSecurityService passwordSecurityService,
            ITokenService tokenService
            )
        {
            _userRepo = userRepo;
            _mapper = mapper;
            _passwordSecurityService = passwordSecurityService;
            _tokenService = tokenService;
        }

        public async Task<List<ApplicationUserDto>> Get()
        {
            return _mapper.Map<List<ApplicationUserDto>>(await _userRepo.Get());
        }

        public async Task<ApplicationUserDto> Get(int id)
        {
            return _mapper.Map<ApplicationUserDto>(await _userRepo.Get(id));
        }

        public async Task<ApplicationUserDto> Get(string email)
        {
            return _mapper.Map<ApplicationUserDto>(await _userRepo.Get(email));
        }

        public async Task<int> CreateUser(ApplicationUserDto appUserDTO)
        {
            ApplicationUser applicationUser = _mapper.Map<ApplicationUser>(appUserDTO);
            SetPasswordHashAndPasswordSalt(applicationUser, appUserDTO.Password);

            return await _userRepo.CreateUser(applicationUser);
        }

        public async Task<bool> IsEmailAlreadyExists(string email)
        {
            return await _userRepo.IsEmailAlreadyExists(email);
        }

        public async Task<Dictionary<int, string>> Authenticate(SigninDto signinDto)
        {
            Dictionary<int, string> status = new Dictionary<int, string>();

            var user = await _userRepo.Get(signinDto.Email);
            if (user == null)
            {
                status.Add(-1, "Invalid email, Please try again with a valid email address.");
                return status;
            }

            var isPasswordValid = _passwordSecurityService.ValidatePassword(user, signinDto.Password);
            if (!isPasswordValid)
            {
                status.Add(0, "Password is incorrect, Please try again with correct password.");
                return status;
            }

            var userRolesViewModel = await _userRepo.GetUserRoles(user.Id);
            var token = _tokenService.GenerateToken(userRolesViewModel.FirstOrDefault());
            status.Add(1, token);

            return status;
        }

        private ApplicationUser SetPasswordHashAndPasswordSalt(ApplicationUser appUser, string password)
        {
            var passwordEncryptionResult = _passwordSecurityService.GetPasswordHashAndPasswordSalt(password);

            appUser.PasswordHash = passwordEncryptionResult[1];
            appUser.PasswordSalt = passwordEncryptionResult[2];

            return appUser;
        }
    }
}
