using AutoMapper;
using cibus.application.DTOs;
using cibus.application.Interfaces.Services;
using cibus.domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cibus.application.MapProfiles
{
    public class DTOProfileMap : Profile
    {
        public DTOProfileMap()
        {
            #region ApplicationUser
            CreateMap<ApplicationUserDto, ApplicationUser>()
                .ReverseMap();
            #endregion
        }
    }
}
