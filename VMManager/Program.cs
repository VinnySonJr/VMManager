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

            addresses = readAddresses();

            //show the logical addresses
            Console.WriteLine("Showing the content of the logical address text file");
            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine("Address        Binary        PageNumber    Offset ");
            Console.WriteLine("-------        ------        ----------    ------ ");
            foreach (var address in addresses)
            {
                stackOfBinaries = DecToBin(address);
                int i = 0;
                while (i < stackOfBinaries.Count)
                {
                    if (i < 8)
                    {
                        pageNumber[i] = stackOfBinaries.ToArray()[i];
                    }
                    else
                        offset[i - 8] = stackOfBinaries.ToArray()[i];

                    i++;
                }

                Console.WriteLine(address + "    " + bitsToString(stackOfBinaries.ToArray(), 16)
                    + "    " + bitsToString(pageNumber, 8) + "    " + bitsToString(offset, 8));
            }

        }

        public static List<int> readAddresses()
        {
            List<int> addresses = new List<int>();
            string[] lines = File.ReadAllLines(@"C:\Users\Augustin Nshimiyiman\Google Drive\OCClasses\Spring 2017\Operating Systems\Assignment\Virtual Memory\addresses.txt");

            foreach (var line in lines)
            {
                addresses.Add(int.Parse(line));
            }

            return addresses;
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

        public static string bitsToString(int [] arr, int size)
        {
            string text = "";
            for(int counter=0; counter <size; counter++)
            {
                text=text+arr[counter];
            }

            return text;
        }

    }
}
