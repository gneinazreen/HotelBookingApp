using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HotelBookingApp.Models;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;


namespace HotelBookingApp.Views
{
    public partial class ReportForm : Form
    {
        public ReportForm()
        {
            InitializeComponent();
            LoadWeeklyReport();
        }
        private List<ReportEntry> weeklyEntries = new List<ReportEntry>();
        private void LoadWeeklyReport()
        {
            listViewReport.Items.Clear();
            weeklyEntries.Clear();

            DateTime weekStart = DateTime.Today;
            while (weekStart.DayOfWeek != DayOfWeek.Monday)
                weekStart = weekStart.AddDays(-1);

            for (int i = 0; i < 7; i++)
            {
                DateTime day = weekStart.AddDays(i);
                string dayLabel = day.ToString("dddd, yyyy-MM-dd");

                var bookingsOnDay = DataStorage.Bookings.Where(b => b.CheckInDate <= day &&  b.CheckOutDate > day).ToList();

                if (bookingsOnDay.Count == 0)
                {
                    var item = new ListViewItem(dayLabel);
                    item.SubItems.Add("No Bookings");
                    listViewReport.Items.Add(item);

                    weeklyEntries.Add(new ReportEntry
                    {
                        Day = dayLabel,
                        Guest = "No bookings",
                        RoomType = "-",
                        Request = "-"
                    });
                }
                else
                {
                    foreach (var b in bookingsOnDay)
                    {
                        var room = DataStorage.Rooms.FirstOrDefault(r => r.RoomId == b.RoomId);
                        var request = DataStorage.Requests.FirstOrDefault(r => r.RequestId == b.RequestId);

                        string roomType = room?.RoomType ?? "Unknown";
                        string requestDesc = request?.Description ?? "None";

                        var item = new ListViewItem(dayLabel);
                        item.SubItems.Add($"{b.FirstName} {b.LastName}");
                        item.SubItems.Add(roomType);
                        item.SubItems.Add(requestDesc);
                        listViewReport.Items.Add(item);

                        weeklyEntries.Add(new ReportEntry
                        {
                            Day = dayLabel,
                            Guest = $"{b.FirstName} {b.LastName}",
                            RoomType = roomType,
                            Request = requestDesc
                        });
                    }
                }
            }
        }
        
        private void ReportForm_Load(object sender, EventArgs e)
        {

        }

        private void listViewReport_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnExportCSV_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "CSV files (*.csv)|*.csv";
                sfd.Title = "Save Weekly Report as CSV";
                sfd.FileName = "WeeklyReport.csv";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    var sb = new StringBuilder();
                    sb.AppendLine("Day,Guest,RoomType,Request");

                    foreach (var entry in weeklyEntries)
                    {
                        sb.AppendLine($"{entry.Day},{entry.Guest},{entry.RoomType},{entry.Request}");
                    }

                    File.WriteAllText(sfd.FileName, sb.ToString());
                    MessageBox.Show("CSV exported successfully.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnExportPDF_Click(object sender, EventArgs e)
        {
            //using (var writer = new PdfWriter("WeeklyReport.pdf"))
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "PDF files (*.pdf)|*.pdf";
                sfd.Title = "Save Weekly Report as PDF";
                sfd.FileName = "WeeklyReport.pdf";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    using (var writer = new PdfWriter(sfd.FileName))
                    {
                        var pdf = new PdfDocument(writer);
                        var doc = new Document(pdf);
                        doc.Add(new Paragraph("Weekly Hotel Booking Report").SetFontSize(16));

                        var table = new Table(4, true);
                        table.AddHeaderCell("Day");
                        table.AddHeaderCell("Guest");
                        table.AddHeaderCell("Room Type");
                        table.AddHeaderCell("Request");

                        foreach (var entry in weeklyEntries)
                        {
                            table.AddCell(entry.Day);
                            table.AddCell(entry.Guest);
                            table.AddCell(entry.RoomType);
                            table.AddCell(entry.Request);
                        }

                        doc.Add(table);
                        doc.Close();
                    }
                    MessageBox.Show("PDF Exported: WeeklyReport.pdf");
                }
                
            }

            
        }
    }
    public class ReportEntry
    {
        public string Day { get; set; }
        public string Guest { get; set; }
        public string RoomType { get; set; }
        public string Request { get; set; }
    }
}
