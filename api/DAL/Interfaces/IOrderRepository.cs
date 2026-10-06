using BookStore.Model;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IOrderRepository
    {
        Task AddOrder(Order order);
        Task<Book> GetBookById(int id);
        Task<List<Order>> GetOrders();
        Task<Order> GetOrderById(int id);
    }
}
