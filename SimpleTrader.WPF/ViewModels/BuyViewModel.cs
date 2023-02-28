using SimpleTrader.Domain.Services;
using SimpleTrader.Domain.TransactionServices;
using SimpleTrader.FinancialModelingPrepAPI.Services;
using SimpleTrader.WPF.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SimpleTrader.WPF.ViewModels
{
    public class BuyViewModel:ViewModelBase
    {
        private string simbol;
        public string Symbol
        {
            get
            {
                return simbol;
            }
            set
            {
                simbol = value;
                OnPropertyChanged(nameof(Symbol));
            }
        }

        private string searchResultSymbol = String.Empty;
        public string SearchResultSymbol
        {
            get
            {
                return searchResultSymbol;
            }
            set
            {
                searchResultSymbol = value;
                OnPropertyChanged(nameof(SearchResultSymbol));
                
            }
        }

        private double stockPrice;
        public double StockPrice
        {
            get
            {
                return stockPrice;
            }
            set
            {
                stockPrice = value;
                OnPropertyChanged(nameof(StockPrice));
                OnPropertyChanged(nameof(TotalPrice));
            }
        }

        private int sharesToBuy;
        public int SharesToBuy
        {
            get
            {
                return sharesToBuy;
            }
            set
            {
                sharesToBuy = value;
                OnPropertyChanged(nameof(SharesToBuy));
                OnPropertyChanged(nameof(TotalPrice));
            }
        }

        public double TotalPrice
        {
            get
            {
                return SharesToBuy * StockPrice;
            }
        }

        public ICommand SearchSymbolCommand { get; set; }
        public ICommand BuyStockCommand { get; set; }

        public BuyViewModel(IStockPriceService stockPriceService,IBuyStockService buyStockService)
        {
            SearchSymbolCommand = new SearchSymbolCommand(this, stockPriceService);
            BuyStockCommand = new BuyStockCommand(this, buyStockService);
        }

       
    }
}
