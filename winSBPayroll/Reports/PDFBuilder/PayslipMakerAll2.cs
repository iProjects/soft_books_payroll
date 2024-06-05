using System;
using System.Collections.Generic;
using System.IO;
using BLL;
using System.Drawing;
using BLL.DataEntry;
using CommonLib;
using System.Windows.Forms;
//Payroll
using DAL;
//--- Add the following to make itext work
using iTextSharp.text;
using iTextSharp.text.pdf;
using VVX;

namespace winSBPayroll.Reports.PDF
{
    public class PayslipMakerAll2
    {
        List<Payslip> payslipList;
        Document document;
        string Message;
        string sFilePDF;
        DataEntry de;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;
        event EventHandler<notificationmessageEventArgs> _notificationmessageEventname;
        string TAG;

        public PayslipMakerAll2(List<Payslip> PayslipList, string FileName, string Conn, EventHandler<notificationmessageEventArgs> notificationmessageEventname)
        {
            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            de = new DataEntry(connection);
            db = new SBPayrollDBEntities(connection);
            rep = new Repository(connection);

            if (PayslipList == null)
                throw new ArgumentNullException("Payslip List is null");
            payslipList = PayslipList;

            _notificationmessageEventname = notificationmessageEventname;

            sFilePDF = FileName;
        }

        public string GetPDF()
        {
            BuildPDF();
            return sFilePDF;
        }

        private void BuildPDF()
        {
            try
            {
                // step 1: creation of a document-object
                document = new Document(PageSize.A4, 10, 10, 10, 10);

                // step 2: we create a writer that listens to the document
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(sFilePDF, FileMode.Create));

                //open document
                document.Open();

                IEnumerable<Tuple<Payslip, Payslip>> PairedPayslips = payslipList.AsPairs();

                foreach (var payslip in PairedPayslips)
                {
                    var payslip1 = payslip.Item1;
                    var payslip2 = payslip.Item2;

                    MakePayslipPDF3 pm = new MakePayslipPDF3(payslip1, payslip2, document, connection, _notificationmessageEventname);
                    pm.BuildPDF();
                    document.NewPage();
                }

                //Finished
                document.Close();
            }
            catch (DocumentException de)
            {
                this.Message = de.Message;
            }
            catch (IOException ioe)
            {
                this.Message = ioe.Message;
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }
        }


    }


    public static class MyExtensions
    {
        // Create Pairs from a list. If the list is odd add a default value for the final pair.
        public static IEnumerable<Tuple<T, T>> AsPairs<T>(this List<T> list)
        {
            int index = 0;

            while (index < list.Count)
            {
                if (index + 1 > list.Count)
                    yield break;

                if (index + 1 == list.Count)
                    yield return new Tuple<T, T>(list[index++], default(T));
                else
                    yield return new Tuple<T, T>(list[index++], list[index++]);
            }
        }

        // Create Pairs from a list. Note if the list is not even in count, the last value is skipped.
        public static IEnumerable<Tuple<T, T>> AsPairsSafe<T>(this List<T> list)
        {
            int index = 0;

            while (index < list.Count)
            {
                if (index + 1 >= list.Count)
                    yield break;

                yield return new Tuple<T, T>(list[index++], list[index++]);
            }

        }
    }


}
