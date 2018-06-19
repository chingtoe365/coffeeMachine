/*
 * This class handles the exception error
 * caused by incorrect drink type input.
 * 
 * Only three drink options available (case sensitive)
 * - coffee - chocolate - tea
 * 
 **/

using System;

namespace CoffeeMachine.Models.Exceptions
{
    public class DrinkOutOfRangeException : Exception
    {
        private string _errorMessage = "Not valid Drink type (coffee/tea/chocolate)";
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