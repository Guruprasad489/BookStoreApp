using CommonLayer.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using RepositoryLayer.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace RepositoryLayer.Services
{
    /// <summary>
    /// User repository layer class for User CRUD operations
    /// </summary>
    /// <seealso cref="RepositoryLayer.Interface.IUserRL" />
    public class UserRL : IUserRL
    {
        private readonly IConfiguration configuration;
        private static string Key = "36c53aa7571c33d2f98d02a4313c4ba1ea15e45c18794eb564b21c19591805g";

        public UserRL(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        /// <summary>
        /// Registers the specified user.
        /// </summary>
        /// <param name="userReg">The user reg.</param>
        /// <returns></returns>
        public UserReg Register(UserReg userReg)
        {
            using (SqlConnection con = new SqlConnection(configuration["ConnectionString:BookStore"]))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("spUserRegistration", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    var encryptPassword = EncryptPassword(userReg.Password);

                    cmd.Parameters.AddWithValue("@FullName", userReg.FullName);
                    cmd.Parameters.AddWithValue("@EmailId", userReg.EmailId);
                    cmd.Parameters.AddWithValue("@Password", encryptPassword);
                    cmd.Parameters.AddWithValue("@MobileNumber", userReg.MobileNumber);

                    con.Open();
                    var result = cmd.ExecuteNonQuery();
                    con.Close();

                    if (result != 0)
                    {
                        return userReg;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }

        }

        /// <summary>
        /// Logins in the specified user.
        /// </summary>
        /// <param name="userLogin">The user login.</param>
        /// <returns></returns>
        public LoginResponse UserLogin(UserLogin userLogin)
        {
            LoginResponse loginResponse = new LoginResponse();
            using (SqlConnection con = new SqlConnection(configuration["ConnectionString:BookStore"]))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("spUserLogin", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@EmailId", userLogin.EmailId);

                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            loginResponse.UserId = Convert.ToInt32(rdr["UserId"] == DBNull.Value ? default : rdr["UserId"]);
                            var password = Convert.ToString(rdr["Password"] == DBNull.Value ? default : rdr["Password"]);

                            loginResponse.FullName = Convert.ToString(rdr["FullName"] == DBNull.Value ? default : rdr["FullName"]);
                            loginResponse.EmailId = Convert.ToString(rdr["EmailId"] == DBNull.Value ? default : rdr["EmailId"]);
                            loginResponse.MobileNumber = Convert.ToInt64(rdr["MobileNumber"] == DBNull.Value ? default : rdr["MobileNumber"]);

                            var decryptPassword = DecryptPassword(password);
                            if (decryptPassword == userLogin.Password)
                            {
                                loginResponse.Token = GenerateSecurityToken(userLogin.EmailId, loginResponse.UserId);
                                return loginResponse;
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

        /// <summary>
        /// Generates the security token.
        /// Method to generate Security key for user (Generating Json Web token) 
        /// </summary>
        /// <param name="emailID">The email identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public string GenerateSecurityToken(string emailID, long userId)
        {
            var SecurityKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(this.configuration["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Role, "User"),
                new Claim(ClaimTypes.Email, emailID),
                new Claim("UserId", userId.ToString())
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

        /// <summary>
        /// Method to get password reset link.
        /// </summary>
        /// <param name="emailID">The email identifier.</param>
        /// <returns></returns>
        public string ForgotPassword(ForgotPassword forgotPassword)
        {
            using (SqlConnection con = new SqlConnection(configuration["ConnectionString:BookStore"]))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("spUserForgotPassword", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@EmailId", forgotPassword.EmailId);

                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            var userId = Convert.ToInt64(rdr["UserId"] == DBNull.Value ? default : rdr["UserId"]);

                            string token = GenerateSecurityToken(forgotPassword.EmailId, userId);
                            new Msmq().SendMessage(token);
                            return "Reset password sent successfully";
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

        /// <summary>
        /// Reset Password for Authenticated emailID after authorization 
        /// </summary>
        /// <param name="resetPassword">The reset password.</param>
        /// <param name="emailID">The email identifier.</param>
        /// <returns></returns>
        public string ResetPassword(ResetPassword resetPassword, string emailId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(configuration["ConnectionString:BookStore"]))
                {
                    if (resetPassword.Password == resetPassword.ConfirmPassword)
                    {
                        var encryptPassword = EncryptPassword(resetPassword.Password);

                        SqlCommand cmd = new SqlCommand("spUserResetPassword", con);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@EmailId", emailId);
                        cmd.Parameters.AddWithValue("@Password", EncryptPassword(resetPassword.Password));
                        con.Open();
                        var result = cmd.ExecuteNonQuery();
                        con.Close();
                        if (result > 0)
                        {
                            return "Congratulations! Your password has been changed successfully";
                        }
                        else
                            return "Failed to reset your password";
                    }
                    else
                        return "Make Sure your Passwords Match";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Encrypts the password.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public static string EncryptPassword(string password)
        {
            try
            {
                if (string.IsNullOrEmpty(password))
                    return null;
                else
                {
                    password += Key;
                    var passwordBytes = Encoding.UTF8.GetBytes(password);
                    return Convert.ToBase64String(passwordBytes);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Decrypts the password.
        /// </summary>
        /// <param name="encodedPassword">The encoded password.</param>
        /// <returns></returns>
        public static string DecryptPassword(string encodedPassword)
        {
            try
            {
                if (string.IsNullOrEmpty(encodedPassword))
                    return null;
                else
                {
                    var encodedBytes = Convert.FromBase64String(encodedPassword);
                    var res = Encoding.UTF8.GetString(encodedBytes);
                    var resPass = res.Substring(0, res.Length - Key.Length);
                    return resPass;
                    //return res;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
