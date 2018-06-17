using CoffeeMachine.Models.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeMachine.Models.Interfaces
{
    interface IOrderDetails : IOrderBase
    {
        int Id { set; get; }
        int InsertOrder();
        string[] GetAllDrinkOptions();
    }
}
