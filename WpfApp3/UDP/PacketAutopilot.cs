using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp3.UDP
{
    class PacketAutopilot
    {
        private byte[] data;

        public byte[] Data
        {
            get { return data; }
        }

        public PacketAutopilot(byte[] byteArray)
        {
            data = byteArray;
            

            
        }
    }
}
