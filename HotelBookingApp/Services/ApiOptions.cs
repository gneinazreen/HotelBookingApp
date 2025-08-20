using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingApp.Services
{
    public sealed class ApiOptions
    {
        public string BaseAddress { get; set; } = "http://localhost:5118"; // API URL
        public bool UseXmlRoutes { get; set; } = true;                     // true => /api/xml/...
    }


}
