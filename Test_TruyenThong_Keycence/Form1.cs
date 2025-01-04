using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PLC_Communication.NET;

namespace Test_TruyenThong_Keycence
{
    public partial class Form1 : Form
    {
        //Sử dụng kết nối Ethernet; IP PLC: 192.168.1.10; Loại kết nối Udp
        KeyenceTCPHostLink cmd=new KeyenceTCPHostLink("192.168.1.10",8051,System.Net.Sockets.ProtocolType.Udp);

        //Sử dụng kết nối RS232
        static SerialPort serialPort = new SerialPort() { PortName="COM1", BaudRate=9600, Parity=Parity.Even, StopBits=StopBits.One, DataBits=8 };
        KeyenceSerialHostLink2 cmd_serial = new KeyenceSerialHostLink2(serialPort);
        
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*==========================Sử dụng kết nối Ethernet=========================================*/

            //ghi giá trị 15 vào DM1000
            bool ketqua1 =cmd.Write(KVDeviceCode.DM,1000,KVDataFormat.S,15);

            //ghi 5 giá trị trong mảng vào DM1000-DM1004
            int[] mangGiaTri = new int[] { 1, 2, 3, 4, 5 };
            bool ketqua2 = cmd.Writes(KVDeviceCode.DM, 1000, KVDataFormat.S, mangGiaTri);

            //ghi 5 bit trong mảng vào MR1000-DM1004; (1=ON, 0=OFF)
            int[] mang_bit = new int[] { 1, 1, 0, 0, 1 };
            bool ketqua3 = cmd.Writes(KVDeviceCode.MR, 1000, KVDataFormat.Bit, mang_bit);


            //đọc dữ liệu DM1000-DM1010=> kết quả trả vào mảng
            string[] ketqua4 = cmd.Reads(KVDeviceCode.DM, 1000, KVDataFormat.S,10);


            /*==========================Sử dụng kết nối RS232=============================================*/

            //ghi giá trị 15 vào DM1000
            bool ketqua5 = cmd_serial.Write(KVDeviceCode.DM, 1000, KVDataFormat.S, 15);

            //ghi 5 giá trị trong mảng vào DM1000-DM1004
            int[] mangGiaTri2 = new int[] { 1, 2, 3, 4, 5 };
            bool ketqua6 = cmd_serial.Writes(KVDeviceCode.DM, 1000, KVDataFormat.S, mangGiaTri2);

            //đọc dữ liệu DM1000-DM1010=> kết quả trả vào mảng
            string[] ketqua7 = cmd_serial.Reads(KVDeviceCode.DM, 1000, KVDataFormat.S, 10);


        }
    }
}
