using CommonLayer.Models;


namespace BusinessLayer.Interfaces
{
    /// <summary>
    /// Interface For User Business Layer Class
    /// </summary>
    public interface IUserBL
    {
        UserReg Register(UserReg userReg);
        LoginResponse UserLogin(UserLogin userLogin);
        string ForgotPassword(ForgotPassword forgotPassword);
        string ResetPassword(ResetPassword resetPassword, string emailId);
    }
}
