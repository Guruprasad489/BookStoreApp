using BusinessLayer.Interfaces;
using CommonLayer.Models;
using RepositoryLayer.Interfaces;
using System;


namespace BusinessLayer.Services
{
    /// <summary>
    ///  User Business Layer Class To Implement methods of IUserBL
    /// </summary>
    /// <seealso cref="BusinessLayer.Interface.IUserBL" />
    public class UserBL : IUserBL
    {
        private readonly IUserRL userRL;
        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }

        public UserReg Register(UserReg userReg)
        {
            try
            {
                return userRL.Register(userReg);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public LoginResponse UserLogin(UserLogin userLogin)
        {
            try
            {
                return userRL.UserLogin(userLogin);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ForgotPassword(ForgotPassword forgotPassword)
        {
            try
            {
                return userRL.ForgotPassword(forgotPassword);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public string ResetPassword(ResetPassword resetPassword, string emailId)
        {
            try
            {
                return userRL.ResetPassword(resetPassword, emailId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
