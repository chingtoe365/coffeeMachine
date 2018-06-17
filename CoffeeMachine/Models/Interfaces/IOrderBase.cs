using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeMachine.Models.Interfaces
{
    interface IOrderBase
    {
        string Drink { get; set; }
        string SugarAmount { set; get; }
        int OwnMug { set; get; }
    }
}
