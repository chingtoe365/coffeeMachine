/*
 * Interface for OrderLogger class
 * 
 **/

using CoffeeMachine.Models.Services;

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
