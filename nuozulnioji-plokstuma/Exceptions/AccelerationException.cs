using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nuozulnioji_plokstuma.Exceptions
{
    public class AccelerationException : Exception
    {
        public int ErrorCode { get; }

        public AccelerationException()
        {
        }

        public AccelerationException(string message)
            : base(message)
        {
        }

        public AccelerationException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public AccelerationException(string message, int errorCode)
            : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
