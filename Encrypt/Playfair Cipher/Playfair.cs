using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encrypt
{
    class Playfair : Encrypt
    {
        private string message = "secret message";
        private string initialMessage = "";
        private string decryptedMessage = "";
        private string keyword = "keywordkw";
        private string alfabet = "abcdefghiklmnopqrstuvwxyz";
        private char[,] matrix = new char[5, 5];
        private string aux = "";
        private string aux2 = "";
        private string[] pairs;
        private string encryptedMessage = "";

        public override string EncryptedMessage { get => encryptedMessage; set => encryptedMessage = value; }
        public override string InitialMessage { get => initialMessage; set => initialMessage = value; }
        public override string KeywordString { get => keyword; set => keyword = value; }
        public override string DecryptedMessage { get => decryptedMessage; set => decryptedMessage = value; }

        public Playfair(string message, string keyword)
        {
            this.message = message;
            this.keyword = keyword;
            initialMessage = message;
        }

        public override void encrypt()
        {
            makeVectorMatrix();
            /*
            Console.WriteLine();
            showMatrix();
            Console.WriteLine();
            */
            addX();
            splitInPairs();
            check();
        }

        public override void decrypt()
        {
            check2();
        }

        /// <summary>
        /// puts the vector in one matrix
        /// </summary>
        private void addInMatrix()
        {
            int l = 0, c = 0;
            for (int i = 0; i < 25; i++)
            {
                if (c < 5)
                {
                    matrix[l, c] = aux[i];
                    c++;
                }
                else
                {
                    l++;
                    c = 0;
                    matrix[l, c] = aux[i];
                    c++;
                }
            }
        }

        /// <summary>
        /// creates the key and puts it in a vector
        /// </summary>
        private void makeVectorMatrix()
        {
            keyword = keyword.ToLower();
            Dictionary<char, bool> caracteristic = new Dictionary<char, bool>();
            for (int i = 0; i < alfabet.Length; i++)
                caracteristic.Add(alfabet[i], false);

            aux = "";
            for (int i = 0; i < keyword.Length; i++)
            {
                if (caracteristic[keyword[i]] != true)
                {
                    caracteristic[keyword[i]] = true;
                    aux = aux + keyword[i];
                }
            }

            for (int i = 0; i < alfabet.Length; i++)
            {
                if (caracteristic[alfabet[i]] != true && aux.Length < 25)
                    aux = aux + alfabet[i];
            }
            addInMatrix();
        }

        /// <summary>
        /// matrix key
        /// </summary>
        private void showMatrix()
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                    Console.Write(matrix[i, j] + " ");
                Console.WriteLine();
            }
        }

        /// <summary>
        /// add "X" where we need: between 2 equal characters, if !even i add x in the end
        /// </summary>
        private void addX()
        {
            message = message.Replace(" ", "");
            message = message.ToLower();

            aux2 = "";
            for (int i = 0; i < message.Length - 1; i += 2)
                if (message[i] == message[i + 1])
                {
                    aux2 = aux2 + message[i] + "x" + message[i + 1];
                }
                else
                {
                    aux2 = aux2 + message[i] + message[i + 1];
                }
            if (message.Length % 2 != 0)
                aux2 = aux2 + message[message.Length - 1];
            message = aux2;
        }

        /// <summary>
        /// splits the message in pairs
        /// </summary>
        private void splitInPairs()
        {
            int index;
            if (message.Length % 2 == 0)
            {
                pairs = new string[message.Length / 2];
                index = 0;
                for (int i = 0; i < message.Length; i += 2)
                {
                    pairs[index] = pairs[index] + message[i] + message[i + 1];
                    index++;
                }
            }
            else
            {
                pairs = new string[message.Length / 2 + 1];
                index = 0;
                message = message + "x";
                for (int i = 0; i < message.Length; i += 2)
                {
                    pairs[index] = pairs[index] + message[i] + message[i + 1];
                    index++;
                }
            }
        }

        /// <summary>
        /// verificam in ce sitatie ne aflam si cream in finctie de situatie mesajul criptat
        /// </summary>
        private void check()
        {
            encryptedMessage = "";
            for (int x = 0; x < pairs.Length; x++)
            {
                int row1 = 0;
                int col1 = 0;
                int row2 = 0;
                int col2 = 0;
                getPos(matrix, pairs[x][0], ref row1, ref col1);
                getPos(matrix, pairs[x][1], ref row2, ref col2);

                if (col1 == col2)
                {
                    string s = "";
                    if (row1 + 1 < 5)
                        s = s + matrix[row1 + 1, col1];
                    else
                        s = s + matrix[0, col1];
                    if (row2 + 1 < 5)
                        s = s + matrix[row2 + 1, col2];
                    else
                        s = s + matrix[0, col2];
                    pairs[x] = s;
                    encryptedMessage = encryptedMessage + s;
                }
                else if (row1 == row2)
                {
                    string s = "";
                    if (col1 + 1 < 5)
                        s = s + matrix[row1, col1 + 1];
                    else
                        s = s + matrix[row1, 0];
                    if (col2 + 1 < 5)
                        s = s + matrix[row2, col2 + 1];
                    else
                        s = s + matrix[row2, 0];
                    pairs[x] = s;
                    encryptedMessage = encryptedMessage + s;
                }
                else
                {
                    string s = "";
                    s = s + matrix[row1, col2];
                    s = s + matrix[row2, col1];
                    pairs[x] = s;
                    encryptedMessage = encryptedMessage + s;
                }
            }
        }
        /// <summary>
        /// verificam in ce sitatie ne aflam si cream in finctie de situatie mesajul decriptat
        /// </summary>
        private void check2()
        {
            decryptedMessage = "";
            for (int x = 0; x < pairs.Length; x++)
            {
                int row1 = 0;
                int col1 = 0;
                int row2 = 0;
                int col2 = 0;
                getPos(matrix, pairs[x][0], ref row1, ref col1);
                getPos(matrix, pairs[x][1], ref row2, ref col2);

                if (col1 == col2)
                {
                    string s = "";
                    if (row1 > 0)
                        s = s + matrix[row1 - 1, col1];
                    else
                        s = s + matrix[0, col1];
                    if (row2 > 0)
                        s = s + matrix[row2 - 1, col2];
                    else
                        s = s + matrix[4, col2];
                    pairs[x] = s;
                    decryptedMessage = decryptedMessage + s;
                }
                else if (row1 == row2)
                {
                    string s = "";
                    if (col1 > 0)
                        s = s + matrix[row1, col1 - 1];
                    else
                        s = s + matrix[row1, 4];
                    if (col2 > 0)
                        s = s + matrix[row2, col2 - 1];
                    else
                        s = s + matrix[row2, 4];
                    pairs[x] = s;
                    decryptedMessage = decryptedMessage + s;
                }
                else
                {
                    string s = "";
                    s = s + matrix[row1, col2];
                    s = s + matrix[row2, col1];
                    pairs[x] = s;
                    decryptedMessage = decryptedMessage + s;
                }
            }
            //delete x
            for (int i = 0; i < decryptedMessage.Length; i++)
                if (decryptedMessage[i] == 'x')
                    decryptedMessage = decryptedMessage.Replace("x", "");
        }

        /// <summary>
        /// returneaza linia si coloana a elemetului "item" din matrice
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="item"></param>
        /// <param name="l"></param>
        /// <param name="c"></param>
        public static void getPos(char[,] matrix, char item, ref int l, ref int c)
        {
            if (item == 'j')
                getPos(matrix, 'i', ref l, ref c);
            for (int i = 0; i < 5; ++i)
                for (int j = 0; j < 5; ++j)
                    if (matrix[i, j] == item)
                    {
                        l = i;
                        c = j;
                    }
        }
    }
}
