using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RangeTree
{
    /// <summary>
    /// Interface for data objects represented by TValue
    /// </summary>
    public interface IValue
    {
        float X { get; }
        float Y { get; }
    }
}
