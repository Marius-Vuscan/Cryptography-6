using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encrypt
{
    class Monoalphabetical : Encrypt
    {
        private string initialMessage="";
        private string alfabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private string keyword = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private string encryptedMessage = "";
        private string decryptedMessage = "";
        private Dictionary<char, char> dic = new Dictionary<char, char>();
        private Dictionary<char, char> dic2 = new Dictionary<char, char>();

        public override string EncryptedMessage { get => encryptedMessage; set => encryptedMessage = value; }
        public override string InitialMessage { get => initialMessage; set => initialMessage = value; }
        public override string DecryptedMessage { get => decryptedMessage; set => decryptedMessage = value; }
        public override string KeywordString { get => keyword; set => keyword = value; }

        public Monoalphabetical(string initialMessage)
        {
            this.initialMessage = initialMessage;
        }

        public override void decrypt()
        {
            decryptedMessage = "";
            makeDictionary2();
            for (int i = 0; i < encryptedMessage.Length; i++)
            {
                decryptedMessage = decryptedMessage + dic2[encryptedMessage[i]];
            }
        }

        public override void encrypt()
        {
            makeKey();
            makeDictionary();
            encryptedMessage = "";
            for (int i=0;i<initialMessage.Length;i++)
            {
                encryptedMessage = encryptedMessage + dic[initialMessage[i]];
            }

        }

        /// <summary>
        /// dictioary use for encrypt
        /// </summary>
        private void makeDictionary()
        {
            for(int i=0;i<alfabet.Length;i++)
                dic.Add(alfabet[i], keyword[i]);
        }

        /// <summary>
        /// dictionary use for decrypt - opposite of "dic"
        /// </summary>
        private void makeDictionary2()
        {
            for (int i = 0; i < alfabet.Length; i++)
                dic2.Add(keyword[i], alfabet[i]);
        }

        /// <summary>
        /// creates a random key by shuffleing the alphabeat
        /// </summary>
        private void makeKey()
        {
            Random r = new Random();

            char[] letters = new char[26];

            for (int i = 0; i < 26; i++)
            {
                letters[i] = alfabet[i];
            }

            //shuffle
            int n = letters.Length;
            for (int i = 0; i < n; i++)
            {
                int r2 = i + r.Next(n - i);
                var t = letters[r2];
                letters[r2] = letters[i];
                letters[i] = t;
            }

            keyword = "";
            for (int i = 0; i < 26; i++)
            {
                keyword = keyword + letters[i];
            }
        }
    }
}
