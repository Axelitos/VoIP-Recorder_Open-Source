using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MiniVoIPRecorder
{
    public partial class FormOptions : Form
    {
        public MiniVRCfg cfg;

        public FormOptions()
        {
            InitializeComponent();
            cfg = null;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;

            cfg.rootFolder = tbRootFolder.Text;

            cfg.Filters = tbFilters.Text;
            cfg.Excludes= tbExcludes.Text;

            cbProtocol.Items.Add("SIP");
            cbProtocol.Items.Add("SCCP or SKINNY");
            cbProtocol.Items.Add("SCCP_CCM7 or SKINNY_CCM7");
            cbProtocol.Items.Add("RTP");
            cbProtocol.Items.Add("H323");
            cbProtocol.Items.Add("IAX2");
            cbProtocol.Items.Add("MGCP");
            cbProtocol.Items.Add("UNISTIM");

            if (cbProtocol.Text == "SCCP_CCM7 or SKINNY_CCM7")
            {
                cfg.Protocol = "SCCP_CCM7";
            }
            else if (cbProtocol.Text == "SCCP or SKINNY")
            {
                cfg.Protocol = "SCCP";
            }
            else
                cfg.Protocol = cbProtocol.Text;

            if (cfg.Protocol == "RTP")
            {
                cfg.RTPPBXAddr = tbRTPPBXAddr.Text;
            }
            else
            {
                cfg.RTPPBXAddr = "";
            }

            cfg.ignoreSameCall = cbIgnorePossibleSameCall.Checked?1:0;

            cfg.recordingCall = cbRecording.Checked?1:0;

            cfg.usePacketTime = cbUsePacketTime.Enabled?1:0;

            try
            {
                cfg.noAudioSeconds = Convert.ToInt32(tbNoAudioSeconds.Text);
            }
            catch (Exception)
            {
                cfg.noAudioSeconds = 0;
            }


            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void FormOptions_Load(object sender, EventArgs e)
        {
            tbRootFolder.Text = cfg.rootFolder;

            rbWav.Checked = true;
            rbMP3.Enabled = false;
            rbGSM.Enabled = false;

            tbFilters.Text = cfg.Filters;
            tbExcludes.Text = cfg.Excludes;

            tbChanNum.Text = "1";
            tbChanNum.Enabled = false;

            cbProtocol.Items.Add("SIP");
            cbProtocol.Items.Add("SCCP or SKINNY");
            cbProtocol.Items.Add("SCCP_CCM7 or SKINNY_CCM7");
            cbProtocol.Items.Add("RTP");
            cbProtocol.Items.Add("H323");
            cbProtocol.Items.Add("IAX2");
            cbProtocol.Items.Add("MGCP");
            cbProtocol.Items.Add("UNISTIM");

            if (cfg.Protocol.Contains("SCCP_CCM7") || cfg.Protocol.Contains("SKINNY_CCM7"))
            {
                cbProtocol.Text = "SCCP_CCM7 or SKINNY_CCM7";
            }
            else if (cfg.Protocol.Contains("SCCP") || cfg.Protocol.Contains("SKINNY"))
            {
                cbProtocol.Text = "SCCP or SKINNY";
            }
            else
                cbProtocol.Text = cfg.Protocol;

            if (cfg.Protocol == "RTP")
            {
                lbRTPPBXAddr.Visible = true;
                tbRTPPBXAddr.Visible = true;
                tbRTPPBXAddr.Text = cfg.RTPPBXAddr;
            }
            else
            {
                lbRTPPBXAddr.Visible = false;
                tbRTPPBXAddr.Visible = false;
            }

            cbIgnorePossibleSameCall.Checked = cfg.ignoreSameCall == 1;

            cbRecording.Checked = cfg.recordingCall == 1;

            cbUsePacketTime.Enabled = cfg.usePacketTime == 1;

            tbNoAudioSeconds.Text = cfg.noAudioSeconds.ToString();
        }

        private void btnRootFolderBrowse_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                tbRootFolder.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void cbProtocol_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbProtocol.Text == "RTP")
            {
                lbRTPPBXAddr.Visible = true;
                tbRTPPBXAddr.Visible = true;
                tbRTPPBXAddr.Text = cfg.RTPPBXAddr;
            }
            else
            {
                lbRTPPBXAddr.Visible = false;
                tbRTPPBXAddr.Visible = false;
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Format: FilterType;Filter1;Filter2;...\nFilterType 0:IP Address, 1:CallID, 2:MAC Address\nSample: 0;192.168.1.10;192.168.1.21\nSample: 1;16135552324;17042223333\nCallID supports wild cards. * means any string. ? means any one character. Sample: 1;1613*;1704*\nSample: 2;00-23-AE-99-3C-14;00-15-5D-01-2E-17");
        }

        private void label9_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Format: FilterType;Filter1;Filter2;...\nFilterType 0:IP Address, 1:CallID, 2:MAC Address\nSample: 0;192.168.1.10;192.168.1.21\nSample: 1;16135552324;17042223333\nCallID supports wild cards. * means any string. ? means any one character. Sample: 1;1613*;1704*\nSample: 2;00-23-AE-99-3C-14;00-15-5D-01-2E-17");
        }
    }
}