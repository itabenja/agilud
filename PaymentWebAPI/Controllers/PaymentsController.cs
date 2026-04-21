using Microsoft.AspNetCore.Mvc;
using PaymentWebAPI.Models;
using PaymentWebAPI.Repositories;

namespace PaymentWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentRepository _repo;

        public PaymentsController(IPaymentRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_repo.GetAll());

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var payment = _repo.GetById(id);
            if (payment == null) return NotFound();
            return Ok(payment);
        }

        [HttpPost]
        public IActionResult Create(Payment payment)
        {
            var created = _repo.Create(payment);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpGet("{id}/status")]
        public IActionResult CheckStatus(int id)
        {
            bool success = _repo.IsPaymentSuccessful(id);
            return Ok(new { paymentId = id, success });
        }
    }
}
