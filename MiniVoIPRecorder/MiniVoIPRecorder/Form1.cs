using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;
using VRAPIASM;

namespace MiniVoIPRecorder
{


    public partial class Form1 : Form
    {
        public bool _bExit;

        public const string MINIVR_REG_KEY_ROOT = "SOFTWARE\\PC Best Networks Inc\\Mini VoIP Recorder\\";

        public VRAPIASM.VRAPIEnv.VR_CB_Call_Offered m_pCallOffered;
        public VRAPIASM.VRAPIEnv.VR_CB_Call_Connected m_pCallConnected;
        public VRAPIASM.VRAPIEnv.VR_CB_Call_Idle m_pCallIdle;
        public VRAPIASM.VRAPIEnv.VR_CB_Call_DTMF m_pCallDTMF;
        //public VRAPIASM.VRAPIEnv.VR_CB_Call_Audio_Buffer m_pAudioBuffer;
        public VRAPIASM.VRAPIEnv.VR_CB_Call_Info m_pCallInfo;
        public VRAPIASM.VRAPIEnv.VR_CB_Call_SIP_Info m_pCallSIPInfo;

        public VRAPIASM.VRAPIEnv.VR_CB_GetWavFileName m_pGetWavFileName;
        public VRAPIASM.VRAPIEnv.VR_CB_GetXMLFileName m_pGetXMLFileName;

        int ChanNum;

        public static Form1 main_form;
        public bool Started;
        public bool Inited;

        public Queue<VR2Event> event_queue;

        public MiniVRCfg cfg;

        public string lic_key;

        public Form1()
        {
            InitializeComponent();
            _bExit = false;

            ChanNum = 1;

            main_form = this;

            Started = false;
            Inited = false;

            event_queue = new Queue<VR2Event>();

            cfg = new MiniVRCfg();

            lic_key = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            notifyIcon1.ContextMenuStrip = contextMenuStrip1;

            Directory.CreateDirectory(cfg.rootFolder);

            tbCaller.Enabled = false;
            tbCallee.Enabled = false;
            tbFileName.Enabled = false;

            string sSysPath = Environment.SystemDirectory;

            if (!File.Exists(sSysPath + "\\packet.dll") || !File.Exists(sSysPath + "\\wpcap.dll"))
            {

                string message = "WinPcap is not installed! Please download it from http://www.winpcap.org/ and install it first.";
                string caption = "Error Detected - Missing WinPcap Driver";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;

                // Displays the MessageBox.
                result = MessageBox.Show(message, caption, buttons);

                _bExit = true;
                Close();      
            }

            LoadConfigFromRegistry();

            InitCapture();

            startRecordingToolStripMenuItem.Enabled = true;
            stopRecordingToolStripMenuItem.Enabled = false;

            recordingToolStripMenuItem.Text = "Status: Recording Stopped";
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_bExit)
            {
                e.Cancel = true;
                Hide();
                this.ShowInTaskbar = false;

                notifyIcon1.Visible = true;
                notifyIcon1.BalloonTipTitle = "Mini VoIP Recorder";
                notifyIcon1.BalloonTipText = "minimized here! double-click to show up.";
                notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
                notifyIcon1.ShowBalloonTip(500);
            }
            else
            {
                Stop();

                FreeCapture();
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == this.WindowState)
            {
                notifyIcon1.BalloonTipTitle = "Mini VoIP Recorder";
                notifyIcon1.BalloonTipText = "minimized here! double-click to show up.";
                notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;

                notifyIcon1.Visible = true;
                notifyIcon1.ShowBalloonTip(500);
                this.Hide();
                this.ShowInTaskbar = false;
            }

            else if (FormWindowState.Normal == this.WindowState)
            {
                notifyIcon1.Visible = false;
                this.ShowInTaskbar = true;
            }

        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;

            //this.ShowInTaskbar = true;
            //notifyIcon1.Visible = false;

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            _bExit = true;
            Close();
        }

        public void LoadConfigFromRegistry()
        {
            RegistryKey regkey;
            //string tmp;
            regkey = Registry.CurrentUser.CreateSubKey(MINIVR_REG_KEY_ROOT);
            if (regkey == null)
                return;

            Object obj = regkey.GetValue("Protocol");
            if (obj != null)
                cfg.Protocol = (string)obj;
            else
                cfg.Protocol = "SIP";

            obj = regkey.GetValue("RootFolder");
            if (obj != null)
                cfg.rootFolder = (string)obj;
            else
                cfg.rootFolder = Application.StartupPath + "\\Audio";

            obj = regkey.GetValue("Filters");
            if (obj != null)
                cfg.Filters = (string)obj;
            else
                cfg.Filters = "";

            obj = regkey.GetValue("Excludes");
            if (obj != null)
                cfg.Excludes = (string)obj;
            else
                cfg.Excludes = "";

            obj = regkey.GetValue("ChannelNumber");
            if (obj != null)
                cfg.ChanNum = (int)obj;
            else
                cfg.ChanNum = 1;

            obj = regkey.GetValue("AudioFormat");
            if (obj != null)
                cfg.AudioFormat = (int)obj;
            else
                cfg.AudioFormat = 0;

            obj = regkey.GetValue("RTPPBXAddr");
            if (obj != null)
                cfg.RTPPBXAddr = (string)obj;
            else
                cfg.RTPPBXAddr = "";

            obj = regkey.GetValue("IgnoreSameCall");
            if (obj != null)
                cfg.ignoreSameCall = (int)obj;
            else
                cfg.ignoreSameCall = 0;

            obj = regkey.GetValue("RecordingCall");
            if (obj != null)
                cfg.recordingCall = (int)obj;
            else
                cfg.recordingCall = 1;

            obj = regkey.GetValue("UsePacketTime");
            if (obj != null)
                cfg.usePacketTime = (int)obj;
            else
                cfg.usePacketTime = 1;

            obj = regkey.GetValue("NoAudioSeconds");
            if (obj != null)
                cfg.noAudioSeconds = (int)obj;
            else
                cfg.noAudioSeconds = 1;

            obj = regkey.GetValue("NICIdx");
            if (obj != null)
                cfg.NICIdx = (int)obj;
            else
                cfg.NICIdx = 1;

            regkey.Close();
        }

        public void SaveConfigToRegsitry()
        {
            RegistryKey regkey;
            //string tmp;
            regkey = Registry.CurrentUser.CreateSubKey(MINIVR_REG_KEY_ROOT);
            if (regkey == null)
                return;

            regkey.SetValue("Protocol", cfg.Protocol);
            regkey.SetValue("RootFolder", cfg.rootFolder);
            regkey.SetValue("Filters", cfg.Filters);
            regkey.SetValue("Excludes", cfg.Excludes);
            regkey.SetValue("ChannelNumber", cfg.ChanNum);
            regkey.SetValue("AudioFormat", cfg.AudioFormat);
            regkey.SetValue("RTPPBXAddr", cfg.RTPPBXAddr);
            regkey.SetValue("IgnoreSameCall", cfg.ignoreSameCall);
            regkey.SetValue("RecordingCall", cfg.recordingCall);
            regkey.SetValue("UsePacketTime", cfg.usePacketTime);
            regkey.SetValue("NoAudioSeconds", cfg.noAudioSeconds);
            regkey.SetValue("NICIdx", cfg.NICIdx);

            regkey.Close();
        }

        public void InitCapture()
        {
            if (!Inited)
            {

                VRAPIASM.VRAPIEnv.InitCapture(0);

                Inited = true;

                m_pCallOffered = new VRAPIASM.VRAPIEnv.VR_CB_Call_Offered(cbCallOffered);
                m_pCallConnected = new VRAPIASM.VRAPIEnv.VR_CB_Call_Connected(cbCallConnected);
                m_pCallIdle = new VRAPIASM.VRAPIEnv.VR_CB_Call_Idle(cbCallIdle);
                m_pCallDTMF = new VRAPIASM.VRAPIEnv.VR_CB_Call_DTMF(cbCallDTMF);
                //m_pAudioBuffer = new VRAPIASM.VRAPIEnv.VR_CB_Call_Audio_Buffer(cbAudioBuffer);
                m_pCallInfo = new VRAPIASM.VRAPIEnv.VR_CB_Call_Info(cbCallInfo);
                m_pCallSIPInfo = new VRAPIASM.VRAPIEnv.VR_CB_Call_SIP_Info(cbCallSIPInfo);

                m_pGetWavFileName = new VRAPIASM.VRAPIEnv.VR_CB_GetWavFileName(cbGetWavFileName);
                m_pGetXMLFileName = new VRAPIASM.VRAPIEnv.VR_CB_GetXMLFileName(cbGetXMLFileName);


                short nicCount = VRAPIASM.VRAPIEnv.GetNICCount();

                if (nicCount <= 0)
                {
                    MessageBox.Show("No Network Interface!");
                    btnStartAndStop.Enabled = false;

                    _bExit = true;
                    Close();

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

                    if (cfg.NICIdx >= 0 && cfg.NICIdx < nicCount)
                        cbNIC.SelectedIndex = cfg.NICIdx;
                    else
                        cbNIC.SelectedIndex = 0;

                }
            }

            //tbChanNum.Text = "4";

            //dataGridView1.Enabled = false;

        }

        public void FreeCapture()
        {
            if (Inited)
            {
                VRAPIASM.VRAPIEnv.FreeCapture();
                Inited = false;
            }
        }

        public readonly static Random _rng = new Random(); 

        private static string RandomString(int size)
        {
            string _chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            char[] buffer = new char[size];

            for (int i = 0; i < size; i++)
            {
                buffer[i] = _chars[_rng.Next(_chars.Length)];
            }
            return new string(buffer);
        }

        public static string cbGetWavFileName(int ChanIndex, string UniqueID, string CallerID, string CalleeID)
        {
            return main_form.cfg.rootFolder + "\\" + RandomString(16) + ".wav";
        }

        public static string cbGetXMLFileName(int ChanIndex, string UniqueID, string CallerID, string CalleeID)
        {
            return "";
        }

        public static string GetSIPAddressInfo(int flag, string sipAddr)
        {
            //some memory problem with this one
            /*
             X [2009-04-21 11:58:43] *************On_ProxyNewCallSession got exception: Attempted to read or write protected memory. This is often an indication that other memory is corrupt.
             X [2009-04-21 11:58:43] System.AccessViolationException: Attempted to read or write protected memory. This is often an indication that other memory is corrupt.
             at GTAPIASM.GTAPIEnv.GTAPI_GetSIPAddressInfo(Int32 flag, String sipAddr)
             at GTAPIASM.GTAPIEnv.GetSIPAddressInfo(Int32 flag, String sipAddr) in C:\TEMP\projects\Samples\GTAPIASM\GTAPIASM\GTAPIEnv.cs:line 821
             at GTSIPPBX.SIPPBX.getExtensionBySIPAddr(String sip_addr)
             at GTSIPPBX.GTSIPPBXEnv.On_ProxyNewCallSession(UInt32 pid, UInt32 sid, UInt32 msg, String fromid, String toid, String suri, String via, String saddr, UInt16 nport, Boolean bCredit)
             */
            //return string.Copy(GTAPI_GetSIPAddressInfo(flag, sipAddr));

            if (sipAddr == null)
                return "";

            if (sipAddr.Length == 0)
                return "";

            //a fix for google andriod phone bad implementation
            if (sipAddr.IndexOf("<sip:") == -1 && sipAddr.IndexOf("sip:") >= 0)
            {
                int idx1 = sipAddr.IndexOf("sip:");
                if (idx1 == 0)
                {
                    sipAddr = "<" + sipAddr + ">";
                }
                else
                {
                    sipAddr = sipAddr.Substring(0, idx1) + "<" + sipAddr.Substring(idx1) + ">";
                }
            }

            int nPos = 0;
            int nPos1 = 0;

            if (flag == 0) //display name
            {
                nPos = sipAddr.IndexOf("<sip:");
                if (nPos == -1 || nPos == 0)
                    return "";
                else
                    return sipAddr.Substring(0, nPos);
            }
            else if (flag == 1) //user name
            {
                nPos = sipAddr.IndexOf("<sip:");
                if (nPos == -1)
                    return "";
                else
                {
                    nPos += 5;

                    nPos1 = sipAddr.IndexOf('@', nPos);
                    if (nPos1 == -1)
                        return "";
                    else
                        return sipAddr.Substring(nPos, nPos1 - nPos);
                }
            }
            else if (flag == 2) //ip address
            {
                nPos = sipAddr.IndexOf('@');
                if (nPos == -1)
                {
                    nPos = sipAddr.IndexOf("<sip:");
                    if (nPos == -1)
                        return "";
                    else
                        nPos += 5;
                }
                else
                    nPos += 1;

                nPos1 = sipAddr.IndexOf('>', nPos);

                string ipAddr = "";

                if (nPos1 == -1)
                    ipAddr = sipAddr.Substring(nPos);
                else
                    ipAddr = sipAddr.Substring(nPos, nPos1 - nPos);

                nPos = ipAddr.IndexOf(';');
                if (nPos != -1)
                    ipAddr = ipAddr.Substring(0, nPos);

                nPos = ipAddr.IndexOf(':');
                if (nPos != -1)
                    ipAddr = ipAddr.Substring(0, nPos);

                return ipAddr;
            }
            else if (flag == 3) //port
            {
                nPos = sipAddr.IndexOf('@');
                if (nPos == -1)
                {
                    nPos = sipAddr.IndexOf("<sip:");
                    if (nPos == -1)
                        return "";
                    else
                        nPos += 5;
                }
                else
                    nPos += 1;

                nPos1 = sipAddr.IndexOf('>', nPos);

                string ipAddr = "";

                if (nPos1 == -1)
                    ipAddr = sipAddr.Substring(nPos);
                else
                    ipAddr = sipAddr.Substring(nPos, nPos1 - nPos);

                nPos = ipAddr.IndexOf(';');
                if (nPos != -1)
                    ipAddr = ipAddr.Substring(0, nPos);

                nPos = ipAddr.IndexOf(':');
                if (nPos == -1)
                    ipAddr = "";
                else
                    ipAddr = ipAddr.Substring(nPos + 1);

                return ipAddr;
            }

            return "";
        }

        public int OnCallOffered(int ChanIndex, string CallerIP, string CallerID, string CalleeIP, string CalleeID, uint CallTime, string UniqueID, string AudioFile, int CallDir)
        {
/*
            dataGridView1.Rows[ChanIndex].Cells[0].Value = "Ringing";
            dataGridView1.Rows[ChanIndex].Cells[1].Value = CallerIP;
            dataGridView1.Rows[ChanIndex].Cells[2].Value = CallerID;
            dataGridView1.Rows[ChanIndex].Cells[3].Value = CalleeIP;
            dataGridView1.Rows[ChanIndex].Cells[4].Value = CalleeID;
            dataGridView1.Rows[ChanIndex].Cells[5].Value = UniqueID;
            dataGridView1.Rows[ChanIndex].Cells[6].Value = AudioFile;
            dataGridView1.Rows[ChanIndex].Cells[7].Value = CallDir == 0 ? "Inbound" : "Outbound";
 */
            if (ChanIndex == 0)
            {
                tbCaller.Text = GetSIPAddressInfo(1, CallerID);
                tbCallee.Text = GetSIPAddressInfo(1, CalleeID);
                tbFileName.Text = "Call conntecting ... " + AudioFile;

                notifyIcon1.BalloonTipTitle = "Mini VoIP Recorder";
                notifyIcon1.BalloonTipText = "New call from " + GetSIPAddressInfo(1, CallerID) + " to " + GetSIPAddressInfo(1, CalleeID);
                notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;

                notifyIcon1.Visible = true;
                notifyIcon1.ShowBalloonTip(1000);
            }

            return 1;
        }


        public static int cbCallOffered(int ChanIndex, string CallerIP, string CallerID, string CalleeIP, string CalleeID, uint CallTime, string UniqueID, string AudioFile, int CallDir)
        {
            //return main_form.OnCallOffered(ChanIndex, CallerIP, CallerID, CalleeIP, CalleeID, CallTime, UniqueID, AudioFile, CallDir);
            try
            {
                if (main_form != null)
                {
                    if (ChanIndex >= 0 && ChanIndex < main_form.ChanNum)
                    {
                        VR2Event evt = new VR2Event();
                        evt.EventID = 1;
                        evt.ChanID = ChanIndex;
                        evt.Params[0] = CallerIP;
                        evt.Params[1] = CallerID;
                        evt.Params[2] = CalleeIP;
                        evt.Params[3] = CalleeID;
                        evt.Params[4] = CallTime.ToString();
                        evt.Params[5] = UniqueID;
                        evt.Params[6] = AudioFile;
                        evt.Params[7] = CallDir.ToString();

                        lock (main_form)
                        {
                            main_form.event_queue.Enqueue(evt);
                        }
                    }
                }
            }
            catch (Exception)
            {
                //VRAPIASM.VRAPIEnv.Log(1, ex.ToString());
            }

            return 0;
        }

        public void OnCallConnected(int ChanIndex, string CallerIP, string CallerID, string CalleeIP, string CalleeID, uint InitTime, uint ConnectTime, string UniqueID, string AudioFile, int CallDir)
        {
/*
            dataGridView1.Rows[ChanIndex].Cells[0].Value = "Recording";
            dataGridView1.Rows[ChanIndex].Cells[1].Value = CallerIP;
            dataGridView1.Rows[ChanIndex].Cells[2].Value = CallerID;
            dataGridView1.Rows[ChanIndex].Cells[3].Value = CalleeIP;
            dataGridView1.Rows[ChanIndex].Cells[4].Value = CalleeID;
            dataGridView1.Rows[ChanIndex].Cells[5].Value = UniqueID;
            dataGridView1.Rows[ChanIndex].Cells[6].Value = AudioFile;
            dataGridView1.Rows[ChanIndex].Cells[7].Value = CallDir == 0 ? "Inbound" : "Outbound";*/

            if (ChanIndex == 0)
            {
                tbCaller.Text = GetSIPAddressInfo(1, CallerID);
                tbCallee.Text = GetSIPAddressInfo(1, CalleeID);
                tbFileName.Text = "Recording .... " + AudioFile;

                notifyIcon1.BalloonTipTitle = "Mini VoIP Recorder";
                notifyIcon1.BalloonTipText = "Call from " + GetSIPAddressInfo(1, CallerID) + " to " + GetSIPAddressInfo(1, CalleeID) + " connected. Recording ... ";
                notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;

                notifyIcon1.ShowBalloonTip(1000);
            }
        }

        public static void cbCallConnected(int ChanIndex, string CallerIP, string CallerID, string CalleeIP, string CalleeID, uint InitTime, uint ConnectTime, string UniqueID, string AudioFile, int CallDir)
        {
            //main_form.OnCallConnected(ChanIndex, CallerIP, CallerID, CalleeIP, CalleeID, InitTime, ConnectTime, UniqueID, AudioFile, CallDir);
            try
            {
                if (main_form != null)
                {
                    if (ChanIndex >= 0 && ChanIndex < main_form.ChanNum)
                    {
                        VR2Event evt = new VR2Event();
                        evt.EventID = 2;
                        evt.ChanID = ChanIndex;
                        evt.Params[0] = CallerIP;
                        evt.Params[1] = CallerID;
                        evt.Params[2] = CalleeIP;
                        evt.Params[3] = CalleeID;
                        evt.Params[4] = InitTime.ToString();
                        evt.Params[5] = ConnectTime.ToString();
                        evt.Params[6] = UniqueID;
                        evt.Params[7] = AudioFile;
                        evt.Params[8] = CallDir.ToString();

                        lock (main_form)
                        {
                            main_form.event_queue.Enqueue(evt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //VRAPIASM.VRAPIEnv.Log(1, ex.ToString());
            }
        }

        public void OnCallIdle(int ChanIndex, string CallerIP, string CallerID, string CalleeIP, string CalleeID, uint InitTime, uint ConnectTime, uint EndTime, string UniqueID, string AudioFile, int AudioFileNum, int Reason, int CallDir, string DTMFStr)
        {
/*
            dataGridView1.Rows[ChanIndex].Cells[0].Value = "Idle";
            dataGridView1.Rows[ChanIndex].Cells[1].Value = "";
            dataGridView1.Rows[ChanIndex].Cells[2].Value = "";
            dataGridView1.Rows[ChanIndex].Cells[3].Value = "";
            dataGridView1.Rows[ChanIndex].Cells[4].Value = "";
            dataGridView1.Rows[ChanIndex].Cells[5].Value = "";
            dataGridView1.Rows[ChanIndex].Cells[6].Value = "";
            dataGridView1.Rows[ChanIndex].Cells[7].Value = "";
            dataGridView1.Rows[ChanIndex].Cells[8].Value = "";
 */

            //assume wav to mp3 happens here
            //string mp3AudioFile = AudioFile;
            //mp3AudioFile = mp3AudioFile.Replace(".wav", ".mp3");
            //VRAPIASM.VRAPIEnv.WAV2MP3(AudioFile, mp3AudioFile, 1);
            if (ChanIndex == 0)
            {
                tbCaller.Text = GetSIPAddressInfo(1, CallerID);
                tbCallee.Text = GetSIPAddressInfo(1, CalleeID);
                tbFileName.Text = "Call done: " + AudioFile;

                notifyIcon1.BalloonTipTitle = "Mini VoIP Recorder";
                notifyIcon1.BalloonTipText = "Call from " + GetSIPAddressInfo(1, CallerID) + " to " + GetSIPAddressInfo(1, CalleeID) + " is done.";
                notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;

                notifyIcon1.ShowBalloonTip(1000);
            }
        }

        public static void cbCallIdle(int ChanIndex, string CallerIP, string CallerID, string CalleeIP, string CalleeID, uint InitTime, uint ConnectTime, uint EndTime, string UniqueID, string AudioFile, int AudioFileNum, int Reason, int CallDir, string DTMFStr, int codec)
        {
            //main_form.OnCallIdle(ChanIndex, CallerIP, CallerID, CalleeIP, CalleeID, InitTime, ConnectTime, EndTime, UniqueID, AudioFile, AudioFileNum, Reason, CallDir, DTMFStr);
            try
            {
                if (main_form != null)
                {
                    if (ChanIndex >= 0 && ChanIndex < main_form.ChanNum)
                    {
                        VR2Event evt = new VR2Event();
                        evt.EventID = 3;
                        evt.ChanID = ChanIndex;
                        evt.Params[0] = CallerIP;
                        evt.Params[1] = CallerID;
                        evt.Params[2] = CalleeIP;
                        evt.Params[3] = CalleeID;
                        evt.Params[4] = InitTime.ToString();
                        evt.Params[5] = ConnectTime.ToString();
                        evt.Params[6] = EndTime.ToString();

                        evt.Params[7] = UniqueID;
                        evt.Params[8] = AudioFile;
                        evt.Params[9] = AudioFileNum.ToString();

                        evt.Params[10] = Reason.ToString();
                        evt.Params[11] = CallDir.ToString();
                        evt.Params[12] = DTMFStr;
                        evt.Params[13] = "1";
                        evt.Params[14] = codec.ToString();

                        lock (main_form)
                        {
                            main_form.event_queue.Enqueue(evt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                VRAPIASM.VRAPIEnv.Log(1, ex.ToString());
            }
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
        public void Start()
        {
            if (Started) return;

            VRAPIASM.VRAPIEnv.SetAudioRootFolder(cfg.rootFolder);

            //<!-- 0 = default(.wav), 1 = mp3, 2 = gsm -->
            VRAPIASM.VRAPIEnv.SetAudioFileFormat(0);

            //log
            VRAPIASM.VRAPIEnv.SetLogFileName(Application.StartupPath + "\\MiniVoIPRecorder-Log.txt");
            VRAPIASM.VRAPIEnv.SetLogLevel(4);

            //license key
            //VRAPIASM.VRAPIEnv.SetLicenseKey("");

            //how many channels to open
            VRAPIASM.VRAPIEnv.SetChannelCount(ChanNum);

            //SIP, SCCP or SKINNY, RTP, H323, IAX2, UNISTIM, MGCP
            //NOTE: only SIP, SCCP, RTP, and MGCP work so far
            VRAPIASM.VRAPIEnv.SetProtocol(cfg.Protocol);

            VRAPIASM.VRAPIEnv.SetNIC(Convert.ToInt16(cbNIC.SelectedIndex));

            VRAPIASM.VRAPIEnv.SetCB_Call_Offered(m_pCallOffered);
            VRAPIASM.VRAPIEnv.SetCB_Call_Connected(m_pCallConnected);
            VRAPIASM.VRAPIEnv.SetCB_Call_Idle(m_pCallIdle);
            VRAPIASM.VRAPIEnv.SetCB_Call_DTMF(m_pCallDTMF);

            VRAPIASM.VRAPIEnv.SetCB_Call_Info(m_pCallInfo);
            VRAPIASM.VRAPIEnv.SetCB_Call_SIP_Info(m_pCallSIPInfo);

            VRAPIASM.VRAPIEnv.SetCB_GetWAVFileName(m_pGetWavFileName);
            VRAPIASM.VRAPIEnv.SetCB_GetXMLFileName(m_pGetXMLFileName);

            //disable creating XML
            VRAPIASM.VRAPIEnv.SetEnableXML(0);

            //ignore possible same call
            VRAPIASM.VRAPIEnv.SetIgnorePossibleSameCall(cfg.ignoreSameCall);

            //if recording.
            VRAPIASM.VRAPIEnv.SetRecording(cfg.recordingCall);

            VRAPIASM.VRAPIEnv.SetUsePacketTime(cfg.usePacketTime);

            VRAPIASM.VRAPIEnv.SetNoAudioSeconds(cfg.noAudioSeconds);

            //VRAPIASM.VRAPIEnv.SetCB_GetWAVFileName(new VRAPIASM.VRAPIEnv.VR_CB_GetWavFileName(cbGetWavFileName));

            VRAPIASM.VRAPIEnv.StartCapture();

            ChanNum = VRAPIASM.VRAPIEnv.GetChannelCount();

            /*
            if (VRAPIASM.VRAPIEnv.IsLicensed() != 0)
            {
                VRAPIASM.VRAPIEnv.Log(4, "Licensed Software");
            }*/

            Started = true;
            btnStartAndStop.Text = "Stop";

            tbCaller.Enabled = true;
            tbCallee.Enabled = true;
            tbFileName.Enabled = true;
            cbNIC.Enabled = false;

            timer1.Enabled = true;

            optionsToolStripMenuItem1.Enabled = false;
            optionsToolStripMenuItem.Enabled = false;

            startRecordingToolStripMenuItem.Enabled = false;
            stopRecordingToolStripMenuItem.Enabled = true;

            recordingToolStripMenuItem.Text = "Status: Recording ...";
        }

        public void Stop()
        {
            if (Started)
            {
                VRAPIASM.VRAPIEnv.StopCapture();

                //reset recorder API just in case
                VRAPIASM.VRAPIEnv.FreeCapture();
                VRAPIASM.VRAPIEnv.InitCapture(0);

                Started = false;
                btnStartAndStop.Text = "Start";

                tbCaller.Enabled = false;
                tbCallee.Enabled = false;
                tbFileName.Enabled = false;
                cbNIC.Enabled = true;

                timer1.Enabled = false;

                optionsToolStripMenuItem1.Enabled = true;
                optionsToolStripMenuItem.Enabled = true;

                startRecordingToolStripMenuItem.Enabled = true;
                stopRecordingToolStripMenuItem.Enabled = false;

                recordingToolStripMenuItem.Text = "Status: Recording Stopped";
            }
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _bExit = true;
            Close();
        }

        private void maximumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void quitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            _bExit = true;
            Close();
        }

        private void optionsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FormOptions dlg = new FormOptions();
            dlg.cfg = cfg;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                SaveConfigToRegsitry();
            }
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormOptions dlg = new FormOptions();
            dlg.cfg = cfg;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                SaveConfigToRegsitry();
            }
        }

        private void btnStartAndStop_Click(object sender, EventArgs e)
        {
            if (!Started)
                Start();
            else
                Stop();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lock (main_form)
            {
                while (event_queue.Count > 0)
                {
                    VR2Event evt = event_queue.Dequeue();
                    if (evt != null)
                    {
                        switch (evt.EventID)
                        {
                            case 1:
                                main_form.OnCallOffered(evt.ChanID, evt.Params[0], evt.Params[1], evt.Params[2], evt.Params[3], Convert.ToUInt32(evt.Params[4]), evt.Params[5], evt.Params[6], Convert.ToInt32(evt.Params[7]));
                                break;
                            case 2:
                                main_form.OnCallConnected(evt.ChanID, evt.Params[0], evt.Params[1], evt.Params[2], evt.Params[3], Convert.ToUInt32(evt.Params[4]), Convert.ToUInt32(evt.Params[5]), evt.Params[6], evt.Params[7], Convert.ToInt32(evt.Params[8]));
                                break;
                            case 3:
                                //VRAPIASM.VRAPIEnv.Log(4, "cbCallIdle event dequeued!");
                                main_form.OnCallIdle(evt.ChanID, evt.Params[0], evt.Params[1], evt.Params[2], evt.Params[3], Convert.ToUInt32(evt.Params[4]), Convert.ToUInt32(evt.Params[5]), Convert.ToUInt32(evt.Params[6]), evt.Params[7], evt.Params[8], Convert.ToInt32(evt.Params[9]), Convert.ToInt32(evt.Params[10]), Convert.ToInt32(evt.Params[11]), evt.Params[12]);
                                break;
                            case 4:
                                break;
                            case 5:
                                break;
                            case 6:
                                break;
                            case 15: //set chan name

                                break;
                        }
                    }
                }

            }
        }

        private void startRecordingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Start();
        }

        private void stopRecordingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stop();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAbout frm = new FormAbout();
            frm.ShowDialog();
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FormAbout frm = new FormAbout();
            frm.ShowDialog();
        }

        private void cbNIC_SelectedIndexChanged(object sender, EventArgs e)
        {
            cfg.NICIdx = cbNIC.SelectedIndex;

            RegistryKey regkey;
            //string tmp;
            regkey = Registry.CurrentUser.CreateSubKey(MINIVR_REG_KEY_ROOT);
            if (regkey == null)
                return;

            regkey.SetValue("NICIdx", cfg.NICIdx);

            regkey.Close();
        }



    }

    public class MiniVRCfg
    {
        public string Protocol;
        public string rootFolder;
        public string Filters;
        public string Excludes;
        public int ChanNum;
        public int AudioFormat;
        public string RTPPBXAddr;
        public int ignoreSameCall;
        public int recordingCall;
        public int usePacketTime;
        public int noAudioSeconds;
        public int NICIdx;

        public MiniVRCfg()
        {
            Protocol = "SIP";
            rootFolder = Application.StartupPath + "\\Audio";
            Filters = "";
            Excludes = "";
            ChanNum = 1;
            AudioFormat = 0;
            RTPPBXAddr = "";
            ignoreSameCall = 0;
            recordingCall = 1;
            usePacketTime = 0;
            noAudioSeconds = 0;
            NICIdx = 0;

        }
    }

    public class VR2Event
    {
        public int EventID;
        public int ChanID;
        public string[] Params;

        public VR2Event()
        {
            EventID = 0;
            ChanID = 0;
            Params = new string[20];
            for (int i = 0; i < 20; i++)
            {
                Params[i] = "";
            }
        }
    }
}