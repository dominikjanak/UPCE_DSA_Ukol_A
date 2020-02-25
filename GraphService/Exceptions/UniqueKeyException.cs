using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ukol_A
{
    class UniqueKeyException : Exception
    {
        public UniqueKeyException(string message) 
            : base(message)
        { }

        public UniqueKeyException()
            : base("Zadaný klíč v grafu již existuje!")
        { }
    }
}
