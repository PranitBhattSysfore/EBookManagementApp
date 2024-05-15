using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionsHandling.Exceptions
{
    public class NoBooksWithThisTitleException : Exception
    {

        public NoBooksWithThisTitleException(string message) : base(message)
        {

        }
    }
}
