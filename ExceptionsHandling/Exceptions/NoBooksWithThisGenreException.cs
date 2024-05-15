using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionsHandling.Exceptions
{
    public class NoBooksWithThisGenreException : Exception
    {

        public NoBooksWithThisGenreException(string message) : base(message)
        {

        }

    }
}
