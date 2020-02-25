using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ukol_A
{
    class UniqueItemException : Exception
    {
        public UniqueItemException(string message) 
            : base(message)
        { }

        public UniqueItemException()
            : base("Zadaná položka v grafu již existuje!")
        { }
    }
}
