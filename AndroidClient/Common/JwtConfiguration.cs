using System;
using System.Text;

namespace Common
{
    public class JwtConfiguration
    {
        public JwtConfiguration()
        {
        }

        public byte[] _byteKey;
        public byte[] ByteKey
        {
            get
            {
                if (_byteKey == null && Key != null)
                {
                    _byteKey = Encoding.Unicode.GetBytes(Key);
                }

                return _byteKey;
            }
        }

        public string Key { get; set; }
        public string Issuer { get; set; }
        public int ExpireDays { get; set; }

        public DateTime Expires => DateTime.Now.AddDays(ExpireDays);
    }
}
