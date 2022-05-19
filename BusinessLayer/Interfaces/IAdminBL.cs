using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IAdminBL
    {
        AdminLoginResponse AdminLogin(AdminLogin adminLogin);
    }
}
