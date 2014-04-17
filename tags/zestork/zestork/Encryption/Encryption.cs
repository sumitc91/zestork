using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace zestork.Encryption
{
    public class Encryption
    {
        public string getEncryptionKey(string plainText, string key)
        {            
            return AES.Encrypt(plainText, key);
        }
    }
}