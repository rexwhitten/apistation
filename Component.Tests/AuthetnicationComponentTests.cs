using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using apistation.Components;

namespace Component.Tests
{
    [TestClass]
    public class AuthetnicationComponentTests
    {

        public IAuthenticationComponent AuthenticationComponent
        {
            get
            {
                // Create your Implementation of IAuthentication Here
                return null;
            }
        }



        [TestMethod]
        public void Test_ValidateauthenticationComponent()
        {
            IAuthenticationComponent auth = this.AuthenticationComponent;

            string good_username = "";
            string good_email = "";
            string good_password = "";
            string bad_username = "";
            string bad_password = "";

            AuthenticationResult good_login = auth.Login(good_username, good_password);
            AuthenticationResult bad_login = auth.Login(bad_username, bad_password);

            AuthenticationResult good_roles = auth.GetRoles(good_username);
            AuthenticationResult bad_roles = auth.GetRoles(bad_username);

            AuthenticationResult register = auth.Register(good_username, good_email, good_password);

        }
    }
}
