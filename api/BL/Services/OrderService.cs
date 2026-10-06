using BL.DTO;
using BL.Interfaces;
using DAL.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class OrderService: IOrderService
    {
        private readonly IOrderRepository _repository;

        public OrderService(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<string> PlaceOrder(CreateOrderDTO _createOrder)
        {
            decimal total = 0;

            var order = new Order
            {
                UserId = _createOrder.UserId,
                Address = _createOrder.Address,
                DeliveryStatus = "Pending",
                OrderDate = DateTime.Now,
                OrderItems = new List<OrderItem>()
            };

            foreach (var item in _createOrder.OrderItems)
            {
                var book = await _repository.GetBookById(item.BookId);

                if (book == null) return "Book not found";

                if (book.Stock < item.Quantity) return $"Insufficient stock for {book.Title}";

                book.Stock -= item.Quantity;

                decimal itemTotal = book.Price * item.Quantity;

                total += itemTotal;

                order.OrderItems.Add(new OrderItem
                {
                    BookId = item.BookId,
                    Quantity = item.Quantity,
                    Price = book.Price
                });
            }

            order.TotalAmount = total;
            await _repository.AddOrder(order);

            return "Order placed successfully";
        }

        public async Task<List<Order>> GetOrders()
        {
            return await _repository.GetOrders();
        }

        public async Task<Order> GetOrderById(int id)
        {
            return await _repository.GetOrderById(id);
        }
    }
}
