using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace VMManager
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> addresses = new List<int>();
            Stack<int> stackOfBinaries = new Stack<int>();
            int[] pageNumber = new int[8];
            int[] offset = new int[8];

            readAddresses(addresses);

            //show the content of addresses
            foreach (var address in addresses)
            {
                Console.WriteLine(address);
            }

            //show the content of addresses in bits
            stackOfBinaries = DecToBin(64);
            Console.WriteLine(stackOfBinaries.Count);
            int i = 0;
            while (i<stackOfBinaries.Count)
            {
                if (i < 8)
                {
                    pageNumber[i] = stackOfBinaries.ToArray()[i];
                }
                else
                    offset[i - 8] = stackOfBinaries.ToArray()[i];

                i++;
            }

            Console.Write("Page number: ");
            bitsToString(pageNumber,8); 
            Console.Write("Offset: ");
            bitsToString(offset, 8);
        }

        public static void readAddresses(List<int> addresses)
        {
            string[] lines = File.ReadAllLines(@"C:\Users\Augustin Nshimiyiman\Google Drive\OCClasses\Spring 2017\Operating Systems\Assignment\Virtual Memory\addresses.txt");

            foreach (var line in lines)
            {
                addresses.Add(int.Parse(line));
            }
        }

        public static Stack<int> DecToBin(int decimalAddress)
        {
            Stack<int> stackOfBinaries = new Stack<int>();
            while (decimalAddress >=2)
            {
                int remainder = decimalAddress % 2;
                stackOfBinaries.Push(remainder); 
                decimalAddress = (decimalAddress - remainder) / 2;
            }
            stackOfBinaries.Push(1);
           
            while(16-stackOfBinaries.Count>0)
            {
                    stackOfBinaries.Push(0);
            }
            
            return stackOfBinaries;
        }

        public static void bitsToString(int [] arr, int size)
        {
            for(int counter=0; counter <size; counter++)
            {
                Console.Write(arr[counter]);
            }
            Console.WriteLine();
        }

    }
}
