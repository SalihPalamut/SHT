using SHT.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Deployment.Application;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using static SHT.HID;
namespace SHT
{
    public partial class SHT : Form
    {
        #region "Initialize & DeInitialize"
        HID UsbHid;
        Hid_Devices SelectedHid;
        public SHT()
        {
            InitializeComponent();
            Icon = Resources.AppIcon;
            UsbHid = new HID(this.Handle);
        }
        private void fill_view()
        {
            HidDevices.Items.Clear();
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

            fill_view();
        }
        private void SHT_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (Hid_Devices Hd in UsbHid.HidDevices)
            {
                if (!Hd.WriteHandle.IsClosed) Hd.WriteHandle.Close();
                if (!Hd.ReadHandle.IsClosed) Hd.ReadHandle.Close();
            }
        }
        #endregion
        #region "User InterFace"
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About abt = new About();
            abt.ShowDialog();
        }
        private void HidDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (HidDevices.SelectedItems.Count < 1) return;

            ReadThread_Stop();

            SelectedHid = UsbHid.HidDevices[HidDevices.Items.IndexOf(HidDevices.SelectedItems[0])];
            SendGroup.Enabled = !SelectedHid.WriteHandle.IsClosed;
            SendGroup.Enabled &= (SelectedHid.Caps.OutputReport > 0);
            Logs.Text = "";

            if (!ReadThread.IsBusy)
                ReadThread.RunWorkerAsync();
        }
        #endregion
        #region "Reading Data"
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
                    UsbHid.GetHidUSBDevices();
                    fill_view();
                    SendGroup.Enabled = false;
                    Logs.Clear();
                    ReadThread_Stop();
                    send_data.Clear();
                }
            } //end of: if(m.Msg == WM_DEVICECHANGE)

            base.WndProc(ref m);
        } //end of: WndProc() function
          //-------------------------------------------------------END CUT AND PASTE BLOCK-------------------------------------------------------------------------------------
          //-------------------------------------------------------------------------------------------------------------------------------------------------------------------

        #endregion
        BackgroundWorker ReadThread;
        private void ReadThread_Stop()
        {
            if (ReadThread != null)
            {
                ReadThread.CancelAsync();
                ReadThread.Dispose();
                SelectedHid.ReadHandle = null;
            }
            UsbHid.HidReadCancel(ref SelectedHid);
            ReadThread = new BackgroundWorker();
            ReadThread.WorkerSupportsCancellation = true;
            ReadThread.DoWork += ReadThread_DoWork;
        }
        delegate void Dump_Delegate(string Label, Byte[] array);
        void Dump(string Label, Byte[] array) //TextBox Kontrolünün Text ini değiştirecek metod
        {

            if (Logs.InvokeRequired) //TextBox kontrolümüze farklı bir thread üzerinden erişiliyorsa
            {
                Dump_Delegate del = new Dump_Delegate(Dump); //yeni bir temsilci tanımlıyoruz
                Logs.Invoke(del, new object[] { Label, array }); //Invoke ve Temsilci ile metodu çağırıyoruz
            }
            else
            {
                Logs.Text += Label + " : ";
                string format = RcvHex.Checked ? "0x{0:X2} " : "{0}";
                for (int i = 1; i < array.Length; i++)
                    Logs.Text += String.Format(format, array[i]);
                Logs.Text += "\n";
            }
        }
        private void ReadThread_DoWork(object sender, DoWorkEventArgs e)
        {
            //-------------------------------------------------------------------------------------------------------------------------------------------------------------------
            //-------------------------------------------------------BEGIN CUT AND PASTE BLOCK-----------------------------------------------------------------------------------

            /*This thread does the actual USB read/write operations (but only when AttachedState == true) to the USB device.
            It is generally preferrable to write applications so that read and write operations are handled in a separate
            thread from the main form.  This makes it so that the main form can remain responsive, even if the I/O operations
            take a very long time to complete.

            Since this is a separate thread, this code below executes independently from the rest of the
            code in this application.  All this thread does is read and write to the USB device.  It does not update
            the form directly with the new information it obtains (such as the ANxx/POT Voltage or pushbutton state).
            The information that this thread obtains is stored in atomic global variables.
            Form updates are handled by the FormUpdateTimer Tick event handler function.

            This application sends packets to the endpoint buffer on the USB device by using the "WriteFile()" function.
            This application receives packets from the endpoint buffer on the USB device by using the "ReadFile()" function.
            Both of these functions are documented in the MSDN library.  Calling ReadFile() is a not perfectly straight
            foward in C# environment, since one of the input parameters is a pointer to a buffer that gets filled by ReadFile().
            The ReadFile() function is therefore called through a wrapper function ReadFileManagedBuffer().

            All ReadFile() and WriteFile() operations in this example project are synchronous.  They are blocking function
            calls and only return when they are complete, or if they fail because of some event, such as the user unplugging
            the device.  It is possible to call these functions with "overlapped" structures, and use them as non-blocking
            asynchronous I/O function calls.  

            Note:  This code may perform differently on some machines when the USB device is plugged into the host through a
            USB 2.0 hub, as opposed to a direct connection to a root port on the PC.  In some cases the data rate may be slower
            when the device is connected through a USB 2.0 hub.  This performance difference is believed to be caused by
            the issue described in Microsoft knowledge base article 940021:
            http://support.microsoft.com/kb/940021/en-us 

            Higher effective bandwidth (up to the maximum offered by interrupt endpoints), both when connected
            directly and through a USB 2.0 hub, can generally be achieved by queuing up multiple pending read and/or
            write requests simultaneously.  This can be done when using	asynchronous I/O operations (calling ReadFile() and
            WriteFile()	with overlapped structures).  The Microchip	HID USB Bootloader application uses asynchronous I/O
            for some USB operations and the source code can be used	as an example.*/


            //    Byte[] OUTBuffer = new byte[5];	//Allocate a memory buffer equal to the OUT endpoint size + 1

            BackgroundWorker worker = sender as BackgroundWorker;
            while (true)
            {

                try
                {

                    if (SelectedHid.AttachedState == true)	//Do not try to use the read/write handles unless the USB device is attached and ready
                    {
                        if (UsbHid.HidRead(ref SelectedHid))     //Blocking function, unless an "overlapped" structure is used	
                        {
                            Dump("Read Data", SelectedHid.ReadBuffer);
                        }
                    }//end of: if(AttachedState == true)
                    else
                    {
                        Thread.Sleep(5);    //Add a small delay.  Otherwise, this while(true) loop can execute very fast and cause 
                                            //high CPU utilization, with no particular benefit to the application.
                    }
                }
                catch (Exception ex)
                {
                    //Exceptions can occur during the read or write operations.  For example,
                    //exceptions may occur if for instance the USB device is physically unplugged
                    //from the host while the above read/write functions are executing.
                    MessageBox.Show(ex.Message);
                    //Don't need to do anything special in this case.  The application will automatically
                    //re-establish communications based on the global AttachedState boolean variable used
                    //in conjunction with the WM_DEVICECHANGE messages to dyanmically respond to Plug and Play
                    //USB connection events.
                }
                //Cancel Working
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
            } //end of while(true) loop
            //-------------------------------------------------------END CUT AND PASTE BLOCK-------------------------------------------------------------------------------------
            //-------------------------------------------------------------------------------------------------------------------------------------------------------------------
        }
        private void Logs_TextChanged(object sender, EventArgs e)
        {
            Logs.Focus();
            Logs.Select(Logs.Text.Length, 0);
        }
        #endregion
        #region "Send Data Format and Sending Data"
        // Accept Hex inputs
        private void send_data_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 8) return;

            if (SndHex.Checked)
            {
                if(send_data.TextLength==0&&e.KeyChar==' ')
                {
                    e.Handled = true;
                    return;
                }

                if (send_data.TextLength >= (SelectedHid.Caps.OutputReport - 1) * 3 + 2)
                {
                    e.Handled = true;
                    return;
                }
                
                char c = e.KeyChar;
                if ((c < '0' || c > '9') &&
                    (c != ' ') && (c < 'A' || c > 'F') && (c < 'a' || c > 'f'))
                {
                    e.Handled = true;
                }
            }
            else
            {
                if (send_data.TextLength > SelectedHid.Caps.OutputReport) e.Handled = true;
            }
        }
        private void send_data_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                send.PerformClick();
            }
        }

        private void send_data_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData < Keys.D0) return;
            if (!SndHex.Checked) return;
            int a = send_data.TextLength;
            string b = send_data.Text;
            if (a < 3) return;
            a -= 1;
            if (b[a] != ' ' && b[a - 1] != ' ' && b[a - 2] != ' ')
                b = b.Insert(a, " ");
            send_data.Text = b;
            send_data.Focus();
            send_data.Select(send_data.Text.Length, 0);
        }
        private void SndHex_CheckedChanged(object sender, EventArgs e)
        {
            send_data.Text = "";
            append.Enabled = !SndHex.Checked;
        }

        private void send_Click(object sender, EventArgs e)
        {
            short Length = SelectedHid.Caps.OutputReport;

            if (Length < 1) return;

            byte[] data = new byte[Length];
            data[0] = 0;

            if (SndHex.Checked)
            {
                string[] split = send_data.Text.Split(' ');
                int i = 0;

                if (split.Length < Length)
                    i = 1;

                foreach (string hex in split)
                {
                    if (hex.Length < 3 && hex.Length > 0 && i < Length)
                        data[i++] = Convert.ToByte(hex, 16);
                }
            }
            else
            {
                int j = 0, n = Length;
                if (send_data.Text.Length < Length)
                {
                    j = 1;
                    n = send_data.Text.Length;
                }

                for (int i = 0; i < n; i++)
                    data[i + j] = (byte)send_data.Text[i];

                if (CR.Checked && LF.Checked)
                {
                    data[Length - 2] = (byte)'\r';
                    data[Length - 1] = (byte)'\n';
                }
                else
                {
                    if (CR.Checked) data[Length - 1] = (byte)'\r';
                    if (LF.Checked) data[Length - 1] = (byte)'\n';
                }
            }
            UsbHid.HidWrite(SelectedHid, data);
        }
        #endregion
    }
}
