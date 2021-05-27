using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NesHardware;
using Emulator = WindowsFormsApp2.NESHardware.Emulator;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        private Emulator emu = new Emulator();
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            emu.cpu.Cycle();
            label10.Text = emu.cpu.acc.ToString();
            label11.Text = emu.cpu.x.ToString();
            label12.Text = emu.cpu.y.ToString();
            label13.Text = emu.cpu.status.C.ToString();
            label14.Text = emu.cpu.status.Z.ToString();
            label15.Text = emu.cpu.status.I.ToString();
            label16.Text = emu.cpu.status.D.ToString();
            label17.Text = emu.cpu.status.B.ToString();
            label18.Text = emu.cpu.status.U.ToString();
            label19.Text = emu.cpu.status.V.ToString();
            label20.Text = emu.cpu.status.N.ToString();
            label22.Text = emu.cpu.pc.ToString("X4");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
