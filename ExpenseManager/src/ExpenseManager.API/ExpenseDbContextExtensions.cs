using ExpenseManager.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseManager.API
{
    public static class ExpenseDbContextExtensions
    {
        public static void EnsureSeedDataForContext(this ExpenseDbContext context)
        {
            if (!context.Categories.Any())
            {
                //init seed category data
                var categories = new List<Category>()
                {
                    new Category()
                    {
                        Name = "Relocation Meals",
                    },
                    new Category()
                    {
                        Name = "Phone Bill",
                    },
                    new Category()
                    {
                        Name = "Relocation Hotel",
                    },
                    new Category()
                    {
                        Name = "Data Card",
                    }
                };
                context.Categories.AddRange(categories); //category data seeded
                context.SaveChanges();
            }//if

            if (!context.Currencies.Any())
            {
                var currencies = new List<Currency>()
                {
                    new Currency()
                    {
                        Acronym = "INR",
                        Name = "Indian Rupee",
                    },
                    new Currency()
                    {
                        Acronym = "USD",
                        Name = "US Dollar",
                    },
                    new Currency()
                    {
                        Acronym = "CAD",
                        Name = "Canadian Dollar",
                    },
                    new Currency()
                    {
                        Acronym = "AUD",
                        Name = "Australian Dollar",
                    },
                };
                context.Currencies.AddRange(currencies); //seed currency data
                context.SaveChanges();
            }//if
            
        }
    }
}
