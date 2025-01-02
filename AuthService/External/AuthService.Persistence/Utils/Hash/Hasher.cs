using AuthService.Application.Abstractions.Hash;
using System.Security.Cryptography;
using System.Text;

namespace AuthService.Persistence.Utils.Hash
{
    public class Hasher : IHasher
    {
        public string Hash(string plainText)
        {
            byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(plainText));

            string hashed = Convert.ToHexString(bytes);

            return hashed;
        }
    }
}
