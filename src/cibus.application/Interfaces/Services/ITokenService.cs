using cibus.domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cibus.application.Interfaces.Services
{
    public interface ITokenService
    {
        string GenerateToken(int userId);
    }
}
