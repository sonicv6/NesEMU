using System;

namespace WindowsFormsApp2.NESHardware
{
    public partial class CPU
    {
        //Helper methods
        private void Fetch()
        {
            fetched = Read(absAddr);
        }
        private void SetZFlag(byte data)
        {
            status.Z = data == 0;
        }

        private void SetNFlag(byte data)
        {
            status.N = (data & 0x80) == 0x80;
        }

        private void SetCFlag(byte data)
        {
            status.C = (data & 0x80) == 0x80;
        }

        private void GetRel()
        {
            absAddr = (ushort) (pc + relAddr);
        }

        private void CheckPage()
        {
            if ((absAddr & 0xFF00) != 0xFF00) cycles++;
        }
        //CPU instructions
        private void ADC()
        {
            Fetch();
            byte result = (byte) (acc + fetched + Convert.ToInt32(status.C));
            SetNFlag(result);
            SetZFlag(result);
            if ((fetched & acc) != (result & 0x80))
            {
                status.V = true;
                status.C = true;
            }
            else
            {
                status.V = false;
                status.C = false;
            }

            acc = result;
        }
        private void AND()
        {
            Fetch();
            acc &= fetched;
            SetZFlag(acc);
            SetNFlag(acc);
        }
        private void ASL()
        {
            if (addressMode != AddressMode.IMP) Fetch();
            byte result = (byte) (fetched << 1);
            SetCFlag(fetched);
            SetZFlag(result);
            SetNFlag(result);
            if (addressMode == AddressMode.IMP) acc = result;
            else Write(absAddr, result);
        }
        private void BCC()
        {
            Fetch();
            if (!status.C)
            {
                cycles++;
                GetRel();
                CheckPage();
                pc = absAddr;
            }
        }

        private void BCS()
        {
            Fetch();
            if (status.C)
            {
                cycles++;
                GetRel();
                CheckPage();
                pc = absAddr;
            }
        }

        private void BEQ()
        {
            Fetch();
            if (status.Z)
            {
                cycles++;
                GetRel();
                CheckPage();
                pc = absAddr;
            }
        }

        private void BIT()
        {
            Fetch();
            status.Z = (acc & fetched) == 0;
            status.V = (fetched & 0x40) == 0x40;
            SetNFlag(fetched);
        }

        private void BMI()
        {
            Fetch();
            if (status.N)
            {
                cycles++;
                GetRel();
                CheckPage();
                pc = absAddr;
            }
        }

        private void BNE()
        {
            Fetch();
            if (!status.Z)
            {
                cycles++;
                GetRel();
                CheckPage();
                pc = absAddr;
            }
        }

        private void BPL()
        {
            Fetch();
            if (!status.N)
            {
                cycles++;
                GetRel();
                CheckPage();
                pc = absAddr;
            }
        }

        private void BRK()
        {
            Write((ushort) (0x100 + pointer--), (byte) (pc>>8));
            Write((ushort) (0x100 + pointer--), (byte) (pc));
            Write((ushort) (0x100 + pointer--), status.GetRegister());
            pc = (ushort) (Read(0xFFFE) | (Read(0xFFFF) << 8));
            status.B = true;
        }

        private void BVC()
        {
            Fetch();
            if (!status.V)
            {
                cycles++;
                GetRel();
                CheckPage();
                pc = absAddr;
            }
        }

        private void BVS()
        {
            Fetch();
            if (status.V)
            {
                cycles++;
                GetRel();
                CheckPage();
                pc = absAddr;
            }
        }

        private void CLC()
        {
            status.C = false;
        }

        private void CLD()
        {
            status.D = false;
        }

        private void CLI()
        {
            status.I = false;
        }

        private void CLV()
        {
            status.V = false;
        }

        private void CMP()
        {
            Fetch();
            byte compared = (byte) (acc - fetched);
            status.C = acc > fetched;
            SetZFlag(compared);
            SetNFlag(compared);
        }

        private void CPX()
        {
            Fetch();
            byte compared = (byte) (x - fetched);
            status.C = x > fetched;
            SetZFlag(compared);
            SetNFlag(compared);
        }

        private void CPY()
        {
            Fetch();
            byte compared = (byte) (y - fetched);
            status.C = y > fetched;
            SetZFlag(compared);
            SetNFlag(compared);
        }

        private void DEC()
        {
            Fetch();
            byte result = (byte) (fetched - 1);
            Write(absAddr, result);
            SetZFlag(result);
            SetNFlag(result);
        }

        private void DEX()
        {
            byte result = (byte) (x - 1);
            x = result;
            SetZFlag(result);
            SetNFlag(result);
        }

        private void DEY()
        {
            byte result = (byte) (y - 1);
            y = result;
            SetZFlag(result);
            SetNFlag(result);
        }

        private void EOR()
        {
            Fetch();
            acc = (byte) (acc ^ fetched);
            SetNFlag(acc);
            SetZFlag(acc);
        }

        private void INC()
        {
            Fetch();
            byte result = (byte) (fetched + 1);
            Write(absAddr, result);
            SetZFlag(result);
            SetNFlag(result);
        }

        private void INCX()
        {
            byte result = (byte) (x + 1);
            x = result;
            SetZFlag(result);
            SetNFlag(result);
        }

        private void INCY()
        {
            byte result = (byte) (y + 1);
            y = result;
            SetZFlag(result);
            SetNFlag(result);
        }

        private void JMP()
        {
            pc = absAddr;
        }

        private void JSR()
        {
            pc--;
            Write((ushort) (0x100 + pointer--), (byte) (pc>>8));
            Write((ushort) (0x100 + pointer--), (byte) (pc));
            pc = absAddr;
        }

        private void LDA()
        {
            Fetch();
            acc = fetched;
            SetZFlag(acc);
            SetNFlag(acc);
        }

        private void LDX()
        {
            Fetch();
            x = fetched;
            SetZFlag(x);
            SetNFlag(x);
        }

        private void LDY()
        {
            Fetch();
            y = fetched;
            SetZFlag(y);
            SetNFlag(y);
        }

        private void LSR()
        {
            if (addressMode != AddressMode.IMP) Fetch();
            byte result = (byte) (fetched >> 1);
            SetCFlag(fetched);
            SetZFlag(result);
            SetNFlag(result);
            if (addressMode == AddressMode.IMP) acc = result;
            else Write(absAddr, result);
        }

        private void NOP()
        {
        }

        private void ORA()
        {
            Fetch();
            acc = (byte) (acc | fetched);
            SetZFlag(acc);
            SetNFlag(acc);
        }

        private void PHA()
        {
            Write((ushort) (0x100 + pointer--), acc);
        }

        private void PHP()
        {
            var s = status.GetRegister();
            Write((ushort) (0x100 + pointer--), status.GetRegister());
        }

        private void PLA()
        {
            acc = Read((ushort) (0x100 + ++pointer));
            SetNFlag(acc);
            SetZFlag(acc);
        }

        private void PLP()
        {
            status.SetRegister(Read((ushort) (0x100 + ++pointer)));
        }

        private void ROL()
        {
            if (addressMode != AddressMode.IMP) Fetch();
            byte result = (byte) (fetched << 1);
            result += (byte) System.Convert.ToInt32(status.C);
            status.C = (fetched & 0x80) == 0x80;
            if (addressMode == AddressMode.IMP) acc = result;
            else Write(absAddr, result);
        }

        private void ROR()
        {
            if (addressMode != AddressMode.IMP) Fetch();
            byte result = (byte) (fetched  >> 1);
            result =  (byte) (result | (System.Convert.ToInt32(status.C)<<7));
            status.C = (fetched & 0x01) == 0x01;
            if (addressMode == AddressMode.IMP) acc = result;
            else Write(absAddr, result);
        }

        private void RTI()
        {
            status.SetRegister(Read((ushort) (0x100 + ++pointer)));
            pc = (ushort) (Read((ushort) (0x100 + ++pointer)) | (Read((ushort) (0x100 + ++pointer)) << 8));
        }

        private void RTS()
        {
            pc = (ushort) (Read((ushort) (0x100 + ++pointer)) | (Read((ushort) (0x100 + ++pointer)) << 8));
            pc++;
        }

        private void SBC()
        {
            Fetch();
            var val = fetched ^ 0xFF;
            byte result = (byte) (acc + fetched + Convert.ToInt32(status.C));
            SetNFlag(result);
            SetZFlag(result);
            if ((fetched & acc) != (result & 0x80))
            {
                status.V = true;
                status.C = true;
            }
            else
            {
                status.V = false;
                status.C = false;
            }

            acc = result;
        }

        private void SEC()
        {
            status.C = true;
        }

        private void SED()
        {
            status.D = true;
        }

        private void SEI()
        {
            status.I = true;
        }

        private void STA()
        {
            Write(absAddr, acc);
        }

        private void STX()
        {
            Write(absAddr, x);
        }

        private void STY()
        {
            Write(absAddr, y);
        }

        private void TAX()
        {
            x = acc;
            SetZFlag(x);
            SetNFlag(x);
        }

        private void TAY()
        {
            y = acc;
            SetZFlag(y);
            SetNFlag(y);
        }

        private void TSX()
        {
            x = pointer;
            SetZFlag(x);
            SetNFlag(x);
        }

        private void TXA()
        {
            acc = x;
            SetZFlag(acc);
            SetNFlag(acc);
        }

        private void TXS()
        {
            pointer = x;
        }

        private void TYA()
        {
            acc = y;
            SetZFlag(acc);
            SetNFlag(acc);
        }
    }
}
