using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoffeeMachine.Models.Exceptions
{
    public class OwnMugOutOfRangeException : Exception
    {
        private string _errorMessage = "Not valid OwnMug input, only 0 (no mug) or 1 (have mug) is allowed";
        public override string Message
        {
            get
            {
                return _errorMessage;
            }
        }
        public OwnMugOutOfRangeException()
        {
        }

        public OwnMugOutOfRangeException(string message) : base(message)
        {
        }

        public OwnMugOutOfRangeException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}