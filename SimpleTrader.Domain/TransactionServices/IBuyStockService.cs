using SimpleTrader.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTrader.Domain.TransactionServices
{
    public interface IBuyStockService
    {
        Task<Account> BuyStock(Account byer, string stock, int shares);
    }
}
