using System;

namespace WindowsFormsApp2.NESHardware
{
    public struct StatusRegister
    {
        public bool C;
        public bool Z;
        public bool I;
        public bool D;
        public bool B;
        public bool U;
        public bool V;
        public bool N;

        public byte GetRegister()
        {
            byte register = 0x00;
            register |= Convert.ToByte(C);
            register |= (byte) (Convert.ToByte(Z) << 1);
            register |= (byte) (Convert.ToByte(I) << 2);
            register |= (byte) (Convert.ToByte(D) << 3);
            register |= (byte) (Convert.ToByte(B) << 4);
            register |= (byte) (Convert.ToByte(U) << 5);
            register |= (byte) (Convert.ToByte(V) << 6);
            register |= (byte) (Convert.ToByte(N) << 7);
            return register;
        }

        public void SetRegister(byte register)
        {
            C = Convert.ToBoolean(register & 0x01);
            Z = Convert.ToBoolean(register & 0x02);
            I = Convert.ToBoolean(register & 0x04);
            D = Convert.ToBoolean(register & 0x08);
            B = Convert.ToBoolean(register & 0x10);
            U = Convert.ToBoolean(register & 0x20);
            V = Convert.ToBoolean(register & 0x40);
            N = Convert.ToBoolean(register & 0x80);
        }
    }
}
