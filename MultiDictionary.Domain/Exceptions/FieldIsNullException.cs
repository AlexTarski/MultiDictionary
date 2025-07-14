using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiDictionary.Domain.Exceptions
{

	public class FieldIsNullException : Exception
	{
		public FieldIsNullException() { }
		public FieldIsNullException(string message) : base(message) { }
		public FieldIsNullException(string message, Exception inner) : base(message, inner) { }
	}
}
