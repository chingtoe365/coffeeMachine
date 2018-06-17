/*
 * Interface for OrderDetails class
 * 
 **/

namespace CoffeeMachine.Models.Interfaces
{
    interface IOrderDetails : IOrderBase
    {
        int Id { set; get; }
        int InsertOrder();
        string[] GetAllDrinkOptions();
    }
}
