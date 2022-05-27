using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sytafe.Extensions
{
    public static class StringExtensions
    {
        public static T JsonDeserialize<T>(this string value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }
    }
}
