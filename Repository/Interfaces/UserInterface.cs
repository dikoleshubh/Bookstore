using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.NewFolder
{

    public interface IUserRL
    {
        public User RegisterDetails(RegisterUser registration);
        public string Login(LoginUser login);
        public MSMQModel ForgetPassword(ForgetPasswordModel forgetPasswordModel);
        bool ResetCustomerAccountPassword(ResetPasswordModel resetPasswordModel);
        public string CreateToken(User info);

        public bool ForgotPassword(string Email);



    }
}
