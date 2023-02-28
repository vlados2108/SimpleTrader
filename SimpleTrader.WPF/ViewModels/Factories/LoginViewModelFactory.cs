using SimpleTrader.WPF.State.Authenticators;
using SimpleTrader.WPF.State.Navigators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTrader.WPF.ViewModels.Factories
{
    public class LoginViewModelFactory : ISimpleTraderViewModelFactory<LoginViewModel>
    {
        private readonly IAuthenticator authenticator;
        private readonly INavigator navigator;

        public LoginViewModelFactory(IAuthenticator authenticator, INavigator navigator)
        {
            this.authenticator = authenticator;
            this.navigator = navigator;
        }

        public LoginViewModel CreateViewModel()
        {
            return new LoginViewModel(authenticator,navigator);
        }
    }
}
