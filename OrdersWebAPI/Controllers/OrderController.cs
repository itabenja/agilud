using Microsoft.AspNetCore.Mvc;
using OrdersWebAPI.Models;
using OrdersWebAPI.Repositories;
using System.Text.Json;

namespace OrdersWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _repo;
        private readonly HttpClient _httpClient;

        public OrdersController(IOrderRepository repo, IHttpClientFactory httpClientFactory)
        {
            _repo = repo;
            _httpClient = httpClientFactory.CreateClient();
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_repo.GetAll());

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var order = _repo.GetById(id);
            if (order == null) return NotFound();
            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Order order)
        {
            // 1️⃣ Check if user exists
            var userResponse = await _httpClient.GetAsync($"http://localhost:5064/api/users/{order.UserId}");
            if (!userResponse.IsSuccessStatusCode)
                return BadRequest("User does not exist.");

            // 2️⃣ Check if payment succeeded
            var paymentResponse = await _httpClient.GetAsync($"http://localhost:5074/api/payments/{order.PaymentId}/status");
            if (!paymentResponse.IsSuccessStatusCode)
                return BadRequest("Payment not found.");

            var paymentStatusJson = await paymentResponse.Content.ReadAsStringAsync();
            var paymentStatus = JsonSerializer.Deserialize<PaymentStatusDto>(paymentStatusJson,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (paymentStatus == null || !paymentStatus.Success)
                return BadRequest("Payment not successful.");

            // 3️⃣ Create order
            var created = _repo.Create(order);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }



        private class PaymentStatusDto
        {
            public int PaymentId { get; set; }
            public bool Success { get; set; }
        }
    }
}
