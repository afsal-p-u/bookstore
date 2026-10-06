using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTO
{
    public class CreateOrderDTO
    {
        public int UserId { get; set; }
        public string Address { get; set; }
        public List<OrderItemDTO> OrderItems { get; set; }
    }
}
