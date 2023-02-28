using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services.AuthenticationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTrader.WPF.State.Authenticators
{
    public interface IAuthenticator
    {
        Account currentAccount { get; }
        bool isLoggedIn { get; }

        Task<RegistrationResult> Register(string email, string username, string password, string confirmedPassword);
        Task<bool> Login(string username, string password);
        void Logout();

    }
}
