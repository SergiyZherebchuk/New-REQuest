using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewRequest.SPA.Code
{
    public interface ISecurityService
    {
        bool Login(string login, string password);
        bool Logout(string login);
        void RefreshPrincipal();
    }
}
