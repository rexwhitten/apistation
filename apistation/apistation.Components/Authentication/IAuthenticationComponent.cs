using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apistation.Components
{
    public interface IAuthenticationComponent : IApiComponent
    {
        bool IsAuthenticated(Nancy.Request Request);

        AuthenticationResult Login(string good_username, string good_password);

        AuthenticationResult GetRoles(string good_username);

        AuthenticationResult Register(string good_username, string good_email, string good_password);
    }
}
