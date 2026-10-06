using BL.DTO;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface IOrderService
    {
        Task<string> PlaceOrder(CreateOrderDTO _createOrder);
        Task<List<Order>> GetOrders();
        Task<Order> GetOrderById(int id);
    }
}
