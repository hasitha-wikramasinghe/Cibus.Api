using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cibus.application.Interfaces.Context
{
    public interface IDapperContext
    {
        public IDbConnection CreateConnection();
    }
}
