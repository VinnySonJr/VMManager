using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMManager
{
    class Frame
    {
        public int frameNumber { get; set; }
        public Boolean isAssigned = false;
        public SByte[] frameBytes = new SByte[256];
    }
}
