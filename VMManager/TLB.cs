using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMManager
{
    class TLB
    {
        public List<Page> pages = new List <Page>();

        public void AddPage( Page page)
        {
            if (pages.Count==16)
            {
                pages.Remove(pages.First());
                pages.Add(page);
            }else
            {
                pages.Add(page);
            }
                
        }

        public  Frame  FindPage(int pageNumber)
        {
            foreach (var item in this.pages)
            {
                if (item.pageNumber == pageNumber)
                {
                    return item.frame;
                }
            }
            return null;
        }

    }
}
