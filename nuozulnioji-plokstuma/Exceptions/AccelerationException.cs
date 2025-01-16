using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nuozulnioji_plokstuma.Exceptions
{
    /// <summary>
    /// Exception klasė naudojama kai suskaičiuotas pagreitis yra neigiamas.
    /// </summary>
    public class AccelerationException : Exception
    {
        /// <summary>
        /// Numatytasis konstruktorius be parametrų.
        /// </summary>
        public AccelerationException()
        {
        }

        /// <summary>
        /// Konstruktorius nustatantis <paramref name="message"/> vertę.
        /// </summary>
        /// <param name="message"><paramref name="message"/> vertė.</param>
        public AccelerationException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Konstruktorius nustatantis <paramref name="message"/> ir <paramref name="inner"/> vertes.
        /// </summary>
        /// <param name="message"><paramref name="message"/> vertė.</param>
        /// <param name="inner"><paramref name="inner"/> vertė.</param>
        public AccelerationException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
