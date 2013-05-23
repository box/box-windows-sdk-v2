using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Box.V2
{
    public interface IBoxFormPart
    {
        string Name { get; }
    }

    public interface IBoxFormPart<T> : IBoxFormPart
    {
        T Value { get; }
    }
}
