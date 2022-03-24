using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicConfigLibrary
{
    public interface IConfigurationReader
    {
        Task<T> GetValue<T>(string key);
        bool SetValue<T>(string key, T value);

    }
}
