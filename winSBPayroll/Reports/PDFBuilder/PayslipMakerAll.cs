using System;
using System.Collections.Generic;
using System.IO;
using BLL;
using BLL.DataEntry;
using CommonLib;
//Payroll
using DAL; 
//--- Add the following to make itext work
using iTextSharp.text;
using iTextSharp.text.pdf;
using VVX;

namespace winSBPayroll.Reports.PDF
{
    public class PayslipMakerAll
    {
        List<Payslip> payslipList;
        Document document;
        string Message;
        string sFilePDF;
        DataEntry de;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;

        Font hFont1 = new Font(Font.TIMES_ROMAN, 14, Font.BOLD);
        Font hFont2 = new Font(Font.TIMES_ROMAN, 10, Font.BOLD);
        Font bFont1 = new Font(Font.TIMES_ROMAN, 12, Font.NORMAL);//body 
        Font tHFont = new Font(Font.TIMES_ROMAN, 12, Font.BOLD); //table Header
        Font tcFont = new Font(Font.HELVETICA, 10, Font.NORMAL);//table cell

        public PayslipMakerAll(List<Payslip> PayslipList, string FileName, string Conn)
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

                foreach (var payslip in payslipList)
                {
                    MakePayslipPDF pm = new MakePayslipPDF(payslip, document, connection);
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

}
