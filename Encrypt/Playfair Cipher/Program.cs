using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encrypt
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Playfair: ");
            Playfair p = new Playfair("secret", "keywordkw");
            Console.WriteLine("Initial message: "+p.InitialMessage);
            p.encrypt();
            Console.WriteLine("Encrypted message: "+p.EncryptedMessage);
            p.decrypt();
            Console.WriteLine("Decrypted message: " + p.DecryptedMessage);

            Console.WriteLine("\nCezar3: ");
            Cezar c = new Cezar("secret",false);
            Console.WriteLine("Initial message: " + c.InitialMessage);
            c.encrypt();
            Console.WriteLine("Encrypted message: " + c.EncryptedMessage);
            c.decrypt();
            Console.WriteLine("Decrypted message: " + c.DecryptedMessage);

            Console.WriteLine("\nCezar13: ");
            Cezar c3 = new Cezar("secret", true);
            Console.WriteLine("Initial message: " + c3.InitialMessage);
            c3.encrypt();
            Console.WriteLine("Encrypted message: " + c3.EncryptedMessage);
            c3.decrypt();
            Console.WriteLine("Decrypted message: " + c3.DecryptedMessage);

            Console.WriteLine("\nCezarN=15: ");
            Cezar c2 = new Cezar("secret", 15);
            Console.WriteLine("Initial message: " + c2.InitialMessage);
            c2.encrypt();
            Console.WriteLine("Encrypted message: " + c2.EncryptedMessage);
            c2.decrypt();
            Console.WriteLine("Decrypted message: " + c2.DecryptedMessage);

            Console.WriteLine("\nSubstitutie Monoalfabetica: ");
            Monoalphabetical ms = new Monoalphabetical("MESSAGE");
            Console.WriteLine("Initial message: " + ms.InitialMessage);
            ms.encrypt();
            Console.WriteLine("Encrypted message: " + ms.EncryptedMessage);
            ms.decrypt();
            Console.WriteLine("Decrypted message: " + ms.DecryptedMessage);

            Console.WriteLine("\nSubstitutie Polialfabetica: ");
            Polyalphabetic sp = new Polyalphabetic("MESSAGE","UNKEY");
            Console.WriteLine("Initial message: " + sp.InitialMessage);
            sp.encrypt();
            Console.WriteLine("Encrypted message: " + sp.EncryptedMessage);
            sp.decrypt();
            Console.WriteLine("Decrypted message: " + sp.DecryptedMessage);
            Console.ReadKey();
        }
    }
}
