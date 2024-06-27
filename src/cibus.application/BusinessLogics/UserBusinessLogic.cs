#nullable enable

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
    public class UserBusinessLogic : IUserBusinessLogic
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IPasswordSecurityService _passwordSecurityService;
        private readonly ITokenService _tokenService;
        public UserBusinessLogic(
            IUserRepository userRepository, 
            IMapper mapper,
            IPasswordSecurityService passwordSecurityService,
            ITokenService tokenService
            )
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _passwordSecurityService = passwordSecurityService;
            _tokenService = tokenService;
        }

        public async Task<List<ApplicationUserDto>?> Get()
        {
            return _mapper.Map<List<ApplicationUserDto>>(await _userRepository.Get());
        }

        public async Task<ApplicationUserDto?> Get(int id)
        {
            return _mapper.Map<ApplicationUserDto>(await _userRepository.Get(id));
        }

        public async Task<ApplicationUserDto?> Get(string email)
        {
            return _mapper.Map<ApplicationUserDto>(await _userRepository.Get(email));
        }

        public async Task<int> CreateUser(ApplicationUserDto appUserDTO)
        {
            ApplicationUser applicationUser = _mapper.Map<ApplicationUser>(appUserDTO);
            SetPasswordHashAndPasswordSalt(applicationUser, appUserDTO.Password);

            return await _userRepository.CreateUser(applicationUser);
        }

        public async Task<bool> IsEmailAlreadyExists(string email)
        {
            return await _userRepository.IsEmailAlreadyExists(email);
        }

        public async Task<Dictionary<int, string>> Authenticate(SigninDto signinDto)
        {
            Dictionary<int, string> status = new Dictionary<int, string>();

            var user = await _userRepository.Get(signinDto.Email);
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

            var token = _tokenService.GenerateToken(user.Id);
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
