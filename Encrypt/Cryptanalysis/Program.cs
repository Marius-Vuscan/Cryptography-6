using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Cryptanalysis
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Analiza cezar:");
            CezarAnalysis ca = new CezarAnalysis();
            ca.decrypt(@"../../CezarEncrypted.txt");

            Console.WriteLine("\n\nAnaliza monoalfabetic:");
            MonoalphabeticalAnalysis ma = new MonoalphabeticalAnalysis();
            ma.decrypt(@"../../MonoEncrypted.txt", @"../../MonoNormalText.txt");
            Console.ReadKey();
        }
    }
}