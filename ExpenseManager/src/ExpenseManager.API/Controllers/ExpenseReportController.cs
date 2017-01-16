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
    [Route("api/[controller]")]
    public class ExpenseReportController:Controller
    {
        private IExpenseRepository _repository;
        public ExpenseReportController(IExpenseRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetReports()
        {
            var reports = _repository.GetExpenseReports();
            var results = Mapper.Map<IEnumerable<ExpenseReportWithoutTransactionDto>>(reports);

            return Ok(results);
        }
        [HttpGet("{id}", Name = "GetExpenseReport")]
        public IActionResult GetReport(int id, bool includeTransactions = true)
        {
            var report = _repository.GetExpenseReport(id, includeTransactions);

            if (report == null)
            {
                return NotFound();
            }

            if (includeTransactions)
            {
                var reportResult = Mapper.Map<ExpenseReportDto>(report);
                return Ok(reportResult);
            }

            var reportsWithoutTransactionsResult = Mapper.Map<ExpenseReportWithoutTransactionDto>(report);
            return Ok(reportsWithoutTransactionsResult);
        }

        [HttpPost]
        public IActionResult CreateExpenseReport([FromBody] ExpenseReportForCreationDto expenseReport)
        {
            if (expenseReport == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var report = Mapper.Map<Entities.ExpenseReport>(expenseReport);

            _repository.AddExpenseReport(report);

            if (!_repository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            var createdReportToReturn = Mapper.Map<Models.ExpenseReportDto>(report);

            return CreatedAtRoute("GetExpenseReport", new
            { id = createdReportToReturn.Id }, createdReportToReturn);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateExpenseReport(int id,
            [FromBody] ExpenseReportForCreationDto expenseReport)
        {
            if (expenseReport == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reportEntity = _repository.GetExpenseReport(id,false);
            if (reportEntity == null)
            {
                return NotFound();
            }

            Mapper.Map(expenseReport, reportEntity);

            if (!_repository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return NoContent();
        }
    }
}
