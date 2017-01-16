using ExpenseManager.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseManager.API.Services
{
    public interface IExpenseRepository
    {
        bool ExpenseReportExists(int ExpenseReportId);
        IEnumerable<ExpenseReport> GetExpenseReports();
        ExpenseReport GetExpenseReport(int ExpenseReportId, bool includeTransactions);
        IEnumerable<Transaction> GetTransactionsForReport(int ExpenseReportId);
        Transaction GetTransactionForReport(int ExpenseReportId, int TransactionId);
        void AddTransactionForReport(int ExpenseReportId, Transaction transaction);
        void AddExpenseReport(ExpenseReport expenseReport);
        void DeleteTransaction(Transaction transaction);
        bool Save();
    }
}
