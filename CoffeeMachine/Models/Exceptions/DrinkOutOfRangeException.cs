using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoffeeMachine.Models.Exceptions
{
    public class DrinkOutOfRangeException : Exception
    {
        private string _errorMessage = "Not valid Drink type (coffer/tea/chocolate)";
        public override string Message
        {
            get
            {
                return _errorMessage;
            }
        }

        public DrinkOutOfRangeException()
        {
        }
        
        public DrinkOutOfRangeException(string message) : base(message)
        {
        }

        public DrinkOutOfRangeException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}