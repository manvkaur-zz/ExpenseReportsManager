using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseManager.API.Entities
{
    public class Currency
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage ="Currency Acronym can't be empty")]
        [MaxLength(5)]
        public string Acronym { get; set; }

        [Required(ErrorMessage ="Currency Name can't be empty")]
        [MaxLength(25)]
        public string Name { get; set; }
    }
}
