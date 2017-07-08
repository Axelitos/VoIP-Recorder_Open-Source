namespace MiniVoIPRecorder
{
    partial class FormOptions
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormOptions));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tbChanNum = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbExcludes = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbFilters = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.rbGSM = new System.Windows.Forms.RadioButton();
            this.rbMP3 = new System.Windows.Forms.RadioButton();
            this.rbWav = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.btnRootFolderBrowse = new System.Windows.Forms.Button();
            this.tbRootFolder = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tbRTPPBXAddr = new System.Windows.Forms.TextBox();
            this.lbRTPPBXAddr = new System.Windows.Forms.Label();
            this.tbNoAudioSeconds = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbUsePacketTime = new System.Windows.Forms.CheckBox();
            this.cbRecording = new System.Windows.Forms.CheckBox();
            this.cbIgnorePossibleSameCall = new System.Windows.Forms.CheckBox();
            this.cbProtocol = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(496, 227);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.tbChanNum);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.tbExcludes);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.tbFilters);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.rbGSM);
            this.tabPage1.Controls.Add(this.rbMP3);
            this.tabPage1.Controls.Add(this.rbWav);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.btnRootFolderBrowse);
            this.tabPage1.Controls.Add(this.tbRootFolder);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(488, 201);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "General";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tbChanNum
            // 
            this.tbChanNum.Location = new System.Drawing.Point(118, 156);
            this.tbChanNum.Name = "tbChanNum";
            this.tbChanNum.Size = new System.Drawing.Size(67, 20);
            this.tbChanNum.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 159);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Channel Number:";
            // 
            // tbExcludes
            // 
            this.tbExcludes.Location = new System.Drawing.Point(118, 120);
            this.tbExcludes.Name = "tbExcludes";
            this.tbExcludes.Size = new System.Drawing.Size(259, 20);
            this.tbExcludes.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 120);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Excludes:";
            // 
            // tbFilters
            // 
            this.tbFilters.Location = new System.Drawing.Point(118, 87);
            this.tbFilters.Name = "tbFilters";
            this.tbFilters.Size = new System.Drawing.Size(259, 20);
            this.tbFilters.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Filters:";
            // 
            // rbGSM
            // 
            this.rbGSM.AutoSize = true;
            this.rbGSM.Location = new System.Drawing.Point(257, 53);
            this.rbGSM.Name = "rbGSM";
            this.rbGSM.Size = new System.Drawing.Size(49, 17);
            this.rbGSM.TabIndex = 6;
            this.rbGSM.TabStop = true;
            this.rbGSM.Text = "GSM";
            this.rbGSM.UseVisualStyleBackColor = true;
            // 
            // rbMP3
            // 
            this.rbMP3.AutoSize = true;
            this.rbMP3.Location = new System.Drawing.Point(188, 53);
            this.rbMP3.Name = "rbMP3";
            this.rbMP3.Size = new System.Drawing.Size(47, 17);
            this.rbMP3.TabIndex = 5;
            this.rbMP3.TabStop = true;
            this.rbMP3.Text = "MP3";
            this.rbMP3.UseVisualStyleBackColor = true;
            // 
            // rbWav
            // 
            this.rbWav.AutoSize = true;
            this.rbWav.Location = new System.Drawing.Point(118, 53);
            this.rbWav.Name = "rbWav";
            this.rbWav.Size = new System.Drawing.Size(48, 17);
            this.rbWav.TabIndex = 4;
            this.rbWav.TabStop = true;
            this.rbWav.Text = "Wav";
            this.rbWav.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Audio Format:";
            // 
            // btnRootFolderBrowse
            // 
            this.btnRootFolderBrowse.Location = new System.Drawing.Point(383, 16);
            this.btnRootFolderBrowse.Name = "btnRootFolderBrowse";
            this.btnRootFolderBrowse.Size = new System.Drawing.Size(97, 22);
            this.btnRootFolderBrowse.TabIndex = 2;
            this.btnRootFolderBrowse.Text = "Browse...";
            this.btnRootFolderBrowse.UseVisualStyleBackColor = true;
            this.btnRootFolderBrowse.Click += new System.EventHandler(this.btnRootFolderBrowse_Click);
            // 
            // tbRootFolder
            // 
            this.tbRootFolder.Location = new System.Drawing.Point(118, 17);
            this.tbRootFolder.Name = "tbRootFolder";
            this.tbRootFolder.Size = new System.Drawing.Size(259, 20);
            this.tbRootFolder.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Audio Root Folder:";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tbRTPPBXAddr);
            this.tabPage2.Controls.Add(this.lbRTPPBXAddr);
            this.tabPage2.Controls.Add(this.tbNoAudioSeconds);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.cbUsePacketTime);
            this.tabPage2.Controls.Add(this.cbRecording);
            this.tabPage2.Controls.Add(this.cbIgnorePossibleSameCall);
            this.tabPage2.Controls.Add(this.cbProtocol);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(488, 201);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "VoIP";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tbRTPPBXAddr
            // 
            this.tbRTPPBXAddr.Location = new System.Drawing.Point(333, 17);
            this.tbRTPPBXAddr.Name = "tbRTPPBXAddr";
            this.tbRTPPBXAddr.Size = new System.Drawing.Size(133, 20);
            this.tbRTPPBXAddr.TabIndex = 8;
            // 
            // lbRTPPBXAddr
            // 
            this.lbRTPPBXAddr.AutoSize = true;
            this.lbRTPPBXAddr.Location = new System.Drawing.Point(228, 21);
            this.lbRTPPBXAddr.Name = "lbRTPPBXAddr";
            this.lbRTPPBXAddr.Size = new System.Drawing.Size(97, 13);
            this.lbRTPPBXAddr.TabIndex = 7;
            this.lbRTPPBXAddr.Text = "RTP PBX Address:";
            // 
            // tbNoAudioSeconds
            // 
            this.tbNoAudioSeconds.Location = new System.Drawing.Point(121, 165);
            this.tbNoAudioSeconds.Name = "tbNoAudioSeconds";
            this.tbNoAudioSeconds.Size = new System.Drawing.Size(71, 20);
            this.tbNoAudioSeconds.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 168);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(99, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "No Audio Seconds:";
            // 
            // cbUsePacketTime
            // 
            this.cbUsePacketTime.AutoSize = true;
            this.cbUsePacketTime.Location = new System.Drawing.Point(19, 128);
            this.cbUsePacketTime.Name = "cbUsePacketTime";
            this.cbUsePacketTime.Size = new System.Drawing.Size(108, 17);
            this.cbUsePacketTime.TabIndex = 4;
            this.cbUsePacketTime.Text = "Use Packet Time";
            this.cbUsePacketTime.UseVisualStyleBackColor = true;
            // 
            // cbRecording
            // 
            this.cbRecording.AutoSize = true;
            this.cbRecording.Location = new System.Drawing.Point(19, 93);
            this.cbRecording.Name = "cbRecording";
            this.cbRecording.Size = new System.Drawing.Size(95, 17);
            this.cbRecording.TabIndex = 3;
            this.cbRecording.Text = "Recording Call";
            this.cbRecording.UseVisualStyleBackColor = true;
            // 
            // cbIgnorePossibleSameCall
            // 
            this.cbIgnorePossibleSameCall.AutoSize = true;
            this.cbIgnorePossibleSameCall.Location = new System.Drawing.Point(19, 59);
            this.cbIgnorePossibleSameCall.Name = "cbIgnorePossibleSameCall";
            this.cbIgnorePossibleSameCall.Size = new System.Drawing.Size(148, 17);
            this.cbIgnorePossibleSameCall.TabIndex = 2;
            this.cbIgnorePossibleSameCall.Text = "Ignore Possible Same Call";
            this.cbIgnorePossibleSameCall.UseVisualStyleBackColor = true;
            // 
            // cbProtocol
            // 
            this.cbProtocol.FormattingEnabled = true;
            this.cbProtocol.Location = new System.Drawing.Point(71, 17);
            this.cbProtocol.Name = "cbProtocol";
            this.cbProtocol.Size = new System.Drawing.Size(121, 21);
            this.cbProtocol.TabIndex = 1;
            this.cbProtocol.SelectedIndexChanged += new System.EventHandler(this.cbProtocol_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Protocol:";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(109, 246);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(96, 31);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(315, 246);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(96, 31);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(393, 90);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(13, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "?";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(393, 123);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(13, 13);
            this.label9.TabIndex = 14;
            this.label9.Text = "?";
            this.label9.Click += new System.EventHandler(this.label9_Click);
            // 
            // FormOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(520, 285);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormOptions";
            this.Text = "Options";
            this.Load += new System.EventHandler(this.FormOptions_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnRootFolderBrowse;
        private System.Windows.Forms.TextBox tbRootFolder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbWav;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbExcludes;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbFilters;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rbGSM;
        private System.Windows.Forms.RadioButton rbMP3;
        private System.Windows.Forms.TextBox tbChanNum;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbProtocol;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox cbUsePacketTime;
        private System.Windows.Forms.CheckBox cbRecording;
        private System.Windows.Forms.CheckBox cbIgnorePossibleSameCall;
        private System.Windows.Forms.TextBox tbRTPPBXAddr;
        private System.Windows.Forms.Label lbRTPPBXAddr;
        private System.Windows.Forms.TextBox tbNoAudioSeconds;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
    }
}