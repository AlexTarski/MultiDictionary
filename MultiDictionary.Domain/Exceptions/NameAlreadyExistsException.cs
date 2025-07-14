using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiDictionary.Domain.Exceptions
{
    public class NameAlreadyExistsException : Exception
    {
        public NameAlreadyExistsException() { }
        public NameAlreadyExistsException(string message) : base(message) { }
        public NameAlreadyExistsException(string message, Exception inner) : base(message, inner) { }
    }
}
