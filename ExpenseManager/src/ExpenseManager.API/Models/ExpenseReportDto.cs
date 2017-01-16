using ExpenseManager.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseManager.API.Models
{
    public class ExpenseReportDto
    {
        public int Id { get; set; }
        public string Purpose { get; set; }
        public DateTime SubmitDate { get; set; }
        public int numberOfTransactions { get
            {
                return Transactions.Count;
            }
        }
        public ICollection<TransactionDto> Transactions { get; set; } = new List<TransactionDto>();

    }
}
