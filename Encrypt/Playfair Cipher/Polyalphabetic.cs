using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encrypt
{
    class Polyalphabetic : Encrypt
    {
        private string initialMessage = "";
        private string encryptedMessage = "";
        private string decryptedMessage = "";
        private string keyword = "";
        private string alfabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private char[,] matrix = new char[26, 26];
        
        public override string EncryptedMessage { get => encryptedMessage; set => encryptedMessage = value; }
        public override string InitialMessage { get => initialMessage; set => initialMessage = value; }
        public override string DecryptedMessage { get => decryptedMessage; set => decryptedMessage = value; }
        public override string KeywordString { get => keyword; set => keyword = value; }

        public Polyalphabetic(string initialMessage, string key)
        {
            this.initialMessage = initialMessage;
            setKeyword(key);
        }

        public override void decrypt()
        {
            decryptedMessage = "";

            for (int i = 0; i < encryptedMessage.Length; i++)
                decryptedMessage = decryptedMessage + matrix[getPos(matrix[0,getPos(encryptedMessage[i],0)], getPos(keyword[i],0)),0];
        }

        public override void encrypt()
        {
            encryptedMessage = "";
            initMatrix();
            //showMatrix();
            
            for (int i=0;i<initialMessage.Length;i++)
                encryptedMessage= encryptedMessage+matrix[getPos(initialMessage[i],0), getPos(keyword[i],0)];
        }

        /// <summary>
        /// here we create the matrix - Vigenere Table
        /// </summary>
        private void initMatrix()
        {
            //first line
            for (int i = 0; i < 26; i++)
                matrix[0, i] = alfabet[i];
            for (int i = 1; i < 26; i++)
            {
                int index = i;
                for (int j = 0; j < 26; j++)
                {
                    if (index < 26)
                        matrix[i, j] = alfabet[index];
                    else
                    {
                        index = 0;
                        matrix[i, j] = alfabet[index];
                    }
                    index++;
                }
            }
        }

        /// <summary>
        /// show the matrix in console, help in production
        /// </summary>
        private void showMatrix()
        {
            for(int i=0;i<26;i++)
            {
                for(int j=0;j<26;j++)
                    Console.Write(matrix[i,j]+" ");
                Console.WriteLine();
            }
        }

        /// <summary>
        /// 1.keyword.length == initialMessage.length
        /// ex. initialKeyword="key", initialMessage="hellothere" => keyword="keykeykeyk"
        /// </summary>
        /// <param name="key"></param>
        private void setKeyword(string key)
        {
            keyword = key;
            string initialKeyword = keyword;
            while (initialMessage.Length > keyword.Length)
            {
                if(initialMessage.Length>=2* initialKeyword.Length)
                {
                    keyword = keyword + initialKeyword;
                }
                else
                {
                    for(int i=0;i<=initialMessage.Length-keyword.Length;i++)
                        keyword = keyword + initialKeyword[i];
                }
            }
        }

        /// <summary>
        /// returns the position in line "li" of char "c"
        /// </summary>
        /// <param name="c"></param>
        /// <param name="li"></param>
        /// <returns></returns>
        private int getPos(char c,int li)
        {
            int solution = 0;
            for (int i = 0; i < 26; i++)
            {
                if (matrix[i,li] == c)
                {
                    solution = i;
                    break;
                }
            }
            return solution;
        }
    }
}
