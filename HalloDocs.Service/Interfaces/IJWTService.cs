﻿using HalloDocs.Entities.DataModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloDocs.Service.Interfaces
{
    public interface IJwtService
    {
        string GenerateJwtToken(AspNetUser aspNetUser);

        bool ValidateToken(string token, out JwtSecurityToken jwtSecurityToken);
    }
}
