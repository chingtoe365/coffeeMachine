/*
 * Base interface 
 * 
 **/
namespace CoffeeMachine.Models.Interfaces
{
    interface IOrderBase
    {
        string Drink { get; set; }
        string SugarAmount { set; get; }
        int OwnMug { set; get; }
    }
}
