
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Cargo4You.Services.Sequrity
{
    public class PasswordManager
    {
        const int defaultHashSize = 256 / 8; // 32 bytes
        const int defaultHashIterations = 1000;
        const int defaultSaltSize = 128 / 8; // 16 bytes

        private int? _hashSize;
        private int? _hashIterations;
        private int? _saltSize;

        public int HashSize
        {
            get { return _hashSize ?? defaultHashSize; }
            set { _hashSize = value; }
        }

        public int HashIterations
        {
            get { return _hashIterations ?? defaultHashIterations; }
            set { _hashIterations = value; }
        }

        public int SaltSize
        {
            get { return _saltSize ?? defaultSaltSize; }
            set { _saltSize = value; }
        }

        public PasswordManager()
        {

        }

        public PasswordManager(int hashSize, int hashIterations, int saltSize)
        {
            HashSize = hashSize;
            HashIterations = hashIterations;
            SaltSize = saltSize;
        }


        public byte[] GenerateSalt()
        {
            var saltBytes = new byte[SaltSize];

            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);
            }

            return saltBytes;
        }


        public string Hash(string password, byte[] salt)
        {
            var passwordHash = PBKDF2(password, salt, HashIterations, HashSize);

            var passwordHashingParts = "sha1:" + HashIterations + ":" +
                                        passwordHash.Length + ":" +
                                        Convert.ToBase64String(salt) + ":" +
                                        Convert.ToBase64String(passwordHash);

            return passwordHashingParts;
        }

        private static byte[] PBKDF2(string password, byte[] salt, int iterations, int outputBytes)
        {
            using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt))
            {
                pbkdf2.IterationCount = iterations;
                return pbkdf2.GetBytes(outputBytes);
            }
        }


    }
}
