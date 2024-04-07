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

        public async Task<List<ApplicationUserForRetrieveDto>> Get()
        {
            return _mapper.Map<List<ApplicationUserForRetrieveDto>>(await _userRepo.Get());
        }

        public async Task<ApplicationUserForRetrieveDto> Get(int id)
        {
            return _mapper.Map<ApplicationUserForRetrieveDto>(await _userRepo.Get(id));
        }

        public async Task<ApplicationUserForRetrieveDto> Get(string email)
        {
            return _mapper.Map<ApplicationUserForRetrieveDto>(await _userRepo.Get(email));
        }

        public async Task<int> CreateUser(ApplicationUserForCreationDto appUserDTO)
        {
            ApplicationUser appUser = _mapper.Map<ApplicationUser>(appUserDTO);
            SetPasswordHashAndPasswordSalt(appUser, appUserDTO.Password);

            return await _userRepo.CreateUser(appUser);
        }

        private ApplicationUser SetPasswordHashAndPasswordSalt(ApplicationUser appUser, string password)
        {
            var passwordEncryptionResult = _passwordSecurityService.GetPasswordHashAndPasswordSalt(password);

            appUser.PasswordHash = passwordEncryptionResult[1];
            appUser.PasswordSalt = passwordEncryptionResult[2];

            return appUser;
        }

        public async Task<int> IsEmailAlreadyExists(string email)
        {
            return await _userRepo.IsEmailAlreadyExists(email);
        }

        public async Task<Dictionary<int, string>> Authenticate(SigninDto signinDto)
        {
            Dictionary<int, string> status = new Dictionary<int, string>();

            //signinDto.Password = _passwordSecurityService.Encrypt(signinDto.Password);

            var user = await _userRepo.Get(signinDto.Email);
            if (user == null)
            {
                status.Add(-1, "Invalid email, Please try again with a valid email address.");
                return status;
            }

            var resss = _passwordSecurityService.ValidatePassword(user, signinDto.Password);
            if (resss == false)
            {
                status.Add(0, "Password is incorrect, Please try again with correct password.");
                return status;
            }

            var userRolesViewModel = await _userRepo.GetUserRoles(user.Id);
            var token = _tokenService.GenerateToken(userRolesViewModel.FirstOrDefault());
            status.Add(1, token);
            return status;
        }
    }
}
