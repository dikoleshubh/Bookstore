using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interfaces
{
    public interface IAdminRL
    {

        public Admin RegisterAdminDetails(RegisterAdmin registration);
        // public string LoginAdmin(string AdminEmail, string AdminPassword);
        public string AdminLogin(LoginAdmin login);
    }
}
