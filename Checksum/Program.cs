using System;
using System.IO;

namespace Checksum
{
    public  class Program
    {
        public static void Main(string[] args)
        {
            string fileName = "../../../checksum - input.txt";
            string cijferReeks =File.ReadAllText(fileName);

            //cijferReeks = "91212129";

            Console.WriteLine($"Checksum volgende cijfer: {ChecksumBerekenaar.BerekenChecksum(cijferReeks, 1)}");
            Console.WriteLine($"Checksum halfweg: {ChecksumBerekenaar.BerekenChecksum(cijferReeks, (int)(cijferReeks.Length/2))}");
        }
        
    }

    public static class ChecksumBerekenaar
    {
        public static int BerekenChecksum(string reeks, int offset)
        {
            int length = reeks.Length;
            int checkSum = 0;

            for (int index = 0; index < length; index++)
            {
                if (reeks[index] == reeks[(index + offset) % length])
                {
                    checkSum += (int)(reeks[index] - '0');
                }
            }

            return checkSum;
        }
    }
}



