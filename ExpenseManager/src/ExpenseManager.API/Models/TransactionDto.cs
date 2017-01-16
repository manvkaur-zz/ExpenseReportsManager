using ExpenseManager.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseManager.API.Models
{
    public class TransactionDto
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string MerchantName { get; set; }
        public int CategoryId { get; set; }
        public int CurrencyId { get; set; }
        public int ExpenseReportId { get; set; }
    }
}
