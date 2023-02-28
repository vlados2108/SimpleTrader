using SimpleTrader.WPF.State.Authenticators;
using SimpleTrader.WPF.State.Navigators;
using SimpleTrader.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SimpleTrader.WPF.Commands
{
    public class LoginCommand : ICommand
    {
        private readonly LoginViewModel loginViewModel;
        private readonly IAuthenticator authenticator;
        private readonly INavigator navigator;


        public LoginCommand(LoginViewModel loginViewModel, IAuthenticator authenticator, INavigator navigator)
        {
            this.authenticator = authenticator;
            this.loginViewModel = loginViewModel;
            this.navigator = navigator;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public async void Execute(object? parameter)
        {
            bool success = await authenticator.Login(loginViewModel.Username,parameter.ToString());

            if (success)
            {
                //navigator.UpdateCurrentViewModelCommand.Execute(ViewType.Home);  
            }
        }
    }
}
