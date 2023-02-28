using SimpleTrader.WPF.State.Navigators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTrader.WPF.ViewModels.Factories
{
    public class RootSimpleTraderViewModelFactory : IRootSimpleTraderViewModelFactory
    {
        private ISimpleTraderViewModelFactory<HomeViewModel> homeViewModelFactory;
        private ISimpleTraderViewModelFactory<PortfolioViewModel> portfolioViewModelFactory;
        private ISimpleTraderViewModelFactory<LoginViewModel> loginViewModelFactory;
        private readonly BuyViewModel buyViewModel;

        public RootSimpleTraderViewModelFactory(ISimpleTraderViewModelFactory<HomeViewModel> homeViewModelFactory,
            ISimpleTraderViewModelFactory<PortfolioViewModel> portfolioViewModelFactory,
            BuyViewModel buyViewModel,
            ISimpleTraderViewModelFactory<LoginViewModel> loginViewModelFactory)
        {
            this.homeViewModelFactory = homeViewModelFactory;
            this.portfolioViewModelFactory = portfolioViewModelFactory;
            this.buyViewModel = buyViewModel;
            this.loginViewModelFactory = loginViewModelFactory;
        }

        public ViewModelBase CreateViewModel(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.Login:
                    return loginViewModelFactory.CreateViewModel();
                case ViewType.Home:
                    return homeViewModelFactory.CreateViewModel();                  
                case ViewType.Portfolio:
                    return portfolioViewModelFactory.CreateViewModel();
                case ViewType.Buy:
                    return buyViewModel;
                default:
                    throw new ArgumentException("The ViewType does not have a ViewModel.", "viewType");
            }
        }
    }
}
