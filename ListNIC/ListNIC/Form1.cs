using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ListNIC
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            VRAPIASM.VRAPIEnv.InitCapture(0);
            short nicCount = VRAPIASM.VRAPIEnv.GetNICCount();
            for (short i = 0; i < nicCount; i++)
            {
                string s = i.ToString() + " : " + VRAPIASM.VRAPIEnv.GetNICName(i) + " - " + VRAPIASM.VRAPIEnv.GetNICDescription(i);
                short ipCount = VRAPIASM.VRAPIEnv.GetNICIPCount(i);

                if (ipCount > 0)
                {
                    s += " - IP:";
                    for (short j = 0; j < ipCount; j++)
                    {
                        string ipAddr = VRAPIASM.VRAPIEnv.GetNICIP(i, j);
                        if(ipAddr != "0.0.0.0")
                            s += ipAddr + ";";
                    }
                }

                listBox1.Items.Add(s);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            VRAPIASM.VRAPIEnv.FreeCapture();
        }
    }
}