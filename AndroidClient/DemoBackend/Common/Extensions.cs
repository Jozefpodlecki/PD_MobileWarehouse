using System;

namespace Common
{
    public static class Extensions
    {
        public static string GetFullName(this Enum renum)
        {
            return $"{renum.GetType().Name}.{renum.ToString()}";
        }
    }
}
