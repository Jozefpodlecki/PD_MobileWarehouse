using System;
using System.Collections.Generic;
using System.Linq;

namespace Common
{
    public static class Helpers
    {
        public static List<KeyValue> MakeKeyValuePairFromEnum<T>(Type type)
        {
            var names = Enum.GetNames(type);
            var values = (T[])Enum.GetValues(type);

            return Enumerable
                .Zip(names, values, (name, value) =>
                new KeyValue
                {
                    Id = Convert.ToInt32(value),
                    Name = name
                }).ToList();
        }
    }
   
}
