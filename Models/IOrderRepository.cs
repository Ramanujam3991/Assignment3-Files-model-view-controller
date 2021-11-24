using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandyShop.Models
{
    public interface IOrderRepository
    {
        void CreateOrder(Order order);


        public IEnumerable<Order> GetAllOrders { get; }
        public Order GetOrderById(String name);

        public void DeleteOrder(int id);
        public void SaveOrder(Order order);
    }
}
