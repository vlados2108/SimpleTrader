using SimpleTrader.Domain.Exceptions;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTrader.Domain.TransactionServices
{
    public class BuyStockService : IBuyStockService
    {
        private readonly IStockPriceService stockPriceService;
        private readonly IDataService<Account> accountService;

        public BuyStockService(IStockPriceService stockPriceService, IDataService<Account> accountService)
        {
            this.stockPriceService = stockPriceService;
            this.accountService = accountService;
        }

        public async Task<Account> BuyStock(Account buyer, string symbol, int shares)
        {
            double stockPrice = await stockPriceService.GetPrice(symbol);
            double transactionPrice = stockPrice * shares;

            if (transactionPrice > buyer.Balance)
                throw new InsufficientFundsException(buyer.Balance, transactionPrice);

            AssetTransaction transaction = new AssetTransaction()
            {
                Account = buyer,
                Asset = new Asset()
                {
                    PricePerShare = stockPrice,
                    Symbol = symbol
                },
                DateProcessed = DateTime.Now,
                Shares = shares,
                IsPurchase = true                
            };
            buyer.AssetTransactions.Add(transaction);
            buyer.Balance -= transactionPrice;

            await accountService.Update(buyer.Id, buyer);
            return buyer;
        }
    }
}
