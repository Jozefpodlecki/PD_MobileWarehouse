using Common.Managers;
using Data_Access_Layer;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

namespace WebApiServer.Managers
{
    public class PasswordManager : IPasswordManager
    {
        private readonly HashAlgorithm _hashAlgorithm;

        [DllImport("msvcrt.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern int memcmp(byte[] b1, byte[] b2, long count);

        static bool ByteArrayCompare(byte[] b1, byte[] b2)
        {
            return b1.Length == b2.Length && memcmp(b1, b2, b1.Length) == 0;
        }

        public PasswordManager()
        {
            _hashAlgorithm = new SHA512Managed();
        }

        public byte[] GetHash(string password)
        {
            var encodedPassword = Encoding.Unicode.GetBytes(password);
            return _hashAlgorithm.ComputeHash(encodedPassword);
        }

        public bool Compare(User user, byte[] hashPassword)
        {
            return ByteArrayCompare(user.PasswordHash, hashPassword);
        }

        public bool Compare(User user, string password)
        {
            var result = GetHash(password);

            return ByteArrayCompare(user.PasswordHash, result);
        }
    }
}
