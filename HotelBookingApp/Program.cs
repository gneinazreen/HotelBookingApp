using HotelBookingApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelBookingApp
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            HotelBookingApp.Models.DataStorage.Initialize();
            Application.Run(new Main());
            //Application.Run(new pdftest());
        }
    }
}
