using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2.NESHardware
{
    public struct PPUMask
    {
        public bool Greyscale;
        public bool BackgroundLeft;
        public bool SpritesLeft;
        public bool Background;
        public bool Sprites;
        public bool EmphasizeRed;
        public bool EmphasizeGreen;
        public bool EmphasizeBlue;

        public byte Register
        {
            set
            {
                Greyscale = Convert.ToBoolean(value & 0x01);
                BackgroundLeft = Convert.ToBoolean(value & 0x02);
                SpritesLeft = Convert.ToBoolean(value & 0x04);
                Background = Convert.ToBoolean(value & 0x08);
                Sprites = Convert.ToBoolean(value & 0x10);
                EmphasizeRed = Convert.ToBoolean(value & 0x20);
                EmphasizeGreen = Convert.ToBoolean(value & 0x40);
                EmphasizeBlue = Convert.ToBoolean(value & 0x80);
            }
        }

        public void Clear()
        {
            Greyscale = false;
            BackgroundLeft = false;
            SpritesLeft = false;
            Background = false;
            Sprites = false;
            EmphasizeRed = false;
            EmphasizeGreen = false;
            EmphasizeBlue = false;
        }
    }
}