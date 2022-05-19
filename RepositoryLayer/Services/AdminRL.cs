using CommonLayer.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Services
{
    public class AdminRL : IAdminRL
    {
        private readonly IConfiguration configuration;

        public AdminRL(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public AdminLoginResponse AdminLogin(AdminLogin adminLogin)
        {
            AdminLoginResponse adminLloginResponse = new AdminLoginResponse();
            using (SqlConnection con = new SqlConnection(configuration["ConnectionString:BookStore"]))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("spAdminLogin", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@EmailId", adminLogin.EmailId);
                    cmd.Parameters.AddWithValue("@Password", adminLogin.Password);

                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            adminLloginResponse.AdminId = Convert.ToInt32(rdr["AdminId"] == DBNull.Value ? default : rdr["AdminId"]);
                            adminLloginResponse.FullName = Convert.ToString(rdr["FullName"] == DBNull.Value ? default : rdr["FullName"]);
                            adminLloginResponse.EmailId = Convert.ToString(rdr["EmailId"] == DBNull.Value ? default : rdr["EmailId"]);
                            adminLloginResponse.MobileNumber = Convert.ToString(rdr["MobileNumber"] == DBNull.Value ? default : rdr["MobileNumber"]);
                            adminLloginResponse.Address = Convert.ToString(rdr["Address"] == DBNull.Value ? default : rdr["Address"]);
                            var password = Convert.ToString(rdr["Password"] == DBNull.Value ? default : rdr["Password"]);

                            if (password == adminLogin.Password)
                            {
                                adminLloginResponse.Token = GenerateSecurityToken(adminLogin.EmailId, adminLloginResponse.AdminId);
                                return adminLloginResponse;
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                    else
                        return null;
                    con.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return default;
        }

        public string GenerateSecurityToken(string emailID, long adminId)
        {
            var SecurityKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(this.configuration["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim(ClaimTypes.Email, emailID),
                new Claim("AdminId", adminId.ToString())
            };
            var token = new JwtSecurityToken(
                this.configuration["Jwt:Issuer"],
                this.configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddHours(24),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
