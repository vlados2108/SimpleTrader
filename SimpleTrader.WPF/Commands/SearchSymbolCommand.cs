using SimpleTrader.Domain.Services;
using SimpleTrader.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SimpleTrader.WPF.Commands
{
    public class SearchSymbolCommand : ICommand
    {
        private BuyViewModel buyViewModel;
        private IStockPriceService stockPriceService;
        public event EventHandler? CanExecuteChanged;

        public SearchSymbolCommand(BuyViewModel buyViewModel, IStockPriceService stockPriceService)
        {
            this.buyViewModel = buyViewModel;
            this.stockPriceService = stockPriceService;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public async void Execute(object? parameter)
        {
            try
            {
                double stockPrice = await stockPriceService.GetPrice(buyViewModel.Symbol);
                buyViewModel.SearchResultSymbol = buyViewModel.Symbol.ToUpper();
                buyViewModel.StockPrice = stockPrice;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            } 
        }
    }
}
