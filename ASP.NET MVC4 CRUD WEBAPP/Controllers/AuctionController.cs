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
            var auctions = new[]{
                new Models.Auction()
                {
                    Title="Example Auction #1",
                    Description= "This is an example Auction",
                    StartTime=DateTime.Now,
                    EndTime=DateTime.Now.AddDays(7),
                    StartPrice =1.00m,
                    CurrentPrice=null,
                },
                new Models.Auction()
                {
                    Title="Example Auction #2",
                    Description="This is a second Auction",
                    StartTime = DateTime.Now,
                    EndTime=DateTime.Now.AddDays(7),
                    StartPrice=1.00m,
                    CurrentPrice=30m,
                }

            };

            return View(auctions);
        }

        public ActionResult TempDataDemo()
        {
            TempData["SuccessMessage"] = "The action succeeded!";
            return RedirectToAction("Index");
        }

        public ActionResult Auction()
        {
            return View();
        }

    }
}
