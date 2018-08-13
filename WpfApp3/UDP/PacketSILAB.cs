using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp3.UDP
{
    class PacketSILAB
    {
        private long spid;
        private float rpm;
        private float vel;
        private int gear;
        private float dist;
        private int mode;
        private bool hadActive;
        private bool hadPossible;
        private long targetSpeed;

        private byte[] data = null;

        public long SILABspid
        {
            get { return spid; }
            set { spid = value; }
        }

        public byte[] SILABdata
        {
            get { return data; }
        }

        public float SILABrpm
        {
            get { return rpm; }
            set { rpm = value; }
        }

        public float SILABvel
        {
            get { return vel; }
            set { vel = value; }
        }

        public float SILABdist
        {
            get { return dist; }
            set { dist = value; }
        }

        public int SILABgear
        {
            get { return gear; }
            set { gear = value; }
        }

        public int SILABmode
        {
            get { return mode; }
            set { mode = value; }
        }

        public bool SILABhadActive
        {
            get { return hadActive; }
            set { hadActive = value; }
        }

        public bool SILABhadPossible
        {
            get { return hadPossible; }
            set { hadPossible = value; }
        }

        public long SILABtargetSpeed
        {
            get { return targetSpeed; }
            set { targetSpeed = value; }
        }

        public PacketSILAB()
        {
            this.spid = 0;
            this.rpm = 0;
            this.vel = 0;
            this.gear = 0;
            this.dist = 0;
            this.mode = 0;
            this.hadActive = false;
            this.hadPossible = true;
            this.targetSpeed = 80;
        }

        public PacketSILAB(byte[] byteArray)
        {
            this.data = byteArray;
            this.spid = BitConverter.ToInt32(data, 0);
            this.rpm = BitConverter.ToInt16(data, 5);
            this.vel = (float)BitConverter.ToInt16(data, 7) / 100;
            this.gear = (int)data[9];
            this.mode = (int)data[10];
            Console.WriteLine("HAF_Aktiv: " + data[11] + ", HAF_Aktiv1: " + data[12] + ", Übernahme: " + data[13]);
            if (data[11] > 0 && data[12] > 0)
            {
                this.hadActive = true;
                Console.WriteLine("had: " + hadActive);
            }
            else
            {
                this.hadActive = false;
                Console.WriteLine("had: " + hadActive);
            }
            if (data[13] > 0)
            {
                this.hadPossible = false;
            }
            else
            {
                this.hadPossible = true;
            }
            this.targetSpeed = BitConverter.ToInt16(data, 14);
            this.dist = BitConverter.ToInt32(data, 16);
        }

        public float getSpeedAngle()
        {
            return ((vel * 280 / 120) - 140);
        }

        public float getRPMAngle()
        {
            return ((rpm * 280 / 6000) - 140);
        }

        public string getString()
        {
            object[] args = new object[] { spid, rpm, vel, gear, dist, mode, hadActive, hadPossible };
            return string.Format("spid: {0}, RPM: {1}, vel: {2:0.00}, gear: {3}, dist: {4}, mode: {5}, hadActive: {6}, hasToTakeOver: {7}", args);
            //return GetString(data);
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

        static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        static string GetString(byte[] bytes)
        {
            char[] chars;
            if (bytes.Length > 1)
                chars = new char[bytes.Length / sizeof(char)];
            else
                chars = new char[bytes.Length];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }
    }
}
