using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ukol_A
{
    class ItemNotFoundException : Exception
    {
        public ItemNotFoundException(string message) 
            : base(message)
        { }

        public ItemNotFoundException()
            : base("Zadaná položka v grafu neexistuje!")
        { }
    }
}
