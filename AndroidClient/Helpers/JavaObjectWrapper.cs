using System.Threading;

namespace Client.Helpers
{
    public class JavaObjectWrapper<T> : Java.Lang.Object
    {
        public T Data;

        public JavaObjectWrapper(T data)
        {
            Data = data;
        }
    }
}