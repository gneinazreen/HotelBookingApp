using HotelBookingApp.Services;
using HotelBookingApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
            //HotelBookingApp.Models.DataStorage.Initialize();
            // set true if API is in XML mode (UseXmlStorage=true), false for EF mode
            var options = new ApiOptions { BaseAddress = "http://localhost:5167", UseXmlRoutes = true };
            var http = new HttpClient();
            var api = new ApiClient(http, options);

            // pass `api` to your forms (DI or ctor)
            Application.Run(new Main(api));
            //Application.Run(new Main());
            //Application.Run(new pdftest());
        }
    }
}
