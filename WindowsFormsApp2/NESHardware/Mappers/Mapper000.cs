using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2.NESHardware.Mappers
{
    class Mapper000 : Mapper
    {
        public Mapper000(byte prgBanks, byte chrBanks) : base(prgBanks, chrBanks)
        {

        }

        public override bool CPURead(ushort addr, ref ushort mapped)
        {
            if (addr >= 0x8000 && addr <= 0xFFFF)
            {
                mapped = (ushort) (addr & (prgBanks > 1 ? 0x7FFF : 0x3FFF));
                return true;
            }

            return false;
        }

        public override bool CPUWrite(ushort addr, ref ushort mapped, byte data)
        {
            if (addr >= 0x8000 && addr <= 0xFFFF)
            {
                if (prgBanks == 1)
                {
                    mapped = (ushort) (addr & 0x3FFF);
                    return true;
                }

                mapped = (ushort) (addr & 0x7FFF);
            }

            return false;
        }

        public override bool PPURead(ushort addr, ref ushort mapped)
        {
            if (addr >= 0 && addr <= 0x1FFF)
            {
                mapped = addr;
                return true;
            }
            return false;
        }

        public override bool PPUWrite(ushort addr, ref ushort mapped)
        {
            if (addr >= 0x0000 && addr <= 0x1FFF)
            {
                if (chrBanks == 0)
                {
                    // Treat as RAM
                    mapped = addr;
                    return true;
                }
            }

            return false;
        }
    }
}
