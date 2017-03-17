using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMManager
{
    class PhysicalMemory
    {
        public Frame[] frames = new Frame[256];
        public Queue<Frame> queuedFrames = new Queue<Frame>();

        public PhysicalMemory()
        {
            for (int i = 0; i < frames.Length; i++)
            {
                Frame frame = new Frame();
                frame.frameNumber = i;
                frames[i] = frame;
            }
        }

        public Frame findNextAvailableFrame()
        {
            Frame newFrame = null;
            for (int i = 0; i < this.frames.Length ; i++)
            {
                if (!frames[i].isAssigned)
                {
                    newFrame = frames[i];
                    queuedFrames.Enqueue(newFrame);
                }
            }
            if (newFrame == null)
            {
                Frame frameToReplace = queuedFrames.Dequeue();
                newFrame = new Frame();
                newFrame.frameNumber=frameToReplace.frameNumber;
                newFrame.isAssigned = false;
                this.frames[newFrame.frameNumber] = newFrame;
            }
            return newFrame;
        }
    }

    
}
