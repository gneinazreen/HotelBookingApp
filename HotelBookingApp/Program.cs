using HotelBookingApp.Services;
using System.Net.Http;
using System;
using System.Windows.Forms;

namespace HotelBookingApp
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var options = new ApiOptions
            {
                BaseAddress = "http://localhost:5118", 
                UseXmlRoutes = false                    // EF mode => false, XML mode => true
            };

            var http = new HttpClient();
            var api = new ApiClient(http, options);

            Application.Run(new Views.Main(api));
        }
    }
}
