using AutoMapper;
using ExpenseManager.API.Entities;
using ExpenseManager.API.Models;
using ExpenseManager.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseManager.API.Controllers
{
    [Route("api/expensereport")]
    public class TransactionController:Controller
    {
        private IExpenseRepository _repository;
        public TransactionController(IExpenseRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{ExpenseReportId}/transaction")]
        public IActionResult GetTransactions(int ExpenseReportId)
        {
            try
            {
                if (!_repository.ExpenseReportExists(ExpenseReportId))
                {
                    return NotFound();
                }

                var transactions = _repository.GetTransactionsForReport(ExpenseReportId);
                var transactionsResults =
                                   Mapper.Map<IEnumerable<TransactionDto>>(transactions);

                return Ok(transactionsResults);
            }
            catch (Exception ex)
            {
               return StatusCode(500, "A problem happened while handling your request.");
            }
        }

        [HttpGet("{ExpenseReportId}/transaction/{id}", Name = "GetTransaction")]
        public IActionResult GetTransaction(int ExpenseReportId, int id)
        {
            if (!_repository.ExpenseReportExists(ExpenseReportId))
            {
                return NotFound();
            }

            var transaction = _repository.GetTransactionForReport(ExpenseReportId, id);

            if (transaction == null)
            {
                return NotFound();
            }

            var transactionResult = Mapper.Map<TransactionDto>(transaction);
            return Ok(transactionResult);
        }

        [HttpPost("{ExpenseReportId}/transaction")]
        public IActionResult CreateTransaction(int ExpenseReportId,
            [FromBody] TransactionForCreationDto transaction)
        {
            if (transaction == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_repository.ExpenseReportExists(ExpenseReportId))
            {
                return NotFound();
            }

            var finalTransaction = Mapper.Map<Entities.Transaction>(transaction);

            _repository.AddTransactionForReport(ExpenseReportId, finalTransaction);

            if (!_repository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            var createdTransactionToReturn = Mapper.Map<Models.TransactionDto>(finalTransaction);

            return CreatedAtRoute("GetTransaction", new
            { ExpenseReportId = ExpenseReportId, id = createdTransactionToReturn.Id }, createdTransactionToReturn);
        }

        [HttpPut("{ExpenseReportId}/transaction/{id}")]
        public IActionResult UpdateTransaction(int ExpenseReportId, int id,
            [FromBody] TransactionForCreationDto transaction)
        {
            if (transaction == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_repository.ExpenseReportExists(ExpenseReportId))
            {
                return NotFound();
            }

            var transactionEntity = _repository.GetTransactionForReport(ExpenseReportId, id);
            if (transactionEntity == null)
            {
                return NotFound();
            }

            Mapper.Map(transaction, transactionEntity);

            if (!_repository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return NoContent();
        }

        [HttpDelete("{ExpenseReportId}/transaction/{id}")]
        public IActionResult DeleteTransaction(int ExpenseReportId, int id)
        {
            if (!_repository.ExpenseReportExists(ExpenseReportId))
            {
                return NotFound();
            }

            var transactionEntity = _repository.GetTransactionForReport(ExpenseReportId, id);
            if (transactionEntity == null)
            {
                return NotFound();
            }

            _repository.DeleteTransaction(transactionEntity);

            if (!_repository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return NoContent();
        }
    }
}
