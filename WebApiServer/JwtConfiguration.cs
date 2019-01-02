using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiServer
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
                if(_byteKey == null && Key != null)
                {
                    _byteKey = Encoding.Unicode.GetBytes(Key);
                }

                return _byteKey;
            }
        } 

        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int Expires { get; set; }
    }
}
