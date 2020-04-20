using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace CurrencyTradingEngine.Money.Domain.Exception
{
    public class UserNotFoundException : System.Exception
    {
        public UserNotFoundException()
        {
        }

        public UserNotFoundException(string message) : base(message)
        {
        }

        public UserNotFoundException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        protected UserNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
