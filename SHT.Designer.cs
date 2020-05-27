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
            this.send = new System.Windows.Forms.Button();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.SndHex = new System.Windows.Forms.RadioButton();
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
            this.Logs.Location = new System.Drawing.Point(4, 19);
            this.Logs.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Logs.Name = "Logs";
            this.Logs.ReadOnly = true;
            this.Logs.Size = new System.Drawing.Size(615, 169);
            this.Logs.TabIndex = 0;
            this.Logs.Text = "";
            // 
            // send_data
            // 
            this.send_data.Dock = System.Windows.Forms.DockStyle.Left;
            this.send_data.Location = new System.Drawing.Point(3, 17);
            this.send_data.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.send_data.Name = "send_data";
            this.send_data.Size = new System.Drawing.Size(409, 22);
            this.send_data.TabIndex = 1;
            this.send_data.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.send_data_KeyPress);
            // 
            // SendGroup
            // 
            this.SendGroup.Controls.Add(this.send);
            this.SendGroup.Controls.Add(this.radioButton1);
            this.SendGroup.Controls.Add(this.SndHex);
            this.SendGroup.Controls.Add(this.send_data);
            this.SendGroup.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.SendGroup.Enabled = false;
            this.SendGroup.Location = new System.Drawing.Point(0, 331);
            this.SendGroup.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SendGroup.Name = "SendGroup";
            this.SendGroup.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SendGroup.Size = new System.Drawing.Size(623, 48);
            this.SendGroup.TabIndex = 2;
            this.SendGroup.TabStop = false;
            this.SendGroup.Text = "Send";
            // 
            // send
            // 
            this.send.Dock = System.Windows.Forms.DockStyle.Left;
            this.send.Location = new System.Drawing.Point(534, 17);
            this.send.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.send.Name = "send";
            this.send.Size = new System.Drawing.Size(75, 29);
            this.send.TabIndex = 4;
            this.send.Text = "Send";
            this.send.UseVisualStyleBackColor = true;
            this.send.Click += new System.EventHandler(this.send_Click);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Dock = System.Windows.Forms.DockStyle.Left;
            this.radioButton1.Location = new System.Drawing.Point(469, 17);
            this.radioButton1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(65, 29);
            this.radioButton1.TabIndex = 3;
            this.radioButton1.Text = "TEXT";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // SndHex
            // 
            this.SndHex.AutoSize = true;
            this.SndHex.Checked = true;
            this.SndHex.Dock = System.Windows.Forms.DockStyle.Left;
            this.SndHex.Location = new System.Drawing.Point(412, 17);
            this.SndHex.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SndHex.Name = "SndHex";
            this.SndHex.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.SndHex.Size = new System.Drawing.Size(57, 29);
            this.SndHex.TabIndex = 2;
            this.SndHex.TabStop = true;
            this.SndHex.Text = "HEX";
            this.SndHex.UseVisualStyleBackColor = true;
            this.SndHex.CheckedChanged += new System.EventHandler(this.SndHex_CheckedChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(623, 28);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(64, 24);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.HidDevices);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 28);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Size = new System.Drawing.Size(623, 303);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "HID Devices";
            // 
            // HidDevices
            // 
            this.HidDevices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HidDevices.FullRowSelect = true;
            this.HidDevices.HideSelection = false;
            this.HidDevices.Location = new System.Drawing.Point(3, 17);
            this.HidDevices.Margin = new System.Windows.Forms.Padding(4);
            this.HidDevices.Name = "HidDevices";
            this.HidDevices.Size = new System.Drawing.Size(617, 284);
            this.HidDevices.TabIndex = 0;
            this.HidDevices.UseCompatibleStateImageBehavior = false;
            this.HidDevices.SelectedIndexChanged += new System.EventHandler(this.HidDevices_SelectedIndexChanged);
            // 
            // Recive
            // 
            this.Recive.Controls.Add(this.Logs);
            this.Recive.Controls.Add(this.panel1);
            this.Recive.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Recive.Location = new System.Drawing.Point(0, 379);
            this.Recive.Margin = new System.Windows.Forms.Padding(4);
            this.Recive.Name = "Recive";
            this.Recive.Padding = new System.Windows.Forms.Padding(4);
            this.Recive.Size = new System.Drawing.Size(623, 220);
            this.Recive.TabIndex = 5;
            this.Recive.TabStop = false;
            this.Recive.Text = "Recive";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.RcvHex);
            this.panel1.Controls.Add(this.radioButton2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(4, 188);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(615, 28);
            this.panel1.TabIndex = 1;
            // 
            // RcvHex
            // 
            this.RcvHex.AutoSize = true;
            this.RcvHex.Checked = true;
            this.RcvHex.Dock = System.Windows.Forms.DockStyle.Right;
            this.RcvHex.Location = new System.Drawing.Point(493, 0);
            this.RcvHex.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.RcvHex.Name = "RcvHex";
            this.RcvHex.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RcvHex.Size = new System.Drawing.Size(57, 28);
            this.RcvHex.TabIndex = 4;
            this.RcvHex.TabStop = true;
            this.RcvHex.Text = "HEX";
            this.RcvHex.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Dock = System.Windows.Forms.DockStyle.Right;
            this.radioButton2.Location = new System.Drawing.Point(550, 0);
            this.radioButton2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(65, 28);
            this.radioButton2.TabIndex = 5;
            this.radioButton2.Text = "TEXT";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // SHT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(623, 599);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.SendGroup);
            this.Controls.Add(this.Recive);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "SHT";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Simple Hid Terminal";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SHT_FormClosing);
            this.Load += new System.EventHandler(this.SHT_Load);
            this.SendGroup.ResumeLayout(false);
            this.SendGroup.PerformLayout();
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
    }
}

