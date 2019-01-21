using System.Collections.Generic;

namespace Common
{
    public class HttpResult<T>
    {
        public Dictionary<string, string[]> Error { get; set; }
        public T Data { get; set; }

        public HttpResult()
        {
            Error = new Dictionary<string, string[]>();
        }
    }
}
