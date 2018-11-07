using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Cryptanalysis
{
    class MonoalphabeticalAnalysis
    {
        public class Aparitii
        {
            public int ap;
            public char litera;

            public Aparitii(char litera)
            {
                this.ap = 0;
                this.litera = litera;
            }
        }

        /// <summary>
        /// primeste ca argument calea de la un fisier cu un text in limba engleza si calculeaza numarul aparitiilor pentru fiecare litera
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private string aparitii(string path)
        {
            string text= File.ReadAllText(path);
            text = text.ToLower();
            Dictionary<char, int> dic = new Dictionary<char, int>();

            //initializare dictionar
            for (char c = 'a'; c <= 'z'; c++)
                dic.Add(c, 0);
            
            //numar aparitii pentru fiecare litera
            for (int i = 0; i < text.Length; i++)
                if (Char.IsLetter(text[i]))
                    dic[text[i]]++;

            //sortare descrescatoare
            var items = from pair in dic
                        orderby pair.Value descending
                        select pair;

            //creez un string care va contine literele orodonate descrescator in functie de numarul aparitiilor
            string aparitii = "";
            foreach (KeyValuePair<char, int> pair in items)
                aparitii = aparitii + pair.Key;

            return aparitii;
        }

        public void decrypt(string path,string path2)
        {
            string text = File.ReadAllText(path);
            Console.WriteLine("Encrypted: " + text);
            text = text.ToLower();

            string alfabetMostFreq = aparitii(path2);
            Dictionary<char, int> dic = new Dictionary<char, int>();

            //initializare dictionar
            for (char c = 'a'; c <= 'z'; c++)
                dic.Add(c, 0);

            //numar aparitii pentru fiecare litera
            for (int i = 0; i < text.Length; i++)
                if (Char.IsLetter(text[i]))
                    dic[text[i]]++;

            //ordonare dictionar descrescator
            var items = from pair in dic
                        orderby pair.Value descending
                        select pair;

            //fiecare char este inlocuit in functie de numarul aparitiilor, ex: a-numar aparitii maxim se inlocuieste cu b-numar de aparitii maxim
            string decripted = "";
            StringBuilder sb = new StringBuilder(text);
            for (int i = 0; i < text.Length; i++)
            {
                if (Char.IsLetter(text[i]))
                {
                    int k = 0;
                    bool ok = false;
                    foreach (KeyValuePair<char, int> pair in items)
                    {
                        if (pair.Key == text[i])
                        {
                            sb[i] = alfabetMostFreq[k];
                            ok = true;
                        }
                        k++;
                    }
                    if(ok==false)
                        sb[i] = text[i];
                }
            }
            Console.WriteLine("\nDecrypted: "+sb);
        }
    }
}
