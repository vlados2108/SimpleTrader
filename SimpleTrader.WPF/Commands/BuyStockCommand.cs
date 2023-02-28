using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.TransactionServices;
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
    public class BuyStockCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public BuyViewModel buyViewModel;
        public IBuyStockService buyStockService;

        public BuyStockCommand(BuyViewModel buyViewModel, IBuyStockService buyStockService)
        {
            this.buyViewModel = buyViewModel;
            this.buyStockService = buyStockService;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public async void Execute(object? parameter)
        {
            try
            {
                Account account = await buyStockService.BuyStock(new Domain.Models.Account()
                {
                    Id = 5,
                    Balance = 500,
                    AssetTransactions = new List<AssetTransaction>()
                }, buyViewModel.Symbol, buyViewModel.SharesToBuy);
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message); 
            }
        }
    }
}
