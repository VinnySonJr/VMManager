using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMManager
{
    class FileReader
    {
        public  SByte [] readValue(int basePosition)
        {
            string fileName = @"C:\Users\Augustin Nshimiyiman\Google Drive\OCClasses\Spring 2017\Operating Systems\Assignment\Virtual Memory\BACKING_STORE.bin";

            using (System.IO.BinaryReader reader = new BinaryReader(File.Open(fileName, FileMode.Open)))
            {
                reader.BaseStream.Position = basePosition;
                SByte[] bytes = new SByte[256];
                for (int i = 0; i < 256; i++)
                {
                    bytes[i] = reader.ReadSByte();
                }
                return bytes;
            }
        }

        public  List<int> readAddresses()
        {
            List<int> addresses = new List<int>();
            string[] lines = File.ReadAllLines(@"C:\Users\Augustin Nshimiyiman\Google Drive\OCClasses\Spring 2017\Operating Systems\Assignment\Virtual Memory\addresses.txt");

            foreach (var line in lines)
            {
                addresses.Add(int.Parse(line));
            }

            return addresses;
        }
    }
}
