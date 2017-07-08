using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace CSharpVoIPRecorder
{
    public partial class Form1 : Form
    {
        public VRAPIASM.VRAPIEnv.VR_CB_Call_Offered m_pCallOffered;
        public VRAPIASM.VRAPIEnv.VR_CB_Call_Connected m_pCallConnected;
        public VRAPIASM.VRAPIEnv.VR_CB_Call_Idle m_pCallIdle;
        public VRAPIASM.VRAPIEnv.VR_CB_Call_DTMF m_pCallDTMF;
        //public VRAPIASM.VRAPIEnv.VR_CB_Call_Audio_Buffer m_pAudioBuffer;
        public VRAPIASM.VRAPIEnv.VR_CB_Call_Info m_pCallInfo;
        public VRAPIASM.VRAPIEnv.VR_CB_Call_SIP_Info m_pCallSIPInfo;

        int ChanNum;

        public static Form1 main_form;
        public bool Started;

        public int file_num;

        public Form1()
        {
            InitializeComponent();

            ChanNum = 0;

            file_num = 0;

            m_pCallOffered = new VRAPIASM.VRAPIEnv.VR_CB_Call_Offered(cbCallOffered);
            m_pCallConnected = new VRAPIASM.VRAPIEnv.VR_CB_Call_Connected(cbCallConnected);
            m_pCallIdle = new VRAPIASM.VRAPIEnv.VR_CB_Call_Idle(cbCallIdle);
            m_pCallDTMF = new VRAPIASM.VRAPIEnv.VR_CB_Call_DTMF(cbCallDTMF);
            //m_pAudioBuffer = new VRAPIASM.VRAPIEnv.VR_CB_Call_Audio_Buffer(cbAudioBuffer);
            m_pCallInfo = new VRAPIASM.VRAPIEnv.VR_CB_Call_Info(cbCallInfo);
            m_pCallSIPInfo = new VRAPIASM.VRAPIEnv.VR_CB_Call_SIP_Info(cbCallSIPInfo);

            main_form = this;

            Started = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            VRAPIASM.VRAPIEnv.InitCapture(0);

            short nicCount = VRAPIASM.VRAPIEnv.GetNICCount();

            if (nicCount <= 0)
            {
                MessageBox.Show("No Network Interface!");
                btnStartAndStop.Enabled = false;
            }
            else
            {
                for (short i = 0; i < nicCount; i++)
                {
                    string s = i.ToString() + " : " + VRAPIASM.VRAPIEnv.GetNICName(i) + " - " + VRAPIASM.VRAPIEnv.GetNICDescription(i);
                    short ipCount = VRAPIASM.VRAPIEnv.GetNICIPCount(i);
                    if (ipCount > 0)
                        s += " IP:";
                    for (short j = 0; j < ipCount; j++)
                    {
                        if (VRAPIASM.VRAPIEnv.GetNICIP(i, j) != "0.0.0.0")
                        {
                            s += VRAPIASM.VRAPIEnv.GetNICIP(i, j) + ";";
                        }
                    }

                    cbNIC.Items.Add(s);
                }
                cbNIC.SelectedIndex = 0;
            }

            tbChanNum.Text = "4";

            dataGridView1.Enabled = false;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Started)
                Stop();

            VRAPIASM.VRAPIEnv.FreeCapture();
        }

        public int OnCallOffered(int ChanIndex, string CallerIP, string CallerID, string CalleeIP, string CalleeID, uint CallTime, string UniqueID, string AudioFile, int CallDir)
        {
            dataGridView1.Rows[ChanIndex].Cells[0].Value = "Ringing";
            dataGridView1.Rows[ChanIndex].Cells[1].Value = CallerIP;
            dataGridView1.Rows[ChanIndex].Cells[2].Value = CallerID;
            dataGridView1.Rows[ChanIndex].Cells[3].Value = CalleeIP;
            dataGridView1.Rows[ChanIndex].Cells[4].Value = CalleeID;
            dataGridView1.Rows[ChanIndex].Cells[5].Value = UniqueID;
            dataGridView1.Rows[ChanIndex].Cells[6].Value = AudioFile;
            dataGridView1.Rows[ChanIndex].Cells[7].Value = CallDir == 0 ? "Inbound" : "Outbound";

            return 1;
        }


        public static int cbCallOffered(int ChanIndex, string CallerIP, string CallerID, string CalleeIP, string CalleeID, uint CallTime, string UniqueID, string AudioFile, int CallDir)
        {
            return main_form.OnCallOffered(ChanIndex, CallerIP, CallerID, CalleeIP, CalleeID, CallTime, UniqueID, AudioFile, CallDir);
        }

        public void OnCallConnected(int ChanIndex, string CallerIP, string CallerID, string CalleeIP, string CalleeID, uint InitTime, uint ConnectTime, string UniqueID, string AudioFile, int CallDir)
        {
            dataGridView1.Rows[ChanIndex].Cells[0].Value = "Recording";
            dataGridView1.Rows[ChanIndex].Cells[1].Value = CallerIP;
            dataGridView1.Rows[ChanIndex].Cells[2].Value = CallerID;
            dataGridView1.Rows[ChanIndex].Cells[3].Value = CalleeIP;
            dataGridView1.Rows[ChanIndex].Cells[4].Value = CalleeID;
            dataGridView1.Rows[ChanIndex].Cells[5].Value = UniqueID;
            dataGridView1.Rows[ChanIndex].Cells[6].Value = AudioFile;
            dataGridView1.Rows[ChanIndex].Cells[7].Value = CallDir == 0 ? "Inbound" : "Outbound";
        }

        public static void cbCallConnected(int ChanIndex, string CallerIP, string CallerID, string CalleeIP, string CalleeID, uint InitTime, uint ConnectTime, string UniqueID, string AudioFile, int CallDir)
        {
            main_form.OnCallConnected(ChanIndex, CallerIP, CallerID, CalleeIP, CalleeID, InitTime, ConnectTime, UniqueID, AudioFile, CallDir);
        }

        public void OnCallIdle(int ChanIndex, string CallerIP, string CallerID, string CalleeIP, string CalleeID, uint InitTime, uint ConnectTime, uint EndTime, string UniqueID, string AudioFile, int AudioFileNum, int Reason, int CallDir, string DTMFStr)
        {
            dataGridView1.Rows[ChanIndex].Cells[0].Value = "Idle";
            dataGridView1.Rows[ChanIndex].Cells[1].Value = "";
            dataGridView1.Rows[ChanIndex].Cells[2].Value = "";
            dataGridView1.Rows[ChanIndex].Cells[3].Value = "";
            dataGridView1.Rows[ChanIndex].Cells[4].Value = "";
            dataGridView1.Rows[ChanIndex].Cells[5].Value = "";
            dataGridView1.Rows[ChanIndex].Cells[6].Value = "";
            dataGridView1.Rows[ChanIndex].Cells[7].Value = "";
            dataGridView1.Rows[ChanIndex].Cells[8].Value = "";

            //assume wav to mp3 happens here
            //string mp3AudioFile = AudioFile;
            //mp3AudioFile = mp3AudioFile.Replace(".wav", ".mp3");
            //VRAPIASM.VRAPIEnv.WAV2MP3(AudioFile, mp3AudioFile, 1);

        }

        public static void cbCallIdle(int ChanIndex, string CallerIP, string CallerID, string CalleeIP, string CalleeID, uint InitTime, uint ConnectTime, uint EndTime, string UniqueID, string AudioFile, int AudioFileNum, int Reason, int CallDir, string DTMFStr, int codec)
        {
            main_form.OnCallIdle(ChanIndex, CallerIP, CallerID, CalleeIP, CalleeID, InitTime, ConnectTime, EndTime, UniqueID, AudioFile, AudioFileNum, Reason, CallDir, DTMFStr);
        }

        public static void cbCallDTMF(int ChanIndex, string KeyPressed, string DTMFStr)
        {
        }

        public static void cbCallInfo(int ChanIndex, string CallerIP, string CallerID, string CalleeIP, string CalleeID, string UniqueID, string AudioFile, int CallDir)
        {
        }

        public static void cbCallSIPInfo(int ChanIndex, string CallerIP, string CallerID, string CalleeIP, string CalleeID, string UniqueID, string RequestURI, string ContactAddr, string PAssertedIdentity, string PChargingVector, string SIPCallID, string SIPInviteMsg)
        {
        }

        /*
        public static void cbAudioBuffer(int ChanIndex, string UniqueID, int Direction, UIntPtr Buff, int BuffSize)
        {
            short[] localBuffer = new short[BuffSize];
            //IntPtr localBuffer = Marshal.AllocCoTaskMem(320);

            if (Direction == 0) //one side of conversation(audio stream)
            {
                unsafe
                {
                    short* buf_p = (short*)Buff;

                    for (int i = 0; i < BuffSize; i++)
                    {
                        localBuffer[i] = *buf_p++;
                    }

                }

                //operation on localBuffer to save your audio data

            }
            else //another direction of audio
            {
                unsafe
                {
                    short* buf_p = (short*)Buff;

                    for (int i = 0; i < BuffSize; i++)
                    {
                        localBuffer[i] = *buf_p++;
                    }

                }

                //operation on localBuffer to save your audio data

            }
        }*/


        /*
        public static string cbGetWavFileName(int ChanIndex, string uniqueID, string CallerID, string CalleeID)
        {
            return "c:\\temp\\" + ChanIndex.ToString() + "-" + uniqueID;
        }*/

        public void Start()
        {
            VRAPIASM.VRAPIEnv.SetAudioRootFolder(tbRoot.Text);

            //<!-- 0 = default(.wav), 1 = mp3, 2 = gsm -->
            VRAPIASM.VRAPIEnv.SetAudioFileFormat(1);

            //log
            VRAPIASM.VRAPIEnv.SetLogFileName("c:\\temp\\VoIP-Recorder-Log.txt");
            VRAPIASM.VRAPIEnv.SetLogLevel(4);

            //how many channels to open
            VRAPIASM.VRAPIEnv.SetChannelCount(Convert.ToInt32(tbChanNum.Text));

            //SIP, SCCP or SKINNY, RTP, H323, IAX2, UNISTIM, MGCP
            //NOTE: only SIP, SCCP, RTP, and MGCP work so far
            VRAPIASM.VRAPIEnv.SetProtocol("SIP");

            //VRAPIASM.VRAPIEnv.SetProtocol("RTP");
            //VRAPIASM.VRAPIEnv.SetRTPPBXCount(1);
            //VRAPIASM.VRAPIEnv.SetRTPPBXAddr(0, "192.168.1.195");
            //VRAPIASM.VRAPIEnv.SetRTPExtenCount(1);
            //VRAPIASM.VRAPIEnv.SetRTPExten(0, "Mike", "101", "192.168.1.66");

            VRAPIASM.VRAPIEnv.SetNIC(Convert.ToInt16(cbNIC.SelectedIndex));

            VRAPIASM.VRAPIEnv.SetCB_Call_Offered(m_pCallOffered);
            VRAPIASM.VRAPIEnv.SetCB_Call_Connected(m_pCallConnected);
            VRAPIASM.VRAPIEnv.SetCB_Call_Idle(m_pCallIdle);
            VRAPIASM.VRAPIEnv.SetCB_Call_DTMF(m_pCallDTMF);

            VRAPIASM.VRAPIEnv.SetCB_Call_Info(m_pCallInfo);
            VRAPIASM.VRAPIEnv.SetCB_Call_SIP_Info(m_pCallSIPInfo);

            //VRAPIASM.VRAPIEnv.SetCB_GetWAVFileName(new VRAPIASM.VRAPIEnv.VR_CB_GetWavFileName(cbGetWavFileName));

            //license key
            //VRAPIASM.VRAPIEnv.SetLicenseKey("");
            //VRAPIASM.VRAPIEnv.SetLicenseMAC("");

            VRAPIASM.VRAPIEnv.StartCapture();
            /*
            if (VRAPIASM.VRAPIEnv.IsLicensed() > 0)
            {
                MessageBox.Show("Licensed");
            }
            else
            {
                MessageBox.Show("NOT Licensed");
            }*/

            ChanNum = VRAPIASM.VRAPIEnv.GetChannelCount();

            /*
            if (VRAPIASM.VRAPIEnv.IsLicensed() != 0)
            {
                VRAPIASM.VRAPIEnv.Log(4, "Licensed Software");
            }*/

            if (!dataGridView1.Enabled || dataGridView1.RowCount != ChanNum)
            {
                dataGridView1.Enabled = true;

                dataGridView1.RowCount = ChanNum;
                dataGridView1.ColumnCount = 9;

                dataGridView1.Columns[0].HeaderText = "Status";
                dataGridView1.Columns[1].HeaderText = "CallerIP";
                dataGridView1.Columns[2].HeaderText = "CallerID";
                dataGridView1.Columns[3].HeaderText = "CalleeIP";
                dataGridView1.Columns[4].HeaderText = "calleeID";
                dataGridView1.Columns[5].HeaderText = "UniqueID";
                dataGridView1.Columns[6].HeaderText = "AudioFile";
                dataGridView1.Columns[7].HeaderText = "Direction";
                dataGridView1.Columns[8].HeaderText = "DTMF";

                //dataGridView1.Rows[0].HeaderCell.OwningColumn.Width = 200;

                for (int i = 0; i < ChanNum; i++)
                {
                    int index = i + 1;
                    dataGridView1.Rows[i].HeaderCell.Value = index.ToString();
                    dataGridView1.Rows[i].Cells[0].Value = "Idle";
                }
            }

            Started = true;
            btnStartAndStop.Text = "Stop";
        }

        public void Stop()
        {
            dataGridView1.Enabled = false;

            VRAPIASM.VRAPIEnv.StopCapture();

            //reset recorder API just in case
            VRAPIASM.VRAPIEnv.FreeCapture();
            VRAPIASM.VRAPIEnv.InitCapture(0);

            Started = false;
            btnStartAndStop.Text = "Start";
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                tbRoot.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void btnStartAndStop_Click(object sender, EventArgs e)
        {
            if (!Started)
                Start();
            else
                Stop();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }







    }
}