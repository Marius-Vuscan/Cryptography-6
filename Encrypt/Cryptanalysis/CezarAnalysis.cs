using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Cryptanalysis
{
    class CezarAnalysis
    {
        /// <summary>
        /// verifica fiecare codare cu Cezar (1-26) si calculeaza numarul aparitiilor
        /// fiecare decriptare va avea ca scor numarul de cuvinte corecte(engleza)
        /// </summary>
        /// <param name="text"></param>
        public void decrypt(string path)
        {
            string text= File.ReadAllText(path);
            
            //delete punctuation
            text = text.ToLower();
            text = text.Replace("\n"," ");
            var sb = new StringBuilder();

            foreach (char c in text)
            {
                if (!char.IsPunctuation(c))
                    sb.Append(c);
            }
            text = sb.ToString();

            Console.WriteLine("Initial text: "+text);

            //decriptare si calculare scor
            int[] score = new int[26];
            StreamReader sr = new StreamReader(@"../../EnglishMostUsedWords.txt");
            List<string> textfile = new List<string>();
            string buffer;
            while ((buffer = sr.ReadLine()) != null)
            {
                textfile.Add(buffer);
                for (int j = 0; j < score.Length; j++)
                {
                    string text1 = decryptCezar(text, j);
                    string[] words = text1.Split(' ');
                    for (int i = 0; i < words.Length; i++)
                    {
                        if (words[i] == buffer)
                            score[j]++;
                    }
                }
            }

            //decriptarea cu scorul cel mai mare este afisata
            int max = 0;
            int index = 0;
            for (int i = 0; i < score.Length; i++)
            {
                if (score[i]>max)
                {
                    max = score[i];
                    index = i;
                }
            }
            Console.WriteLine("\nDecrypted text: "+decryptCezar(text,index));
        }

        /// <summary>
        /// decripteaza daca stie cheia
        /// </summary>
        /// <param name="encryptedMessage"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public string decryptCezar(string encryptedMessage, int keyword)
        {
            string decryptedMessage = "";
            for (int i = 0; i < encryptedMessage.Length; i++)
            {
                if (encryptedMessage[i] == ' ')
                    decryptedMessage = decryptedMessage + " ";
                else
                {
                    char d = char.IsUpper(encryptedMessage[i]) ? 'A' : 'a';
                    decryptedMessage = decryptedMessage + (char)((((encryptedMessage[i] + 26 - keyword) - d) % 26) + d);
                }
            }
            return decryptedMessage;
        }
    }
}
