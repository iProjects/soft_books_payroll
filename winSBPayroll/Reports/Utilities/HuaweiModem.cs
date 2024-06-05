using System; 
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks; 
using GsmComm.GsmCommunication;
using GsmComm.Interfaces;
using GsmComm.PduConverter;
using GsmComm.Server;

namespace fMessagingSystem.ServiceAgent.ModemService
{
    public class HuaweiModem
    {
        #region "public properties"
        public static Int16 Comm_Port = 0;
        public static Int32 Comm_BaudRate = 0;
        public static Int32 Comm_TimeOut = 0;
        public static GsmCommMain comm;
        #endregion "public properties"

        #region "private properties"
        private string Manufacturer;
        private string Model;
        private string Revision;
        private string SerialNumber;
       private  string _Output;
        #endregion "private properties"

        #region "constructor"
        public HuaweiModem()
        {
            Comm_Port = Convert.ToInt16(ConfigurationManager.AppSettings["Comm_Port"]);
            Comm_BaudRate = Convert.ToInt32(ConfigurationManager.AppSettings["Comm_BaudRate"]);
            Comm_TimeOut = Convert.ToInt32(ConfigurationManager.AppSettings["Comm_TimeOut"]);

            Connect();
        }
        #endregion "constructor"

        #region "public methods"
        public bool Connect()
        {
            comm = new GsmCommMain(Comm_Port, Comm_BaudRate, Comm_TimeOut);
            bool connected = false;
            comm.Open();

            if (comm.IsConnected())
            {
                Manufacturer = comm.IdentifyDevice().Manufacturer.ToUpper().ToString();
                Model = comm.IdentifyDevice().Model.ToUpper().ToString();
                Revision = comm.IdentifyDevice().Revision.ToUpper().ToString();
                SerialNumber = comm.IdentifyDevice().SerialNumber.ToUpper().ToString();
                connected = true;
                Debug.Write("Manufacturer " + Manufacturer + Environment.NewLine + "Model " + Model + Environment.NewLine + "Revision " + Revision + Environment.NewLine + "SerialNumber " + SerialNumber + Environment.NewLine + "connected " + connected.ToString());

            }
            return connected;
        }
        public void SendSMS(string CELL_Number, string SMS_Message)
        {
            SmsSubmitPdu pdu1;
            if (comm.IsConnected() == true)
            {
                pdu1 = new SmsSubmitPdu(SMS_Message, CELL_Number, "");

                comm.SendMessage(pdu1);
                comm.Close();
            }
        }
        public void ReadReceivedSMS()
        {
            if (comm.IsConnected() == true)
            {
                DecodedShortMessage[] messages = comm.ReadMessages
                               (PhoneMessageStatus.All, PhoneStorageType.Sim);
                foreach (DecodedShortMessage message in messages)
                {
                    ShowMessage(message.Data);

                    //string Output = string.Format("Message status = {0}, Location =  {1}/{2}, ActualLength =  {3}, DataCodingScheme =  {4}, ProtocolID =  {5}, SmscAddress =  {6}, SmscAddressType =  {7}, TotalLength =  {8}, UserData =  {9}, UserDataLength =  {10}, UserDataText =  {11}", Convert.ToString(message.Status), Convert.ToString(message.Storage), Convert.ToString(message.Index), Convert.ToString(message.Data.ActualLength), Convert.ToString(message.Data.DataCodingScheme), Convert.ToString(message.Data.ProtocolID), Convert.ToString(message.Data.SmscAddress), Convert.ToString(message.Data.SmscAddressType), Convert.ToString(message.Data.TotalLength), Convert.ToString(message.Data.UserData), Convert.ToString(message.Data.UserDataLength), Convert.ToString(message.Data.UserDataText));
                    //Debug.WriteLine(Output);
                }
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\Public\Downloads\testworm.txt"))
                {
                    file.WriteLine(_Output);
                }
                comm.Close();
            }
        }
        private void ReceiveMessage()
        {
            if (comm.IsConnected() == true)
            {
                DecodedShortMessage[] messages = comm.ReadMessages(PhoneMessageStatus.All, PhoneStorageType.Phone);
                foreach (DecodedShortMessage message in messages)
                {
                    Output(string.Format("Message status = {0}, Location = {1}/{2}",
                        StatusToString(message.Status), message.Storage, message.Index));
                    ShowMessage(message.Data);
                    Output("");
                }
                comm.Close();
            }
        }
        private string StatusToString(PhoneMessageStatus status)
        {
            // Map a message status to a string 
            string ret;
            switch (status)
            {
                case PhoneMessageStatus.All:
                    ret = "All";
                    break;
                case PhoneMessageStatus.ReceivedRead:
                    ret = "Read";
                    break;
                case PhoneMessageStatus.ReceivedUnread:
                    ret = "Unread";
                    break;
                case PhoneMessageStatus.StoredSent:
                    ret = "Sent";
                    break;
                case PhoneMessageStatus.StoredUnsent:
                    ret = "Unsent";
                    break;
                default:
                    ret = "Unknown (" + status.ToString() + ")";
                    break;
            }
            return ret;
        }
        private void ShowMessage(SmsPdu pdu)
        {
            if (pdu is SmsSubmitPdu)
            {
                // Stored (sent/unsent) message 
                SmsSubmitPdu data = (SmsSubmitPdu)pdu;
                Output("SENT/UNSENT MESSAGE");
                Output("Recipient: " + data.DestinationAddress);
                Output("Message text: " + data.UserDataText);
                Output("-------------------------------------------------------------------");
                return;
            }
            if (pdu is SmsDeliverPdu)
            {
                // Received message 
                SmsDeliverPdu data = (SmsDeliverPdu)pdu;
                Output("RECEIVED MESSAGE");
                Output("Sender: " + data.OriginatingAddress);
                Output("Sent: " + data.SCTimestamp.ToString());
                Output("Message text: " + data.UserData.ToString());
                Output("-------------------------------------------------------------------");
                return;
            }
            if (pdu is SmsStatusReportPdu)
            {
                // Status report 
                SmsStatusReportPdu data = (SmsStatusReportPdu)pdu;
                Output("STATUS REPORT");
                Output("Recipient: " + data.RecipientAddress);
                Output("Status: " + data.Status.ToString());
                Output("Timestamp: " + data.DischargeTime.ToString());
                Output("Message ref: " + data.MessageReference.ToString());
                Output("-------------------------------------------------------------------");
                return;
            }
            Output("Unknown message type: " + pdu.GetType().ToString());
        }
        private void Output(string text)
        {
            _Output += text+ Environment.NewLine;
        }
        #endregion "public methods"




    }
}