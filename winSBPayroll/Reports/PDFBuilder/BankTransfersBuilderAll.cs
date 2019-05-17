using System; 
using System.Collections.Generic;
using System.Drawing; 
using System.IO;
using System.Text;
using System.Windows.Forms;
using BLL; 
using BLL.DataEntry;
//Payroll
using BLL.KRA.Models; 
using CommonLib;
//Payroll
using DAL; 
//--- Add the following to make itext work
using iTextSharp.text;
using iTextSharp.text.pdf; 
using VVX;

namespace winSBPayroll.Reports.PDF
{
    public class BankTransfersBuilderAll
    {
        BankTransferReportModel _bankTransfers;
        Document document;
        string Message;
        string sFilePDF;
        DataEntry de;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;

        public BankTransfersBuilderAll(BankTransferReportModel bankTransfers, string FileName, string Conn)
        {
            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            de = new DataEntry(connection);
            db = new SBPayrollDBEntities(connection);
            rep = new Repository(connection);

            if (bankTransfers == null)
                throw new ArgumentNullException("BankTransferReportModel  is null");
            _bankTransfers = bankTransfers;

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

                //IEnumerable<Tuple<Payslip, Payslip>> PairedPayslips = payslipList.AsPairs();

                //foreach (var payslip in PairedPayslips)
                //{
                //    var payslip1 = payslip.Item1;
                //    var payslip2 = payslip.Item2;

                //    MakePayslipPDF3 pm = new MakePayslipPDF3(payslip1, payslip2, document, connection);
                //    pm.BuildPDF();
                //    document.NewPage();
                //}

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
}