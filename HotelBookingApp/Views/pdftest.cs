using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System.IO;

namespace HotelBookingApp.Views
{
    public partial class pdftest : Form
    {
        public pdftest()
        {
            InitializeComponent();
        }

        private void pdftest_Load(object sender, EventArgs e)
        {

        }


        private void button1_Click(object sender, EventArgs e)
        {
            string filePath = "TestReport.pdf";

            using (var writer = new PdfWriter(filePath))
            using (var pdf = new PdfDocument(writer))
            {
                var doc = new Document(pdf);
                doc.Add(new Paragraph("PDF export test successful!"));
            }

            MessageBox.Show("PDF exported to: " + Path.GetFullPath(filePath));
        }
    }
}
