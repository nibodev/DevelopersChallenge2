using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using API.Controllers.Responses;
using API.Domain;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/tranaction")]
    public class TransactionController : ControllerBase
    {
        private ITransactionsRepository _repository;

        public TransactionController(ITransactionsRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TransactionResponse>),(int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [Route("{year}/{month}")]
        public async Task<IActionResult> MonthlyTransactions(int year, int month)
        {
            var transactions = await _repository.MonthlyBalance(year, month);
            var response = transactions.Select(x => x.MapToResponse());

            return response.Any() ? Ok(response) : NoContent() as IActionResult; 
        }

        [HttpPost]
        [ProducesResponseType(typeof(TransactionResponse),(int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        [Route("{id}/reconcile")]
        public async Task<IActionResult> Reconcile(Guid id)
        {
            var transaction = await _repository.Get(id);

            if (transaction == null) return NotFound();

            if (transaction.Reconciled) return Conflict(transaction.MapToResponse());

            transaction.Reconcile();

            await _repository.Update();

            return Ok(transaction.MapToResponse());
        }

        [HttpPost]
        [ProducesResponseType(typeof(TransactionResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        [Route("{id}/unreconcile")]
        public async Task<IActionResult> UnReconcile(Guid id)
        {
            var transaction = await _repository.Get(id);

            if (transaction == null) return NotFound();

            if (!transaction.Reconciled) return Conflict(transaction.MapToResponse());

            transaction.UnReconcile();

            await _repository.Update();

            return Ok(transaction.MapToResponse());
        }
    }
}