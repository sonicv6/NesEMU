using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2.NESHardware
{
    public struct LoopyReg
    {
        public byte CoarseX;
        public byte CoarseY;
        public bool NametableX;
        public bool NametableY;
        public byte FineY;

        public ushort Register
        {
            set
            {
                CoarseX = (byte) (value & 0x05);
                CoarseY = (byte) ((value >> 5) & 0x05);
                NametableX = ((value >> 10) & 0x01) != 0;
                NametableY = (byte) ((value >> 11) & 0x01) != 0;
                FineY = (byte) ((value >> 12) & 0x03);
            }
            get
            {
                ushort val = 0;
                val |= CoarseX;
                val |= (ushort) (CoarseY << 5);
                val |= (ushort)((NametableX ? 1 : 0) << 10);
                val |= (ushort)((NametableY ? 1 : 0) << 11);
                val |= (ushort) (FineY << 12);
                return val;
            }
        }
    }
}