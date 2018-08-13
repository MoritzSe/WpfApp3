using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp3.UDP
{
    class PacketMenueNavigation
    {
        private byte [] data;

        public byte [] MenueNavigationData
        {
            get { return data; }
            set { data = value; }
        }

        public byte [] Data
        {
            get { return data; }
        }

        public PacketMenueNavigation(byte[] byteArray)
        {
            this.data = byteArray;
            if (data[1] == 1)
            {
                Data[1] = 1;
            }
            if (data[1] == 2)
            {
                Data[1] = 2;
            }

        }

    }
}
