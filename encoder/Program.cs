using System;

namespace encoder
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter Path: ");
            string clearTextFilePath = Console.ReadLine();
            Console.WriteLine("Enter Encode Type: ");
            string EncodeType = Console.ReadLine();
            Encoder encoder = new Encoder(EncodeType, clearTextFilePath);
            encoder.Encode();
        }
    }
}
