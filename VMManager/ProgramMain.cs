using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace VMManager
{
    class ProgramMain
    {
        static void Main(string[] args)
        {
            List<int> addresses = new List<int>();
            Stack<int> stackOfBinaries = new Stack<int>();
            FileReader fileReader = new FileReader();
            BaseConverter baseConverter = new BaseConverter();
            PageTable pageTable = new PageTable();
            TLB tlb = new TLB();
            PhysicalMemory memory = new PhysicalMemory();

            int[] pageNumber = new int[8];
            int[] offset = new int[8];
            string logicalAddressBin;
            string physicalAddressBin="";
            int physicalAddressDec=0;
            string offsetBin = "";
            string frameNberBin = "";
            int offsetDec = 0;
            int frameNumberDec = 0;
            int pageNber=0;

            int TLBhits=0;
            int addressReferences = 0;

            addresses = fileReader.readAddresses();

            //show the logical addresses
            Console.WriteLine("Showing the content of the logical address text file");
            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine("Address        Binary        PageNumber    Offset     Physical Address Bin    Physical Address Dec    Value");
            Console.WriteLine("-------        ------        ----------    ------     --------------------    --------------------    -----");
            foreach (var address in addresses)
            {
                //Extract the page number and the offset from the logical address
                stackOfBinaries = baseConverter.DecToBin(address,16);
                logicalAddressBin = baseConverter.bitsToString(stackOfBinaries.ToArray(),16);

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

                // search the TLB for this page               
                pageNber = baseConverter.BinToDec(baseConverter.bitsToString(pageNumber, 8));
                Frame frame = tlb.FindPage(pageNber);

                if (frame == null)//If the page is not in the TLB, search the page table
                {
                    
                    //check if the page number results into a page fault, is so, assign the page the next available frame
                    Boolean isPageFault = pageTable.isPageFault(pageNber);

                    if (isPageFault)
                    {
                        //Add the new page in the page table
                        Page page = new Page();
                        page.pageNumber = pageNber;
                        pageTable.pages[pageNber] = page;

                        //Find a free frame in the physical memory, for this page
                        pageTable.pages[pageNber].frame = memory.findNextAvailableFrame();
                        frameNumberDec = pageTable.pages[pageNber].frame.frameNumber;

                        //put 256 bytes from the binary file into the allocated frame
                        memory.frames[frameNumberDec].frameBytes = fileReader.readValue(pageNber * 256);

                        //set the frame to already assigned
                        memory.frames[frameNumberDec].isAssigned = true;

                        //Add this page to TLB
                        tlb.AddPage(pageTable.pages[pageNber]);
                    }else
                    {
                      //Add this page to TLB
                        tlb.AddPage(pageTable.pages[pageNber]);
                    }
                }
                else// otherwise, the page is in the tlb
                {
                    TLBhits += 1;
                }

                //get the frame number
                frameNumberDec = pageTable.pages[pageNber].frame.frameNumber;

                //Convert the decimal frame number to binary
                frameNberBin = baseConverter.bitsToString(baseConverter.DecToBin(frameNumberDec, 8).ToArray(), 8);

                //Get the binary string of the offset
                offsetBin = baseConverter.bitsToString(offset, 8);

                // concatenate the frame number and the offset to get the physical 
                //address
                physicalAddressBin = frameNberBin + offsetBin;

                //calculate the decimal representation of the physical address
                physicalAddressDec = baseConverter.BinToDec(physicalAddressBin);
                offsetDec = baseConverter.BinToDec(offsetBin);

                int value = memory.frames[frameNumberDec].frameBytes[offsetDec];
                addressReferences += 1;

                Console.WriteLine(address + "    " + baseConverter.bitsToString(stackOfBinaries.ToArray(), 16)
                    + "    " + baseConverter.bitsToString(pageNumber,8) + "    " + baseConverter.bitsToString(offset, 8)+"    "+physicalAddressBin+"                "+physicalAddressDec+ "                   "+value);
            }
            Console.WriteLine();
            Console.WriteLine("TLB hits: " + TLBhits);
            Console.WriteLine("Page faults: " + pageTable.pageFaults);
            Console.WriteLine("Total Number of address references: " + addressReferences);
            Console.WriteLine("Page-fault rate: "+((float)pageTable.pageFaults/addressReferences)*100+"%");
            Console.WriteLine("TLB hit rate: " +((float)TLBhits / addressReferences)*100+"%");
        }
    }
}
