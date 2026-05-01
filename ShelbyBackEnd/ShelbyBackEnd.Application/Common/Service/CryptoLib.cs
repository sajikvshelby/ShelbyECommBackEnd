using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ShelbyBackEnd.Application.Common.Service
{
    public class CryptoLib
    {
        private const int HASH_BYTES = 18;
        private const int PBKDF2_ITERATIONS = 64000;

        public enum ByteSizes
        {
            Sixteen = 16,
            ThirtyTwo = 32,
            SixtyFour = 64,
            OneTwentyEight = 128,
            TwoFiftySix = 256,
            FiveTwelve = 512
        }
        public enum Projects
        {
            SHELBYECOMM
        }

        #region "Encryption"

        public static string Encrypt(string DataToEncrypt, CryptoLib.Projects ProjectName)
        {
            FetchRegKeys Fetch = new FetchRegKeys(ProjectName);
            byte[] KeyBytes = Convert.FromBase64String(Fetch.GetSecretKey()); // 32 Bytes
            byte[] IVBytes = Convert.FromBase64String(Fetch.GetCommonKey()); // 16 Bytes

            using (AesCng AESObj = new AesCng())
            {
                AESObj.KeySize = 256;
                AESObj.BlockSize = 128;

                byte[] DataByteArray = Encoding.UTF8.GetBytes(DataToEncrypt);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, AESObj.CreateEncryptor(KeyBytes, IVBytes), CryptoStreamMode.Write);
                cs.Write(DataByteArray, 0, DataByteArray.Length);
                cs.FlushFinalBlock();

                return Convert.ToBase64String(ms.ToArray());
            }
        }

        public static string Decrypt(string DataToDecrypt, CryptoLib.Projects ProjectName)
        {
            FetchRegKeys Fetch = new FetchRegKeys(ProjectName);
            byte[] KeyBytes = Convert.FromBase64String(Fetch.GetSecretKey()); // 32 Bytes
            byte[] IVBytes = Convert.FromBase64String(Fetch.GetCommonKey()); // 16 Bytes
            string DecryptedVal = "";

            try
            {
                using (AesCng AESObj = new AesCng())
                {
                    AESObj.KeySize = 256;
                    AESObj.BlockSize = 128;

                    if (DataToDecrypt.Contains(" "))
                    {
                        DataToDecrypt = DataToDecrypt.Replace(" ", "+");
                    }

                    byte[] DataByteArray = new byte[DataToDecrypt.Length];
                    DataByteArray = Convert.FromBase64String(DataToDecrypt);

                    MemoryStream ms = new MemoryStream();
                    CryptoStream cs = new CryptoStream(ms, AESObj.CreateDecryptor(KeyBytes, IVBytes), CryptoStreamMode.Write);
                    cs.Write(DataByteArray, 0, DataByteArray.Length);
                    cs.FlushFinalBlock();

                    DecryptedVal = Encoding.UTF8.GetString(ms.ToArray());
                }
            }
            catch
            {
                DecryptedVal = "";
            }

            return DecryptedVal;
        }

        #endregion

        #region "Hashing"

        public static string CreateHash(string DataToHash, CryptoLib.Projects ProjectName)
        {
            FetchRegKeys Fetch = new FetchRegKeys(ProjectName);
            string SecretKey = Fetch.GetHashKey(); // 128 bytes
                                                   // The salt needs to be unique per-user per-password. Every time a user creates an account or changes their 
                                                   // password, the password should be hashed using a new random salt. Never reuse a salt. The salt also needs 
                                                   // to be long, so that there are many possible salts. As a rule of thumb, make sure your salt is at least as long 
                                                   // as the hash function's output.
            int KeySize = Convert.FromBase64String(SecretKey).Length;
            byte[] SaltBytes = new byte[96];

            // Salt should be generated using a Cryptographically Secure Pseudo-Random Number Generator (CSPRNG). 
            // CSPRNGs are very different than ordinary pseudo-random number generators, like the rand() function. 
            // CSPRNGs are designed to be cryptographically secure, providing a high level of randomness and unpredictability.
            //using (RNGCryptoServiceProvider CSprng = new RNGCryptoServiceProvider())
            using (var CSprng = RandomNumberGenerator.Create())
            {
                CSprng.GetBytes(SaltBytes);
            }

            // The salt should be stored in the user account table alongside the hash.
            byte[] HashBytes = GetPBKDF2(DataToHash, SaltBytes);

            // Use the secret key to sign the password.
            byte[] SignedHash = SignHash(HashBytes, SecretKey);

            // Save both the salt And the hash in the user's database record.
            string Parts = Convert.ToBase64String(SaltBytes) + ":" + Convert.ToBase64String(SignedHash);
            return Parts;
        }
        public static bool VerifyHash(string DataToCompare, string StoredHashParts, CryptoLib.Projects ProjectName)
        {
            if (!StoredHashParts.Contains(":"))
            {
                return false;
            }

            FetchRegKeys Fetch = new FetchRegKeys(ProjectName);
            string SecretKey = Fetch.GetHashKey();
            try
            {
                // Retrieve the user's salt and hash from the database.
                string[] Parts = StoredHashParts.Split(':');
                byte[] StoredSaltBytes = Convert.FromBase64String(Parts[0]);
                byte[] StoredPwdBytes = Convert.FromBase64String(Parts[1]);

                // Prepend the salt to the given password And hash it using the same hash function.
                byte[] ComparePwdBytes = GetPBKDF2(DataToCompare, StoredSaltBytes);

                // Use the secret key to sign the given password.
                byte[] SignedComparePwdBytes = SignHash(ComparePwdBytes, SecretKey);

                // Compare the hash Of the given password With the hash from the database. If they match, the password Is correct. Otherwise, the password Is incorrect.
                return SlowEquals(StoredPwdBytes, SignedComparePwdBytes);
            }
            catch
            {
                return false;
            }
        }

        private static byte[] GetPBKDF2(string HashData, byte[] Salt)
        {
            // Prepend the salt to the password and hash it with a standard password hashing function like PBKDF2.
            // Key stretching is implemented using a special type of CPU-intensive hash function like PBKDF2.

            //using (Rfc2898DeriveBytes pbkdf2obj = new Rfc2898DeriveBytes(HashData, Salt))
            //{
            //    pbkdf2obj.IterationCount = PBKDF2_ITERATIONS;
            //    return pbkdf2obj.GetBytes(HASH_BYTES);
            //}

            using (var pbkdf2obj = new Rfc2898DeriveBytes(HashData, Salt, PBKDF2_ITERATIONS, HashAlgorithmName.SHA512))
            {
                return pbkdf2obj.GetBytes(HASH_BYTES);
            }
        }

        private static byte[] SignHash(byte[] HashBytes, string SecretKey)
        {
            // As long as an attacker can use a hash to check whether a password guess is right or wrong, 
            // they can run a dictionary or brute-force attack on the hash. The next step is to add a secret key 
            // to the hash so that only someone who knows the key can use the hash to validate a password. 
            // This can be accomplished by including the secret key in the hash using a keyed hash algorithm like HMAC.
            using (HMACSHA512 HmacObj = new HMACSHA512(Convert.FromBase64String(SecretKey)))
            {
                byte[] hashValue = HmacObj.ComputeHash(HashBytes);
                return hashValue;
            }
        }

        private static bool SlowEquals(byte[] a, byte[] b)
        {
            // The code uses the XOR "^" operator to compare integers for equality, instead of the "==" operator. The result of XORing two integers will be zero if and only if they are exactly the same. 
            // This is because 0 XOR 0 = 0, 1 XOR 1 = 0, 0 XOR 1 = 1, 1 XOR 0 = 1. If we apply that to all the bits in both integers, the result will be zero only if all the bits matched.
            // So, in the first line, If a.length Is equal to b.length, the diff variable will Get a zero value, but If Not, it will Get some non-zero value. 
            // Next, we compare the bytes Using Xor, And Or the result into diff. This will Set diff To a non-zero value If the bytes differ. 
            // Because ORing never un-sets bits, the only way diff will be zero at the end of the loop Is if it was zero before the loop began (a.length == b.length) And all of the bytes in the two arrays match (none of the XORs resulted in a non-zero value).
            // The reason we need to use Xor instead of the "==" operator to compare integers Is that "==" Is usually translated/compiled/interpreted as a branch.
            uint diff = System.Convert.ToUInt32(a.Length) ^ System.Convert.ToUInt32(b.Length);
            int i = 0;

            while (i < a.Length && i < b.Length)
            {
                diff = diff | System.Convert.ToUInt32((a[i] ^ b[i]));
                i += 1;
            }

            return (diff == 0);
        }


        #endregion


       

    }

    internal class FetchRegKeys
    {
        private string _SecretKey;  //32 bytes
        private string _CommonIV;   //16 bytes
        private string _HashKey;    //128 bytes

        public FetchRegKeys(CryptoLib.Projects ProjectName)
        {
            if (ProjectName == CryptoLib.Projects.SHELBYECOMM)
            {

                _SecretKey = SharedLib.GetRegKeyVal(@"SOFTWARE\SAIK", "SKeyEcomm");
                _CommonIV = SharedLib.GetRegKeyVal(@"SOFTWARE\SAIK", "CKeyEcomm");
                _HashKey = SharedLib.GetRegKeyVal(@"SOFTWARE\SAIK", "HKeyEcomm");
            }
        }

        public string GetCommonKey()
        {
            return _CommonIV;
        }
        public string GetSecretKey()
        {
            return _SecretKey;
        }
        public string GetHashKey()
        {
            return _HashKey;
        }

        


    }

}
