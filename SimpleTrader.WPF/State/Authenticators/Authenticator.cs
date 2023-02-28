using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services.AuthenticationServices;
using SimpleTrader.WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTrader.WPF.State.Authenticators
{
    public class Authenticator :ObservableObject ,IAuthenticator
    {
        private readonly IAuthenticationService authenticationService;

        public Authenticator(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }


        private Account CurrentAccount;
        public Account currentAccount
        {
            get
            {
                return CurrentAccount;
            }
            private set
            {
                CurrentAccount = value;
                OnPropertyChanged(nameof(currentAccount));
                OnPropertyChanged(nameof(isLoggedIn));

            }
        }
        
        public bool isLoggedIn => currentAccount != null ;

        public async Task<bool> Login(string username, string password)
        {
            bool success = false;
            try
            {
                currentAccount = await authenticationService.Login(username, password);
                success = true;
            }
            catch (Exception)
            {
                success = false;   
            }
            return success;
        }

        public void Logout()
        {
            currentAccount = null;
        }

        public async Task<RegistrationResult> Register(string email, string username, string password, string confirmedPassword)
        {
            return await authenticationService.Register(email,username,password,confirmedPassword);
        }
    }
}
