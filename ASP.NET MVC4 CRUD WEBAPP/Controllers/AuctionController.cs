using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASP.NET_MVC4_CRUD_WEBAPP.Controllers
{
    public class AuctionController : Controller
    {
        //
        // GET: /Auction/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Auction()
        {
            return View();
        }

    }
}
