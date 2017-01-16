using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseManager.API.Models
{
    public class ExpenseReportForCreationDto
    {
        public string Purpose { get; set; }
        public DateTime SubmitDate { get; set; }
    }
}
