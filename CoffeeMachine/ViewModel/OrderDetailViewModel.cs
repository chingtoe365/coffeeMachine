using System;
using System.ComponentModel.DataAnnotations;

namespace CoffeeMachine.ViewModel
{
    public class OrderDetailViewModel
    {
        public OrderDetailViewModel()
        {
            this.Drink = Drink;
            this.SugarAmount = SugarAmount;
            this.OwnMug = OwnMug;
        }
        [Required(ErrorMessage = "*")]
        public String Drink { get; set; }
        [Required(ErrorMessage = "*")]
        public String SugarAmount { get; set; }
        [Required(ErrorMessage = "*")]
        public Int16 OwnMug { get; set; }
    }
}