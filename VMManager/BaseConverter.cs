using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMManager
{
    class BaseConverter
    {
        public Stack<int> DecToBin(int decimalAddress, int length)
        {
            Stack<int> stackOfBinaries = new Stack<int>();

            while (decimalAddress >= 2)
            {
                int remainder = decimalAddress % 2;
                stackOfBinaries.Push(remainder);
                decimalAddress = (decimalAddress - remainder) / 2;
            }
            if (decimalAddress != 0)
                stackOfBinaries.Push(1);
            else
                stackOfBinaries.Push(0);

            while (length - stackOfBinaries.Count > 0)
            {
                stackOfBinaries.Push(0);
            }

            return stackOfBinaries;
        }

        public int BinToDec(string binaryString)
        {
            int decimalNumber = Convert.ToInt32(binaryString,2);

            return decimalNumber;
        }

        public string bitsToString(int[] arr, int size)
        {
            string text = "";
            for (int counter = 0; counter < size; counter++)
            {
                text = text + arr[counter];
            }

            return text;
        }
    }
}
