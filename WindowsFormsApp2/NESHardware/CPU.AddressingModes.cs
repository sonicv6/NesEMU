namespace WindowsFormsApp2.NESHardware
{
    public partial class CPU
    {
        private void IMP()
        {
            fetched = acc;
            addressMode = AddressMode.IMP;
        }

        private void IMM()
        {
            absAddr = pc++;
            addressMode = AddressMode.IMM;
        }

        private void ZP0()
        {
            absAddr = Read(pc++);
            absAddr &= 0x00FF;
            addressMode = AddressMode.ZP0;
        }

        private void ZPX()
        {
            absAddr = (ushort) (Read(pc++) + x);
            absAddr &= 0x00FF;
            addressMode = AddressMode.ZPX;
        }
        private void ZPY()
        {
            absAddr = (ushort) (Read(pc++) + y);
            absAddr &= 0x00FF;  
            addressMode = AddressMode.ZPY;
        }

        private void ABS()
        {
            ushort lowByte = Read(pc++);
            ushort highByte = Read(pc++);
            absAddr = (ushort) ((highByte << 8) | lowByte);
            addressMode = AddressMode.ABS;
        }
        private void ABX()
        {
            ushort lowByte = Read(pc++);
            ushort highByte = Read(pc++);
            absAddr = (ushort) ((highByte << 8) | lowByte);
            absAddr += x;
            if ((absAddr & 0xFF00) != highByte<<8) cycles++;
            addressMode = AddressMode.ABX;
        }

        private void ABY()
        {
            ushort lowByte = Read(pc++);
            ushort highByte = Read(pc++);
            absAddr = (ushort) ((highByte << 8) | lowByte);
            absAddr += y;
            if ((absAddr & 0xFF00) != highByte<<8) cycles++;
            addressMode = AddressMode.ABY;
        }

        private void IND()
        {
            ushort lowByte = Read(pc++);
            ushort highByte = Read(pc++);
            ushort ptr = (ushort) ((highByte << 8) | lowByte);
            //Emulates long-running error in 6502 processors.
            if (lowByte == 0x00FF) absAddr = (ushort) ((Read((ushort) (ptr & 0xFF00)) << 8) | Read(ptr));
            else absAddr = (ushort) ((Read((ushort) (ptr + 1)) << 8) | Read(ptr)); 
            addressMode = AddressMode.IND;
        }

        private void INX()
        {
            ushort temp = Read(pc++);
            ushort lowByte = Read((ushort) ((temp + x) & 0xFF));
            ushort highByte = Read((ushort) ((temp + x + 1) & 0xFF));
            absAddr = (ushort) ((highByte << 8) | lowByte);
            addressMode = AddressMode.INX;
        }

        private void INY()
        {
            ushort temp = Read(pc++);
            ushort lowByte = Read((ushort) (temp & 0xFF));
            ushort highByte = Read((ushort) ((temp + 1) & 0xFF));
            absAddr = (ushort) ((highByte << 8) | lowByte);
            absAddr += y;
            if ((absAddr & 0xFF00) != highByte << 8) cycles++;
            addressMode = AddressMode.INY;
        }

        private void REL()
        {
            relAddr = Read(pc++);
            if ((relAddr & 0x80) != 0)
            {
                relAddr |= 0xFF00;
            }
            addressMode = AddressMode.REL;
        }
    }
}
