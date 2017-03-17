using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMManager
{
    class PageTable
    {
        public int pageFaults = 0;
        public Page[] pages = new Page[256];


        public bool isPageFault(int pageNumber)
        {
            if(pages[pageNumber]==null)
            {
                this.pageFaults += 1;
                return true;
            }
            return false;
        }
    }
}
