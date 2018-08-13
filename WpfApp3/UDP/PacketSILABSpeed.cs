using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp3.UDP
{
    class PacketSILABSpeed
    {
        private float speed;
        private byte[] data;

        public float SILABSpeedSpeed
        {
            get { return speed; }
            set { speed = value; }
        }
        public byte[] SILABSpeeddata
        {
            get { return data;}
            set { data = value; }
        }

        public PacketSILABSpeed()
        {
            this.speed = 0;
            
        }
        public PacketSILABSpeed(byte[] byteArray)
        {
            this.data = byteArray;
            this.speed = (float)BitConverter.ToInt16(data, 7) / 100; // startindex "7" und "/100" übernommen. SILAB Nachricht prüfen!
        }

        public string getBytes()
        {
            string answer = "";
            foreach (byte b in data)
            {
                answer += string.Format("{0},", b);
            }
            return answer;
        }
        public float getSpeed()
        {
            return this.speed;
        }
    }
}
