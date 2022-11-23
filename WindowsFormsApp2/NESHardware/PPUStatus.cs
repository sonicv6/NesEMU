using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2.NESHardware
{
    public struct PPUStatus
    {
        public bool SpriteOverflow;
        public bool SpriteZeroHit;
        public bool VerticalBlank;

        public byte Register
        {
            get
            {
                byte register = 0x00;
                register |= (byte) (Convert.ToByte(SpriteOverflow) << 5);
                register |= (byte) (Convert.ToByte(SpriteZeroHit) << 6);
                register |= (byte) (Convert.ToByte(VerticalBlank) << 7);
                return register;
            }
        }

        public void Clear()
        {
            SpriteOverflow = false;
            SpriteZeroHit = false;
            VerticalBlank = false;
        }
    }
}
