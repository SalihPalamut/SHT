using SHT.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using static SHT.HID;
namespace SHT
{
    public partial class SHT : Form
    {
        #region "Initialize"
        HID UsbHid;
        public SHT()
        {
            InitializeComponent();
            Icon = Resources.AppIcon;
            UsbHid = new HID(this.Handle);
        }

        private void SHT_Load(object sender, EventArgs e)
        {
            // Set to details view.
            HidDevices.View = View.Details;
            // Add a column with width 20 and left alignment.
            HidDevices.Columns.Add("No");
            HidDevices.Columns.Add("Name");
            HidDevices.Columns.Add("Procuder");
            HidDevices.Columns.Add("Vid");
            HidDevices.Columns.Add("Pid");
            HidDevices.Columns.Add("Input");
            HidDevices.Columns.Add("Output");
           
            int i = 1;
            foreach (Hid_Devices Hd in UsbHid.HidDevices)
            {
                ListViewItem item = new ListViewItem(new string[] 
                {
                    i.ToString(),
                    Hd.DeviceName,
                    Hd.DeviceManufacturer,
                    String.Format("{0:X4}",Hd.Attributes.VendorID),
                    String.Format("{0:X4}",Hd.Attributes.ProductID),
                    (Hd.Caps.InputReport-(Hd.Caps.InputReport>0?1:0)).ToString(),//Real Size -1 , First byte must be report number
                    (Hd.Caps.OutputReport-(Hd.Caps.OutputReport>0?1:0)).ToString()//Real Size -1 , First byte must be report number
                });
                HidDevices.Items.Add(item);
                i++;
            }
            HidDevices.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }
        #endregion
        #region "WndProc() "
        //Constant definitions for certain WM_DEVICECHANGE messages
        internal const uint WM_DEVICECHANGE = 0x0219;
        internal const uint DBT_DEVICEARRIVAL = 0x8000;
        internal const uint DBT_DEVICEREMOVEPENDING = 0x8003;
        internal const uint DBT_DEVICEREMOVECOMPLETE = 0x8004;
        internal const uint DBT_CONFIGCHANGED = 0x0018;
        //This is a callback function that gets called when a Windows message is received by the form.
        //We will receive various different types of messages, but the ones we really want to use are the WM_DEVICECHANGE messages.
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_DEVICECHANGE)
            {
                //Device Add or Remove
                if (((int)m.WParam == DBT_DEVICEARRIVAL) || ((int)m.WParam == DBT_DEVICEREMOVEPENDING) || ((int)m.WParam == DBT_DEVICEREMOVECOMPLETE) || ((int)m.WParam == DBT_CONFIGCHANGED))
                {

                }
            } //end of: if(m.Msg == WM_DEVICECHANGE)

            base.WndProc(ref m);
        } //end of: WndProc() function
          //-------------------------------------------------------END CUT AND PASTE BLOCK-------------------------------------------------------------------------------------
          //-------------------------------------------------------------------------------------------------------------------------------------------------------------------

        #endregion
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About abt = new About();
            abt.ShowDialog();
        }
    }
}
