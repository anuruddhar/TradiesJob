#region Modification Log
/*------------------------------------------------------------------------------------------------------------------------------------------------- 
    System      -   TradiesJob
    Client      -   Fergus Software Ltd New Zealand         
    Module      -   Core
    Sub_Module  -   DataAccess

    Copyright   -   Anuruddha Rajapaksha 
 
 Modification History:
 ==================================================================================================================================================
 Date              Version      Modify by              Description
 --------------------------------------------------------------------------------------------------------------------------------------------------
 03/06/2022         1.0      Anuruddha         Initial Version.
--------------------------------------------------------------------------------------------------------------------------------------------------*/
#endregion

#region Namespace
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
#endregion

namespace TradiesJob.Core.DataAccess.Encrypter {
    public class Encrypter : IEncrypter {
        private const string KEY_ID = "!@#$%^&*()AX826Gwkf58e?s";
        private const int SALTLENTH = 4;
        private const string VECTOR_ID = "w48*+-36dfthjklo";


        public Encrypter() {

        }

        // create salted password to save in Db
        public byte[] CreateDBPassword(byte[] unsaltedPassword) {
            //Create a salt value
            var saltValue = new byte[SALTLENTH];

            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(saltValue);

            return CreateSaltedPassword(saltValue, unsaltedPassword);
        }

        // create a salted password given the salt value
        private byte[] CreateSaltedPassword(byte[] saltValue, byte[] unsaltedPassword) {
            // add the salt to the hash
            var rawSalted = new byte[unsaltedPassword.Length + saltValue.Length];
            unsaltedPassword.CopyTo(rawSalted, 0);
            saltValue.CopyTo(rawSalted, unsaltedPassword.Length);

            //Create the salted hash			
            var sha1 = SHA1.Create();
            var saltedPassword = sha1.ComputeHash(rawSalted);

            // add the salt value to the salted hash
            var dbPassword = new byte[saltedPassword.Length + saltValue.Length];
            saltedPassword.CopyTo(dbPassword, 0);
            saltValue.CopyTo(dbPassword, saltedPassword.Length);

            return dbPassword;
        }

        public string Decrypt(string cipherText) {
            var memStreamDecryptedData = new MemoryStream();

            var rij = default(Rijndael);
            rij = new RijndaelManaged();
            rij.Mode = CipherMode.CBC;

            var transform = rij.CreateDecryptor(Encoding.ASCII.GetBytes(KEY_ID), Encoding.ASCII.GetBytes(VECTOR_ID));
            var decStream = new CryptoStream(memStreamDecryptedData, transform, CryptoStreamMode.Write);
            var bytesData = Convert.FromBase64String(cipherText);
            try {
                decStream.Write(bytesData, 0, bytesData.Length);
            } catch (Exception ex) {
                throw new Exception("Error while writing encrypted data to the stream:" + ex.Message);
            }
            decStream.FlushFinalBlock();
            decStream.Close();

            return Encoding.ASCII.GetString(memStreamDecryptedData.ToArray());
        }

        public string Encrypt(string planText) {
            var memStreamEncryptedData = new MemoryStream();
            var rij = default(Rijndael);
            rij = new RijndaelManaged();
            rij.Mode = CipherMode.CBC;

            rij.Key = Encoding.ASCII.GetBytes(KEY_ID);
            rij.IV = Encoding.ASCII.GetBytes(VECTOR_ID);

            var bytesData = Encoding.ASCII.GetBytes(planText);

            var transform = rij.CreateEncryptor();
            var encStream = new CryptoStream(memStreamEncryptedData, transform, CryptoStreamMode.Write);

            try {
                encStream.Write(bytesData, 0, bytesData.Length);
            } catch (Exception ex) {
                throw new Exception("Error while writing encrypted data to the stream:" + ex.Message);
            }

            encStream.FlushFinalBlock();
            encStream.Close();

            return Convert.ToBase64String(memStreamEncryptedData.ToArray());
        }


    }
}
