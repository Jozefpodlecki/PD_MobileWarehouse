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

        static bool ByteArrayCompare(byte[] b1, byte[] b2)
        {
            if (b1.Length != b2.Length)
            {
                return false;
            }

            if (ReferenceEquals(b1, b2))
            {
                return true;
            }

            for (int i = 0; i < b1.Length; i++)
            {
                if (b1[i] != b2[i])
                {
                    return false;
                }
            }

            return true;
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
