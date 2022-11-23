using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2.NESHardware
{
    public struct PPUControl
    {
        public bool NameTableX;
        public bool NameTableY;
        public bool Increment;
        public bool SpriteAddress;
        public bool BackgroundAddress;
        public bool SpriteSize;
        public bool Slave;
        public bool NMI;

        public byte Register
        {
            set
            {
                NameTableX = Convert.ToBoolean(value & 0x01);
                NameTableY = Convert.ToBoolean(value & 0x02);
                Increment = Convert.ToBoolean(value & 0x04);
                SpriteAddress = Convert.ToBoolean(value & 0x08);
                BackgroundAddress = Convert.ToBoolean(value & 0x10);
                SpriteSize = Convert.ToBoolean(value & 0x20);
                Slave = Convert.ToBoolean(value & 0x40);
                NMI = Convert.ToBoolean(value & 0x80);
            }
        }

        public void Clear()
        {
            NameTableX = false;
            NameTableY = false;
            Increment = false;
            SpriteAddress = false;
            BackgroundAddress = false;
            SpriteSize = false;
            Slave = false;
            NMI = false;
        }
    }
}