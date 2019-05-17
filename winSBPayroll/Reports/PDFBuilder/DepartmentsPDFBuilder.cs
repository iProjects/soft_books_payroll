using System;
using System.IO;
using System.Text;
using BLL;
using BLL.DataEntry;
//Payroll
using BLL.KRA.Models;
using CommonLib;
using DAL;
//--- Add the following to make itext work
using iTextSharp.text;
using iTextSharp.text.pdf;
using VVX;

namespace winSBPayroll.Reports.PDF
{
    public class DepartmentsPDFBuilder
    {
         
        BankTransferModel _ViewModel;
        Document document;
        string Message;
        string sFilePDF;

        //DEFINED fONTS
        Font hfont1 = new Font(Font.TIMES_ROMAN, 12, Font.BOLD);
        Font hfont2 = new Font(Font.TIMES_ROMAN, 10, Font.BOLD);
        Font bfont1 = new Font(Font.TIMES_ROMAN, 10, Font.NORMAL);//BODY
        Font tHfont1 = new Font(Font.TIMES_ROMAN, 8, Font.BOLD);//TABLE HEADER
        Font tcFont = new Font(Font.HELVETICA, 8, Font.NORMAL);//table cell
        Font rms6Normal = new Font(Font.TIMES_ROMAN, 6, Font.NORMAL);
        Font rms8Normal = new Font(Font.TIMES_ROMAN, 8, Font.NORMAL);
        Font rms6Bold = new Font(Font.TIMES_ROMAN, 6, Font.BOLD);
        Font rms8Bold = new Font(Font.TIMES_ROMAN, 8, Font.BOLD);

        //constructor
        public DepartmentsPDFBuilder(BankTransferModel departmentsModel, string FileName)
        {
            if (departmentsModel == null)
                throw new ArgumentNullException("BankTransferModel is null");
            _ViewModel = departmentsModel; 

            sFilePDF = FileName;
        }

        public string GetPDF()
        {
            BuildPDF();
            return sFilePDF;
        }

        /**Build the document **/
        private void BuildPDF()
        {
            try
            {
                //step 1 creation of the document
                document = new Document(PageSize.A4.Rotate());

                // step 2:create a writer that listens to the document
                PdfWriter.GetInstance(document, new FileStream(sFilePDF, FileMode.Create));

                //open document
                document.Open();

                //document header
                //AddDocHeader();

                //document body

                //AddDocBody();

                //document footer
                //AddFooter();

                //close document
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