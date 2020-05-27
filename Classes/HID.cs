using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace SHT
{
    //https://antiduh.com/blog/node/9
    //https://github.com/mentatpsi/Microchip/tree/master/USB/Device%20-%20HID%20-%20Custom%20Demos/PnP%20Demo%20-%20Windows%20Software/Microsoft%20Visual%20CSharp%202005%20Express
    class HID
    {
        #region "Definations"
        //Globally Unique Identifier (GUID) for HID class devices.  Windows uses GUIDs to identify things.
        Guid InterfaceClassGuid = new Guid(0x4d1e55b2, 0xf16f, 0x11cf, 0x88, 0xcb, 0x00, 0x11, 0x11, 0x00, 0x00, 0x30);
        //Constant definitions from setupapi.h, which we aren't allowed to include directly since this is C#
        internal const uint DIGCF_PRESENT = 0x02;
        internal const uint DIGCF_ALLCLASSES = 0x04;
        internal const uint DIGCF_PROFILE = 0x08;
        internal const uint DIGCF_DEVICEINTERFACE = 0x10;
        //Constants for CreateFile() and other file I/O functions
        internal const short FILE_ATTRIBUTE_NORMAL = 0x80;
        internal const short INVALID_HANDLE_VALUE = -1;
        internal const uint GENERIC_READ = 0x80000000;
        internal const uint GENERIC_WRITE = 0x40000000;
        internal const uint CREATE_NEW = 1;
        internal const uint CREATE_ALWAYS = 2;
        internal const uint OPEN_EXISTING = 3;
        internal const uint FILE_SHARE_READ = 0x00000001;
        internal const uint FILE_SHARE_WRITE = 0x00000002;

        //Other constant definitions
        internal const uint DBT_DEVTYP_DEVICEINTERFACE = 0x05;
        internal const uint DEVICE_NOTIFY_WINDOW_HANDLE = 0x00;
        internal const uint ERROR_SUCCESS = 0x00;
        internal const uint ERROR_NO_MORE_ITEMS = 0x00000103;
        internal const uint SPDRP_HARDWAREID = 0x00000001;

        //Various structure definitions for structures that this code will be using
        internal struct SP_DEVICE_INTERFACE_DATA
        {
            internal uint cbSize;               //DWORD
            internal Guid InterfaceClassGuid;   //GUID
            internal uint Flags;                //DWORD
            internal uint Reserved;             //ULONG_PTR MSDN says ULONG_PTR is "typedef unsigned __int3264 ULONG_PTR;"  
        }

        internal struct SP_DEVICE_INTERFACE_DETAIL_DATA
        {
            internal uint cbSize;               //DWORD
            internal char[] DevicePath;         //TCHAR array of any size
        }

        internal struct SP_DEVINFO_DATA
        {
            internal uint cbSize;       //DWORD
            internal Guid ClassGuid;    //GUID
            internal uint DevInst;      //DWORD
            internal uint Reserved;     //ULONG_PTR  MSDN says ULONG_PTR is "typedef unsigned __int3264 ULONG_PTR;"  
        }

        internal struct DEV_BROADCAST_DEVICEINTERFACE
        {
            internal uint dbcc_size;            //DWORD
            internal uint dbcc_devicetype;      //DWORD
            internal uint dbcc_reserved;        //DWORD
            internal Guid dbcc_classguid;       //GUID
            internal char[] dbcc_name;          //TCHAR array
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct HIDP_CAPS
        {
            public short Usage;
            public short UsagePage;
            public short InputReport;
            public short OutputReport;
            public short FeatureReport;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
            public short[] Reserved;
            public short NumberLinkCollectionNodes;
            public short NumberInputButtonCaps;
            public short NumberInputValueCaps;
            public short NumberInputDataIndices;
            public short NumberOutputButtonCaps;
            public short NumberOutputValueCaps;
            public short NumberOutputDataIndices;
            public short NumberFeatureButtonCaps;
            public short NumberFeatureValueCaps;
            public short NumberFeatureDataIndices;

        }
        public struct HIDD_ATTRIBUTES
        {
            public Int32 Size;
            public Int16 VendorID;
            public Int16 ProductID;
            public Int16 VersionNumber;

        }
        #endregion
        #region "Dll Imports"

        //DLL Imports.  Need these to access various C style unmanaged functions contained in their respective DLL files.
        //--------------------------------------------------------------------------------------------------------------
        // HidD_GetManufacturerString
        [DllImport("hid.dll", SetLastError = true)]
        public static extern Boolean HidD_GetManufacturerString(
            SafeFileHandle HidDeviceObject,
            [MarshalAs( UnmanagedType.LPWStr )]
        StringBuilder Buffer,
            UInt32 BufferLength);
        // HidD_GetSerialNumberString
        [DllImport("hid.dll", SetLastError = true)]
        public static extern Boolean HidD_GetSerialNumberString(
            SafeFileHandle HidDeviceObject,
            [MarshalAs( UnmanagedType.LPWStr )]
        StringBuilder Buffer,
            UInt32 BufferLength);
        // HidD_GetProductString
        [DllImport("hid.dll", SetLastError = true)]
        public static extern Boolean HidD_GetProductString(
            SafeFileHandle HidDeviceObject,
            [MarshalAs( UnmanagedType.LPWStr )]
        StringBuilder Buffer,
            UInt32 BufferLength);
        // HidD_GetAttributes
        [DllImport("hid.dll", SetLastError = true)]
        public static extern Boolean HidD_GetAttributes(
              SafeFileHandle HidDeviceObject,
              ref HIDD_ATTRIBUTES Attributes);
        //HidD_FreePreparsedData
        [DllImport("hid.dll", SetLastError = true)]
        public static extern bool HidD_FreePreparsedData(ref IntPtr PreparsedData);
        // HidD_GetPreparsedData
        [DllImport("hid.dll", SetLastError = true)]
        public static extern bool HidD_GetPreparsedData(SafeFileHandle HidDeviceObject, ref IntPtr PreparsedData);
        //HidP_GetCaps
        [DllImport("hid.dll", SetLastError = true)]
        public static extern int HidP_GetCaps(IntPtr preparsedData, ref HIDP_CAPS capabilities);
        //Returns a HDEVINFO type for a device information set.  We will need the 
        //HDEVINFO as in input parameter for calling many of the other SetupDixxx() functions.
        [DllImport("setupapi.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern IntPtr SetupDiGetClassDevs(
            ref Guid ClassGuid,     //LPGUID    Input: Need to supply the class GUID. 
            IntPtr Enumerator,      //PCTSTR    Input: Use NULL here, not important for our purposes
            IntPtr hwndParent,      //HWND      Input: Use NULL here, not important for our purposes
            uint Flags);            //DWORD     Input: Flags describing what kind of filtering to use.

        //Gives us "PSP_DEVICE_INTERFACE_DATA" which contains the Interface specific GUID (different
        //from class GUID).  We need the interface GUID to get the device path.
        [DllImport("setupapi.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern bool SetupDiEnumDeviceInterfaces(
            IntPtr DeviceInfoSet,           //Input: Give it the HDEVINFO we got from SetupDiGetClassDevs()
            IntPtr DeviceInfoData,          //Input (optional)
            ref Guid InterfaceClassGuid,    //Input 
            uint MemberIndex,               //Input: "Index" of the device you are interested in getting the path for.
            ref SP_DEVICE_INTERFACE_DATA DeviceInterfaceData);    //Output: This function fills in an "SP_DEVICE_INTERFACE_DATA" structure.

        //SetupDiDestroyDeviceInfoList() frees up memory by destroying a DeviceInfoList
        [DllImport("setupapi.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern bool SetupDiDestroyDeviceInfoList(
            IntPtr DeviceInfoSet);          //Input: Give it a handle to a device info list to deallocate from RAM.

        //SetupDiEnumDeviceInfo() fills in an "SP_DEVINFO_DATA" structure, which we need for SetupDiGetDeviceRegistryProperty()
        [DllImport("setupapi.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern bool SetupDiEnumDeviceInfo(
            IntPtr DeviceInfoSet,
            uint MemberIndex,
            ref SP_DEVINFO_DATA DeviceInterfaceData);

        //SetupDiGetDeviceRegistryProperty() gives us the hardware ID, which we use to check to see if it has matching VID/PID
        [DllImport("setupapi.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern bool SetupDiGetDeviceRegistryProperty(
            IntPtr DeviceInfoSet,
            ref SP_DEVINFO_DATA DeviceInfoData,
            uint Property,
            ref uint PropertyRegDataType,
            IntPtr PropertyBuffer,
            uint PropertyBufferSize,
            ref uint RequiredSize);

        //SetupDiGetDeviceInterfaceDetail() gives us a device path, which is needed before CreateFile() can be used.
        [DllImport("setupapi.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern bool SetupDiGetDeviceInterfaceDetail(
            IntPtr DeviceInfoSet,                   //Input: Wants HDEVINFO which can be obtained from SetupDiGetClassDevs()
            ref SP_DEVICE_INTERFACE_DATA DeviceInterfaceData,                    //Input: Pointer to an structure which defines the device interface.  
            IntPtr DeviceInterfaceDetailData,      //Output: Pointer to a SP_DEVICE_INTERFACE_DETAIL_DATA structure, which will receive the device path.
            uint DeviceInterfaceDetailDataSize,     //Input: Number of bytes to retrieve.
            ref uint RequiredSize,                  //Output (optional): The number of bytes needed to hold the entire struct 
            IntPtr DeviceInfoData);                 //Output (optional): Pointer to a SP_DEVINFO_DATA structure

        //Overload for SetupDiGetDeviceInterfaceDetail().  Need this one since we can't pass NULL pointers directly in C#.
        [DllImport("setupapi.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern bool SetupDiGetDeviceInterfaceDetail(
            IntPtr DeviceInfoSet,                   //Input: Wants HDEVINFO which can be obtained from SetupDiGetClassDevs()
            ref SP_DEVICE_INTERFACE_DATA DeviceInterfaceData,               //Input: Pointer to an structure which defines the device interface.  
            IntPtr DeviceInterfaceDetailData,       //Output: Pointer to a SP_DEVICE_INTERFACE_DETAIL_DATA structure, which will contain the device path.
            uint DeviceInterfaceDetailDataSize,     //Input: Number of bytes to retrieve.
            IntPtr RequiredSize,                    //Output (optional): Pointer to a DWORD to tell you the number of bytes needed to hold the entire struct 
            IntPtr DeviceInfoData);                 //Output (optional): Pointer to a SP_DEVINFO_DATA structure

        //Need this function for receiving all of the WM_DEVICECHANGE messages.  See MSDN documentation for
        //description of what this function does/how to use it. Note: name is remapped "RegisterDeviceNotificationUM" to
        //avoid possible build error conflicts.
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern IntPtr RegisterDeviceNotification(
            IntPtr hRecipient,
            IntPtr NotificationFilter,
            uint Flags);

        //Takes in a device path and opens a handle to the device.
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        static extern SafeFileHandle CreateFile(
            string lpFileName,
            uint dwDesiredAccess,
            uint dwShareMode,
            IntPtr lpSecurityAttributes,
            uint dwCreationDisposition,
            uint dwFlagsAndAttributes,
            IntPtr hTemplateFile);

        //Uses a handle (created with CreateFile()), and lets us write USB data to the device.
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        static extern bool WriteFile(
            SafeFileHandle hFile,
            byte[] lpBuffer,
            uint nNumberOfBytesToWrite,
            ref uint lpNumberOfBytesWritten,
            IntPtr lpOverlapped);

        //Uses a handle (created with CreateFile()), and lets us read USB data from the device.
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        static extern bool ReadFile(
            SafeFileHandle hFile,
            IntPtr lpBuffer,
            uint nNumberOfBytesToRead,
            ref uint lpNumberOfBytesRead,
            IntPtr lpOverlapped);
        #endregion

        #region "User Definations"
        internal struct Hid_Devices
        {
            internal String DevicePath;
            internal string DeviceName;
            internal string DeviceManufacturer;
            internal string SerialNumber;
            internal HIDD_ATTRIBUTES Attributes;
            internal HIDP_CAPS Caps;
            internal SafeFileHandle WriteHandle;
            internal SafeFileHandle ReadHandle;
            internal bool AttachedState;
            internal bool AttachedButBroken;
            internal byte[] ReadBuffer;
        }
        public List<Hid_Devices> HidDevices { get; set; } = new List<Hid_Devices>();
        Hid_Devices HidDev = new Hid_Devices();
        #endregion
        public HID(IntPtr Handle)

        {
            HidInit(Handle);
        }
        private void HidInit(IntPtr Handle)
        {
            //Additional constructor code

            //Register for WM_DEVICECHANGE notifications.  This code uses these messages to detect plug and play connection/disconnection events for USB devices
            DEV_BROADCAST_DEVICEINTERFACE DeviceBroadcastHeader = new DEV_BROADCAST_DEVICEINTERFACE();
            DeviceBroadcastHeader.dbcc_devicetype = DBT_DEVTYP_DEVICEINTERFACE;
            DeviceBroadcastHeader.dbcc_size = (uint)Marshal.SizeOf(DeviceBroadcastHeader);
            DeviceBroadcastHeader.dbcc_reserved = 0;	//Reserved says not to use...
            DeviceBroadcastHeader.dbcc_classguid = InterfaceClassGuid;

            //Need to get the address of the DeviceBroadcastHeader to call RegisterDeviceNotification(), but
            //can't use "&DeviceBroadcastHeader".  Instead, using a roundabout means to get the address by 
            //making a duplicate copy using Marshal.StructureToPtr().
            IntPtr pDeviceBroadcastHeader = IntPtr.Zero;  //Make a pointer.
            pDeviceBroadcastHeader = Marshal.AllocHGlobal(Marshal.SizeOf(DeviceBroadcastHeader)); //allocate memory for a new DEV_BROADCAST_DEVICEINTERFACE structure, and return the address 
            Marshal.StructureToPtr(DeviceBroadcastHeader, pDeviceBroadcastHeader, false);  //Copies the DeviceBroadcastHeader structure into the memory already allocated at DeviceBroadcastHeaderWithPointer
            RegisterDeviceNotification(Handle, pDeviceBroadcastHeader, DEVICE_NOTIFY_WINDOW_HANDLE);
            //Get All Hid Devices
            GetHidUSBDevices();
        }
        private bool GetHidUSBDevices()
        {
            /* 
           Before we can "connect" our application to our USB embedded device, we must first find the device.
           A USB bus can have many devices simultaneously connected, so somehow we have to find our device only.
           This is done with the Vendor ID (VID) and Product ID (PID).  Each USB product line should have
           a unique combination of VID and PID.  

           Microsoft has created a number of functions which are useful for finding plug and play devices.  Documentation
           for each function used can be found in the MSDN library.  We will be using the following functions (unmanaged C functions):

           SetupDiGetClassDevs()					//provided by setupapi.dll, which comes with Windows
           SetupDiEnumDeviceInterfaces()			//provided by setupapi.dll, which comes with Windows
           GetLastError()							//provided by kernel32.dll, which comes with Windows
           SetupDiDestroyDeviceInfoList()			//provided by setupapi.dll, which comes with Windows
           SetupDiGetDeviceInterfaceDetail()		//provided by setupapi.dll, which comes with Windows
           SetupDiGetDeviceRegistryProperty()		//provided by setupapi.dll, which comes with Windows
           CreateFile()							//provided by kernel32.dll, which comes with Windows

           In order to call these unmanaged functions, the Marshal class is very useful.

           We will also be using the following unusual data types and structures.  Documentation can also be found in
           the MSDN library:

           PSP_DEVICE_INTERFACE_DATA
           PSP_DEVICE_INTERFACE_DETAIL_DATA
           SP_DEVINFO_DATA
           HDEVINFO
           HANDLE
           GUID

           The ultimate objective of the following code is to get the device path, which will be used elsewhere for getting
           read and write handles to the USB device.  Once the read/write handles are opened, only then can this
           PC application begin reading/writing to the USB device using the WriteFile() and ReadFile() functions.

           Getting the device path is a multi-step round about process, which requires calling several of the
           SetupDixxx() functions provided by setupapi.dll.
           */

            HidDevices.Clear();
            string DevicePath = "";

            try
            {
                IntPtr DeviceInfoTable = IntPtr.Zero;
                SP_DEVICE_INTERFACE_DATA InterfaceDataStructure = new SP_DEVICE_INTERFACE_DATA();
                SP_DEVICE_INTERFACE_DETAIL_DATA DetailedInterfaceDataStructure = new SP_DEVICE_INTERFACE_DETAIL_DATA();
                SP_DEVINFO_DATA DevInfoData = new SP_DEVINFO_DATA();

                uint InterfaceIndex = 0;
                uint dwRegType = 0;
                uint dwRegSize = 0;
                uint StructureSize = 0;
                IntPtr PropertyValueBuffer = IntPtr.Zero;
                uint ErrorStatus;
                uint LoopCounter = 0;
                //First populate a list of plugged in devices (by specifying "DIGCF_PRESENT"), which are of the specified class GUID. 
                DeviceInfoTable = SetupDiGetClassDevs(ref InterfaceClassGuid, IntPtr.Zero, IntPtr.Zero, DIGCF_PRESENT | DIGCF_DEVICEINTERFACE);

                if (DeviceInfoTable != IntPtr.Zero)
                {
                    //Now look through the list we just populated.  We are trying to see if any of them match our device. 
                    while (true)
                    {
                        InterfaceDataStructure.cbSize = (uint)Marshal.SizeOf(InterfaceDataStructure);
                        if (SetupDiEnumDeviceInterfaces(DeviceInfoTable, IntPtr.Zero, ref InterfaceClassGuid, InterfaceIndex, ref InterfaceDataStructure))
                        {
                            ErrorStatus = (uint)Marshal.GetLastWin32Error();
                            if (ErrorStatus == ERROR_NO_MORE_ITEMS) //Did we reach the end of the list of matching devices in the DeviceInfoTable?
                            {   //Cound not find the device.  Must not have been attached.
                                SetupDiDestroyDeviceInfoList(DeviceInfoTable);  //Clean up the old structure we no longer need.
                                return false;
                            }
                        }
                        else    //Else some other kind of unknown error ocurred...
                        {
                            ErrorStatus = (uint)Marshal.GetLastWin32Error();
                            SetupDiDestroyDeviceInfoList(DeviceInfoTable);  //Clean up the old structure we no longer need.
                            return HidDevices.Count > 0;
                        }

                        //Now retrieve the hardware ID from the registry.  The hardware ID contains the VID and PID, which we will then 
                        //check to see if it is the correct device or not.

                        //Initialize an appropriate SP_DEVINFO_DATA structure.  We need this structure for SetupDiGetDeviceRegistryProperty().
                        DevInfoData.cbSize = (uint)Marshal.SizeOf(DevInfoData);
                        SetupDiEnumDeviceInfo(DeviceInfoTable, InterfaceIndex, ref DevInfoData);

                        //First query for the size of the hardware ID, so we can know how big a buffer to allocate for the data.
                        SetupDiGetDeviceRegistryProperty(DeviceInfoTable, ref DevInfoData, SPDRP_HARDWAREID, ref dwRegType, IntPtr.Zero, 0, ref dwRegSize);


                        //Device must have been found.  In order to open I/O file handle(s), we will need the actual device path first.
                        //We can get the path by calling SetupDiGetDeviceInterfaceDetail(), however, we have to call this function twice:  The first
                        //time to get the size of the required structure/buffer to hold the detailed interface data, then a second time to actually 
                        //get the structure (after we have allocated enough memory for the structure.)
                        DetailedInterfaceDataStructure.cbSize = (uint)Marshal.SizeOf(DetailedInterfaceDataStructure);
                        //First call populates "StructureSize" with the correct value
                        SetupDiGetDeviceInterfaceDetail(DeviceInfoTable, ref InterfaceDataStructure, IntPtr.Zero, 0, ref StructureSize, IntPtr.Zero);
                        //Need to call SetupDiGetDeviceInterfaceDetail() again, this time specifying a pointer to a SP_DEVICE_INTERFACE_DETAIL_DATA buffer with the correct size of RAM allocated.
                        //First need to allocate the unmanaged buffer and get a pointer to it.
                        IntPtr pUnmanagedDetailedInterfaceDataStructure = IntPtr.Zero;  //Declare a pointer.
                        pUnmanagedDetailedInterfaceDataStructure = Marshal.AllocHGlobal((int)StructureSize);    //Reserve some unmanaged memory for the structure.
                        DetailedInterfaceDataStructure.cbSize = 6; //Initialize the cbSize parameter (4 bytes for DWORD + 2 bytes for unicode null terminator)
                        Marshal.StructureToPtr(DetailedInterfaceDataStructure, pUnmanagedDetailedInterfaceDataStructure, false); //Copy managed structure contents into the unmanaged memory buffer.

                        //Now call SetupDiGetDeviceInterfaceDetail() a second time to receive the device path in the structure at pUnmanagedDetailedInterfaceDataStructure.
                        if (SetupDiGetDeviceInterfaceDetail(DeviceInfoTable, ref InterfaceDataStructure, pUnmanagedDetailedInterfaceDataStructure, StructureSize, IntPtr.Zero, IntPtr.Zero))
                        {
                            //Need to extract the path information from the unmanaged "structure".  The path starts at (pUnmanagedDetailedInterfaceDataStructure + sizeof(DWORD)).
                            IntPtr pToDevicePath = new IntPtr((uint)pUnmanagedDetailedInterfaceDataStructure.ToInt32() + 4);  //Add 4 to the pointer (to get the pointer to point to the path, instead of the DWORD cbSize parameter)
                            DevicePath = Marshal.PtrToStringUni(pToDevicePath); //Now copy the path information into the globally defined DevicePath String.

                            //We now have the proper device path, and we can finally use the path to open I/O handle(s) to the device.
                            //SetupDiDestroyDeviceInfoList(DeviceInfoTable);	//Clean up the old structure we no longer need.
                            Marshal.FreeHGlobal(pUnmanagedDetailedInterfaceDataStructure);  //No longer need this unmanaged SP_DEVICE_INTERFACE_DETAIL_DATA buffer.  We already extracted the path information.
                            uint ErrorStatusWrite;
                            uint ErrorStatusRead;
                            HidDev.DevicePath = DevicePath;

                            HidDev.WriteHandle = CreateFile(DevicePath, GENERIC_WRITE, FILE_SHARE_READ | FILE_SHARE_WRITE, IntPtr.Zero, OPEN_EXISTING, 0, IntPtr.Zero);
                            ErrorStatusWrite = (uint)Marshal.GetLastWin32Error();
                            HidDev.ReadHandle = CreateFile(DevicePath, GENERIC_READ, FILE_SHARE_READ | FILE_SHARE_WRITE, IntPtr.Zero, OPEN_EXISTING, 0, IntPtr.Zero);
                            ErrorStatusRead = (uint)Marshal.GetLastWin32Error();
                            if ((ErrorStatusWrite == ERROR_SUCCESS) && (ErrorStatusRead == ERROR_SUCCESS))
                            {
                                HidDev.Caps = new HIDP_CAPS();
                                IntPtr preparsedData = new IntPtr();
                                HidD_GetPreparsedData(HidDev.WriteHandle, ref preparsedData);
                                HidP_GetCaps(preparsedData, ref HidDev.Caps);
                                HidD_FreePreparsedData(ref preparsedData);
                                HidD_GetAttributes(HidDev.WriteHandle, ref HidDev.Attributes);
                                StringBuilder Builder = new StringBuilder(253);

                                bool success = HidD_GetSerialNumberString(HidDev.WriteHandle, Builder, (uint)Builder.Capacity);
                                if (success)
                                    HidDev.SerialNumber = Builder.ToString();

                                success = HidD_GetProductString(HidDev.WriteHandle, Builder, (uint)Builder.Capacity);
                                if (success)
                                    HidDev.DeviceName = Builder.ToString();

                                success = HidD_GetManufacturerString(HidDev.WriteHandle, Builder, (uint)Builder.Capacity);
                                if (success)
                                    HidDev.DeviceManufacturer = Builder.ToString();

                            }
                            else //for some reason the device was physically plugged in, but one or both of the read/write handles didn't open successfully...
                            {
                                HidDev.AttachedState = false;      //Let the rest of this application known not to read/write to the device.
                                HidDev.AttachedButBroken = true;   //Flag so that next time a WM_DEVICECHANGE message occurs, can retry to re-open read/write pipes
                                if (ErrorStatusWrite == ERROR_SUCCESS)
                                    HidDev.WriteHandle.Close();
                                if (ErrorStatusRead == ERROR_SUCCESS)
                                    HidDev.ReadHandle.Close();
                            }
                            HidDev.ReadBuffer = new byte[HidDev.Caps.InputReport];
                            if (HidDev.DeviceName != null)
                                HidDevices.Add(HidDev);
                        }
                        else //Some unknown failure occurred
                        {
                            uint ErrorCode = (uint)Marshal.GetLastWin32Error();
                            SetupDiDestroyDeviceInfoList(DeviceInfoTable);  //Clean up the old structure.
                            Marshal.FreeHGlobal(pUnmanagedDetailedInterfaceDataStructure);  //No longer need this unmanaged SP_DEVICE_INTERFACE_DETAIL_DATA buffer.  We already extracted the path information.
                            return false;
                        }

                        InterfaceIndex++;
                        //Keep looping until we either find a device with matching VID and PID, or until we run out of devices to check.
                        //However, just in case some unexpected error occurs, keep track of the number of loops executed.
                        //If the number of loops exceeds a very large number, exit anyway, to prevent inadvertent infinite looping.
                        LoopCounter++;
                        if (LoopCounter == 10000000)    //Surely there aren't more than 10 million devices attached to any forseeable PC...
                        {
                            return false;
                        }
                    }//end of while(true)
                }
                return false;
            }//end of try
            catch
            {
                //Something went wrong if PC gets here.  Maybe a Marshal.AllocHGlobal() failed due to insufficient resources or something.
                return false;
            }

        }
    }
}
