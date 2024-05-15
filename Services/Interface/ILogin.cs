using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using ModelClass.Dto;

namespace Services.Interface
{
    public interface ILogin
    {
        public string Register(RegisterDto registerDto);
        public string Login(LoginDto loginDto);
    }
}
