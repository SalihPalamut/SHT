namespace SHT
{
    partial class SHT
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
            this.Logs = new System.Windows.Forms.RichTextBox();
            this.send_data = new System.Windows.Forms.TextBox();
            this.SendGroup = new System.Windows.Forms.GroupBox();
            this.append = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.CR = new System.Windows.Forms.CheckBox();
            this.LF = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.send = new System.Windows.Forms.Button();
            this.SndHex = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.HidDevices = new System.Windows.Forms.ListView();
            this.Recive = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.RcvHex = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.SendGroup.SuspendLayout();
            this.append.SuspendLayout();
            this.panel2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.Recive.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Logs
            // 
            this.Logs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Logs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Logs.Location = new System.Drawing.Point(3, 16);
            this.Logs.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Logs.Name = "Logs";
            this.Logs.ReadOnly = true;
            this.Logs.Size = new System.Drawing.Size(464, 140);
            this.Logs.TabIndex = 0;
            this.Logs.Text = "";
            this.Logs.TextChanged += new System.EventHandler(this.Logs_TextChanged);
            // 
            // send_data
            // 
            this.send_data.Dock = System.Windows.Forms.DockStyle.Right;
            this.send_data.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.send_data.Location = new System.Drawing.Point(2, 0);
            this.send_data.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.send_data.Name = "send_data";
            this.send_data.Size = new System.Drawing.Size(308, 22);
            this.send_data.TabIndex = 1;
            this.send_data.KeyDown += new System.Windows.Forms.KeyEventHandler(this.send_data_KeyDown);
            this.send_data.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.send_data_KeyPress);
            this.send_data.KeyUp += new System.Windows.Forms.KeyEventHandler(this.send_data_KeyUp);
            // 
            // SendGroup
            // 
            this.SendGroup.Controls.Add(this.append);
            this.SendGroup.Controls.Add(this.panel2);
            this.SendGroup.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.SendGroup.Enabled = false;
            this.SendGroup.Location = new System.Drawing.Point(0, 239);
            this.SendGroup.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.SendGroup.Name = "SendGroup";
            this.SendGroup.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.SendGroup.Size = new System.Drawing.Size(470, 57);
            this.SendGroup.TabIndex = 2;
            this.SendGroup.TabStop = false;
            this.SendGroup.Text = "Send";
            // 
            // append
            // 
            this.append.Controls.Add(this.label1);
            this.append.Controls.Add(this.CR);
            this.append.Controls.Add(this.LF);
            this.append.Dock = System.Windows.Forms.DockStyle.Top;
            this.append.Enabled = false;
            this.append.Location = new System.Drawing.Point(2, 38);
            this.append.Name = "append";
            this.append.Size = new System.Drawing.Size(466, 19);
            this.append.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Right;
            this.label1.Location = new System.Drawing.Point(343, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Append";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CR
            // 
            this.CR.AutoSize = true;
            this.CR.Dock = System.Windows.Forms.DockStyle.Right;
            this.CR.Location = new System.Drawing.Point(387, 0);
            this.CR.Name = "CR";
            this.CR.Size = new System.Drawing.Size(41, 19);
            this.CR.TabIndex = 6;
            this.CR.Text = "CR";
            this.CR.UseVisualStyleBackColor = true;
            // 
            // LF
            // 
            this.LF.AutoSize = true;
            this.LF.Dock = System.Windows.Forms.DockStyle.Right;
            this.LF.Location = new System.Drawing.Point(428, 0);
            this.LF.Name = "LF";
            this.LF.Size = new System.Drawing.Size(38, 19);
            this.LF.TabIndex = 7;
            this.LF.Text = "LF";
            this.LF.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.send_data);
            this.panel2.Controls.Add(this.send);
            this.panel2.Controls.Add(this.SndHex);
            this.panel2.Controls.Add(this.radioButton1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(2, 15);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(466, 23);
            this.panel2.TabIndex = 5;
            // 
            // send
            // 
            this.send.Dock = System.Windows.Forms.DockStyle.Right;
            this.send.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.send.Location = new System.Drawing.Point(310, 0);
            this.send.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.send.Name = "send";
            this.send.Size = new System.Drawing.Size(56, 23);
            this.send.TabIndex = 4;
            this.send.Text = "Send";
            this.send.UseVisualStyleBackColor = true;
            this.send.Click += new System.EventHandler(this.send_Click);
            // 
            // SndHex
            // 
            this.SndHex.AutoSize = true;
            this.SndHex.Checked = true;
            this.SndHex.Dock = System.Windows.Forms.DockStyle.Right;
            this.SndHex.Location = new System.Drawing.Point(366, 0);
            this.SndHex.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.SndHex.Name = "SndHex";
            this.SndHex.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.SndHex.Size = new System.Drawing.Size(47, 23);
            this.SndHex.TabIndex = 2;
            this.SndHex.TabStop = true;
            this.SndHex.Text = "HEX";
            this.SndHex.UseVisualStyleBackColor = true;
            this.SndHex.CheckedChanged += new System.EventHandler(this.SndHex_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Dock = System.Windows.Forms.DockStyle.Right;
            this.radioButton1.Location = new System.Drawing.Point(413, 0);
            this.radioButton1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(53, 23);
            this.radioButton1.TabIndex = 3;
            this.radioButton1.Text = "TEXT";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(470, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.HidDevices);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 24);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Size = new System.Drawing.Size(470, 215);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "HID Devices";
            // 
            // HidDevices
            // 
            this.HidDevices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HidDevices.FullRowSelect = true;
            this.HidDevices.HideSelection = false;
            this.HidDevices.Location = new System.Drawing.Point(2, 15);
            this.HidDevices.Name = "HidDevices";
            this.HidDevices.Size = new System.Drawing.Size(466, 198);
            this.HidDevices.TabIndex = 0;
            this.HidDevices.UseCompatibleStateImageBehavior = false;
            this.HidDevices.SelectedIndexChanged += new System.EventHandler(this.HidDevices_SelectedIndexChanged);
            // 
            // Recive
            // 
            this.Recive.Controls.Add(this.Logs);
            this.Recive.Controls.Add(this.panel1);
            this.Recive.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Recive.Location = new System.Drawing.Point(0, 296);
            this.Recive.Name = "Recive";
            this.Recive.Size = new System.Drawing.Size(470, 182);
            this.Recive.TabIndex = 5;
            this.Recive.TabStop = false;
            this.Recive.Text = "Recive";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.RcvHex);
            this.panel1.Controls.Add(this.radioButton2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(3, 156);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(464, 23);
            this.panel1.TabIndex = 1;
            // 
            // RcvHex
            // 
            this.RcvHex.AutoSize = true;
            this.RcvHex.Checked = true;
            this.RcvHex.Dock = System.Windows.Forms.DockStyle.Right;
            this.RcvHex.Location = new System.Drawing.Point(364, 0);
            this.RcvHex.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.RcvHex.Name = "RcvHex";
            this.RcvHex.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RcvHex.Size = new System.Drawing.Size(47, 23);
            this.RcvHex.TabIndex = 4;
            this.RcvHex.TabStop = true;
            this.RcvHex.Text = "HEX";
            this.RcvHex.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Dock = System.Windows.Forms.DockStyle.Right;
            this.radioButton2.Location = new System.Drawing.Point(411, 0);
            this.radioButton2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(53, 23);
            this.radioButton2.TabIndex = 5;
            this.radioButton2.Text = "TEXT";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // SHT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 478);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.SendGroup);
            this.Controls.Add(this.Recive);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MinimizeBox = false;
            this.Name = "SHT";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Simple Hid Terminal";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SHT_FormClosing);
            this.Load += new System.EventHandler(this.SHT_Load);
            this.SendGroup.ResumeLayout(false);
            this.append.ResumeLayout(false);
            this.append.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.Recive.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox Logs;
        private System.Windows.Forms.TextBox send_data;
        private System.Windows.Forms.GroupBox SendGroup;
        private System.Windows.Forms.Button send;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton SndHex;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox Recive;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton RcvHex;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.ListView HidDevices;
        private System.Windows.Forms.Panel append;
        private System.Windows.Forms.CheckBox CR;
        private System.Windows.Forms.CheckBox LF;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
    }
}

