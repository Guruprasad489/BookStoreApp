using CommonLayer.Models;

namespace RepositoryLayer.Interfaces
{
    /// <summary>
    /// User Interface
    /// </summary>
    public interface IUserRL
    {
        UserReg Register(UserReg userReg);
        LoginResponse UserLogin(UserLogin userLogin);
        string ForgotPassword(ForgotPassword forgotPassword);
        string ResetPassword(ResetPassword resetPassword, string emailId);
    }
}
