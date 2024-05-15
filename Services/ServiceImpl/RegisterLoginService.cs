using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AppSettings;
using Azure.Core;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ModelClass;
using ModelClass.Dto;
using ModelClass.ModelClass;
using Services.Interface;

namespace Services.ServiceImpl
{
    public class RegisterLoginService : ILogin
    {
        private readonly IConfiguration _configurations;
        private readonly  JwtClaimDetails _jwtClaimDetails;

        public RegisterLoginService(IConfiguration configurations , IOptions<JwtClaimDetails> jwtDetails)
        {
            _configurations = configurations;
            _jwtClaimDetails = jwtDetails.Value;
        }

        public string Login(LoginDto loginDto)
        {
            
            string token = CreateToken(loginDto);
            //return Ok(token);

            return token;
        }

        private string CreateToken(LoginDto user)
        {
            using SqlConnection connection = new SqlConnection(_configurations.GetConnectionString("ConnectionString"));
            connection.Open();
            try
            {
                if (string.IsNullOrEmpty(user.Username))
                {
                    throw new ArgumentException("Invalid username or password");
                }
                string hashedPassword = "";
                string role = "";
                //usp_storedprodname
                var storedprod = "ValidateUser";
                SqlCommand command = new SqlCommand(storedprod, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@UserName", SqlDbType.VarChar).Value = user.Username;

                hashedPassword = command.ExecuteScalar() as string;

                if (hashedPassword != null && BCrypt.Net.BCrypt.EnhancedVerify(user.Password, hashedPassword))
                {
                    string roleQuery = "GetRole";
                    SqlCommand roleCommand = new SqlCommand(roleQuery, connection);
                    roleCommand.CommandType = CommandType.StoredProcedure;
                    roleCommand.Parameters.Add("@UserName", SqlDbType.VarChar).Value = user.Username;
                    roleCommand.Parameters.Add("@Password", SqlDbType.VarChar).Value = hashedPassword;
                    var result = roleCommand.ExecuteScalar();
                    if (result != null)
                    {
                        role = Convert.ToString(result);
                    }
                }
                else
                {
                    Console.WriteLine("Invalid username or password");
                }

                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtClaimDetails.Key));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var claim = new List<Claim>
                {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, role)
                };

                var jwtSecurityToken = new JwtSecurityToken(
                    issuer: _jwtClaimDetails.Issuer,
                    audience: _jwtClaimDetails.Audience,
                    claims: claim,
                    expires: DateTime.Now.AddMinutes(10),
                    signingCredentials: signinCredentials
                );

                return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return e.Message;
            }
        }

        public string Register(RegisterDto registerDto)
        {
            var storedprod = "UserRegistration";
            DynamicParameters dp = new DynamicParameters();
            using SqlConnection connection = new SqlConnection(_configurations.GetConnectionString("ConnectionString"));
            connection.Open();
            string passwordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(registerDto.Password, 13);
            dp.Add("@Username", registerDto.Username);
            dp.Add("@Password", passwordHash);
            dp.Add("@Role",Role.User.ToString());
            var User = connection.Execute(storedprod, dp, commandType: CommandType.StoredProcedure);
            return "ok";
        }

    }
}
