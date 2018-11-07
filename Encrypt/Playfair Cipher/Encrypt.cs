using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encrypt
{
    abstract class Encrypt
    {
        public abstract string EncryptedMessage { get; set; }
        public abstract string InitialMessage { get; set; }
        public abstract string DecryptedMessage { get; set; }
        public virtual string KeywordString { get; set; }
        public virtual int KeywordInt { get; set; }
        public abstract void encrypt();
        public abstract void decrypt();
    }
}
