using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2.NESHardware.Mappers
{
    abstract class Mapper
    {
        protected byte prgBanks;
        protected byte chrBanks;

        protected Mapper(byte prgBanks, byte chrBanks)
        {
            this.prgBanks = prgBanks;
            this.chrBanks = chrBanks;
        }

        public abstract bool CPURead(ushort addr, ref ushort mapped);
        public abstract bool CPUWrite(ushort addr, ref ushort mapped, byte data);
        public abstract bool PPURead(ushort addr, ref ushort mapped);
        public abstract bool PPUWrite(ushort addr, ref ushort mapped);
    }
}
