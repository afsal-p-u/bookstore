using BL.DTO;
using BL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController: ControllerBase
    {
        private readonly IOrderService _service;

        public OrderController(IOrderService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _service.GetOrders();

            return Ok(orders);
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder(CreateOrderDTO orderDTO)
        {
            var result = await _service.PlaceOrder(orderDTO);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _service.GetOrderById(id);

            if (order == null)
            {
                return NotFound("Order not found");
            }

            return Ok(order);
        }
    }
}
