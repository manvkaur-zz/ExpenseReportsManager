using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseManager.API.Entities
{
    public class Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage ="Transaction amount can't be empty.")]
        public int Amount { get; set; }

        [Required(ErrorMessage ="Transaction date is a required field.")]
        public DateTime TransactionDate { get; set; }

        [Required(ErrorMessage = "Transaction date is a required field.")]
        [MaxLength(50)]
        public string MerchantName { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
        public int CategoryId { get; set; }

        [ForeignKey("CurrencyId")]
        public Currency Currency { get; set; }
        public int CurrencyId { get; set; }

        [ForeignKey("ExpenseReportId")]
        public ExpenseReport ExpenseReport { get; set; }
        public int ExpenseReportId { get; set; }

    }
}
