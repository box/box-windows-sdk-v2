using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Box.V2.Services
{
    public interface IBoxConverter
    {
        T Parse<T>(string content);

        string Serialize<T>(T entity);
    }
}
