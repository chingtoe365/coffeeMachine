using CoffeeMachine.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeMachine.Models.Interfaces
{
    interface IOrderLogger : IOrderBase
    {
        int Id { set; get; }
        int LastOrderId { get; set; }
        void UpdateLogger();
        OrderDetails GetLastOrder();
    }
}
