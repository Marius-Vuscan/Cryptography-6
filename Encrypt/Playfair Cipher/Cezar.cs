using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encrypt
{
    class Cezar : Encrypt
    {
        private string initialMessage;
        private int keyword = 3;
        private string encryptedMessage = "";
        private string decryptedMessage = "";

        public override string EncryptedMessage { get => encryptedMessage; set => encryptedMessage = value; }
        public override string InitialMessage { get => initialMessage; set => initialMessage = value; }
        public override int KeywordInt { get => keyword; set => keyword = value; }
        public override string DecryptedMessage { get => decryptedMessage; set => decryptedMessage=value; }

        /// <summary>
        /// User chose the key
        /// </summary>
        /// <param name="initialMessage"></param>
        /// <param name="keyword"></param>
        public Cezar(string initialMessage, int keyword)
        {
            this.initialMessage = initialMessage;
            this.keyword = keyword;
        }

        /// <summary>
        /// Rot13/ Default
        /// </summary>
        /// <param name="initialMessage"></param>
        /// <param name="b"></param>
        public Cezar(string initialMessage, bool b)
        {
            if (b == false)
                CezarDefault(initialMessage);
            else
            {
                this.initialMessage = initialMessage;
                keyword = 13;
            }
        }

        /// <summary>
        /// Cezar n=3
        /// </summary>
        /// <param name="initialMessage"></param>
        private void CezarDefault(string initialMessage)
        {
            this.initialMessage = initialMessage;
            keyword = 3;
        }

        public override void decrypt()
        {
            for (int i = 0; i < encryptedMessage.Length; i++)
            {
                char d = char.IsUpper(encryptedMessage[i]) ? 'A' : 'a';
                decryptedMessage = decryptedMessage + (char)((((encryptedMessage[i] + 26 - keyword) - d) % 26) + d);
            }
        }

        public override void encrypt()
        {
            for (int i = 0; i < initialMessage.Length; i++)
            {
                char d = char.IsUpper(initialMessage[i]) ? 'A' : 'a';
                encryptedMessage = encryptedMessage + (char)((((initialMessage[i] + keyword) - d) % 26) + d);
            }
        }
    }
}
