using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpenseManager.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExpenseManager.API.Services
{
    public class ExpenseRepository : IExpenseRepository
    {
        private ExpenseDbContext _db;
        public ExpenseRepository(ExpenseDbContext db)
        {
            _db = db;
        }
        public IEnumerable<ExpenseReport> GetExpenseReports()
        {
            return _db.ExpenseReports.ToList();
        }
        public ExpenseReport GetExpenseReport(int ExpenseReportId, bool includeTransactions=false)
        {
            if (includeTransactions)
            {
                return _db.ExpenseReports.Include(t => t.Transactions).
                    Where(r => r.Id == ExpenseReportId).FirstOrDefault();
            }
            return _db.ExpenseReports.Where(r => r.Id == ExpenseReportId).FirstOrDefault();
        }      

        public Transaction GetTransactionForReport(int ExpenseReportId, int TransactionId)
        {
            return _db.Transactions.Where(t => t.ExpenseReportId == ExpenseReportId
            && t.Id == TransactionId).FirstOrDefault();
        }

        public IEnumerable<Transaction> GetTransactionsForReport(int ExpenseReportId)
        {
            return _db.Transactions.Where(t => t.ExpenseReportId == ExpenseReportId).ToList();
        }

        public bool ExpenseReportExists(int ExpenseReportId)
        {
            return _db.ExpenseReports.Any(r => r.Id == ExpenseReportId);
        }

        public void AddTransactionForReport(int ExpenseReportId, Transaction transaction)
        {
            var report = GetExpenseReport(ExpenseReportId, false);
            report.Transactions.Add(transaction);
        }

        public bool Save()
        {
            return (_db.SaveChanges() >= 0);
        }

        public void AddExpenseReport(ExpenseReport expenseReport)
        {
            _db.ExpenseReports.Add(expenseReport);
        }

        public void DeleteTransaction(Transaction transaction)
        {
            _db.Transactions.Remove(transaction);
        }
    }
}
