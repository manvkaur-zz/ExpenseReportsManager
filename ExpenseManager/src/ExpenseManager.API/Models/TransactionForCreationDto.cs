using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseManager.API.Models
{
    public class TransactionForCreationDto
    {
        public int Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string MerchantName { get; set; }
        public int CurrencyId { get; set; }
        public int CategoryId { get; set; }
    }
}
