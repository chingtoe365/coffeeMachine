/*
 * This class handles the exception error
 * caused by incorrect sugar amount input.
 * 
 * Only integer is acceptable
 * 
 **/

using System;

namespace CoffeeMachine.Models.Exceptions
{
    public class SugarAmountTypeErrorException : Exception
    {
        private string _errorMessage = "Not valid SugarAmount type, integer required";
        public override string Message
        {
            get
            {
                return _errorMessage;
            }
        }
        public SugarAmountTypeErrorException() { }
        public SugarAmountTypeErrorException(string message) : base(message) { }
        public SugarAmountTypeErrorException(string message, Exception inner) : base(message, inner) { }
    }
}