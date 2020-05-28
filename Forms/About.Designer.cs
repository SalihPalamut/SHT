namespace SHT
{
    partial class About
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.about_txt = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::SHT.Properties.Resources.sht;
            this.pictureBox1.Location = new System.Drawing.Point(12, 15);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(128, 128);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // about_txt
            // 
            this.about_txt.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.about_txt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.about_txt.Dock = System.Windows.Forms.DockStyle.Right;
            this.about_txt.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.about_txt.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.about_txt.Location = new System.Drawing.Point(146, 0);
            this.about_txt.Name = "about_txt";
            this.about_txt.ReadOnly = true;
            this.about_txt.ShortcutsEnabled = false;
            this.about_txt.Size = new System.Drawing.Size(253, 159);
            this.about_txt.TabIndex = 2;
            this.about_txt.TabStop = false;
            this.about_txt.Text = "Simple Hid Terminal\n\nVerison : 1.0\n\nSLHTech®\nCopy © 2020\n\nmailto:palamutsalih@gma" +
    "il.com\nhttps://github.com/SalihPalamut/SHT";
            this.about_txt.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.about_txt_LinkClicked);
            // 
            // About
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(399, 159);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.about_txt);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "About";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About";
            this.Load += new System.EventHandler(this.About_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.RichTextBox about_txt;
    }
}