using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP.NET_MVC4_CRUD_WEBAPP.Models
{
    public class Auction
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public decimal StartPrice { get; set; }
        public decimal? CurrentPrice { get; set; }


    }
}