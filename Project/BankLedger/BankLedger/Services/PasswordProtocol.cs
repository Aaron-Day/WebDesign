using System;
using System.Security.Cryptography;

namespace BankLedger.Services
{
    public class PasswordProtocol
    {
        public static string PasswordSalt
        {
            get
            {
                // encryption service
                var rng = new RNGCryptoServiceProvider();

                // empty buffer
                var buff = new byte[64];

                // fill buffer with salt
                rng.GetBytes(buff);

                // convert salt to string and return
                return Convert.ToBase64String(buff);
            }
        }
    }
}