﻿using ASP.NET_MVC4_CRUD_WEBAPP.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;


namespace ASP.NET_MVC4_CRUD_WEBAPP.Controllers
{
    public class AuctionController : Controller
    {

        //
        // GET: /Auctions/
        [AllowAnonymous]
        [OutputCache(Duration=1)]
        public ActionResult Index()
        {
            var db = new AuctionsDataContext();
            var auctionsAll = db.Auctions.ToArray();
            var auctions = new List<Auction>();
             

         foreach(Auction auction in auctionsAll)
         {
             if (String.IsNullOrEmpty(auction.IsFinished)!=true)
             { 
                 String auctionIsFinished = auction.IsFinished;
                     if (auctionIsFinished.Equals("No"))
                     {
                         auctions.Add(auction);
                     }
             }
         }

             return View(auctions);
        }

        public ActionResult FinishedAuctions()
        {
            var db = new AuctionsDataContext();
            var auctionsAll = db.Auctions.ToArray();
            var auctions = new List<Auction>();


            foreach (Auction auction in auctionsAll)
            {
                if (String.IsNullOrEmpty(auction.IsFinished) != true )
                {
                    String auctionIsFinished = auction.IsFinished;
                    if (auctionIsFinished.Equals("Yes"))
                    {
                        auctions.Add(auction);
                    }
                }
            }

            return View(auctions);
        }

         [OutputCache(Duration = 10)]
        public ActionResult Auction(long id)
        {
            var db = new AuctionsDataContext();
            var auction = db.Auctions.Find(id);

            return View(auction);
        }

        [HttpPost]
        public ActionResult Bid(Bid bid)
        {
            var db = new AuctionsDataContext();
            var auction = db.Auctions.Find(bid.AuctionId);

            if (auction == null)
            {
                ModelState.AddModelError("AuctionId", "Auction not found!");
            }
            else if (auction.CurrentPrice >= bid.Amount)
            {
                ModelState.AddModelError("Amount", "Bid amount must exceed current bid");
            }
            else
            {
                bid.Username = User.Identity.Name;
                auction.Bids.Add(bid);
                auction.CurrentPrice = bid.Amount;
                auction.LastBindingPerson = bid.Username;
                db.SaveChanges();
            }


            if (!Request.IsAjaxRequest())
                return RedirectToAction("Auction", new { id = bid.AuctionId });

            return PartialView("_CurrentPrice", auction); 
        }

        [HttpGet]
        public ActionResult Create()
        {
            var categoryList = new SelectList(new[] { "Automotive", "Electronics", "Games", "Home" });
            ViewBag.CategoryList = categoryList;
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Create([Bind(Exclude = "CurrentPrice")]Models.Auction auction)
        {
            if (ModelState.IsValid)
            {
                // Save to the database
                var db = new AuctionsDataContext();
                String CurrentUser = System.Web.HttpContext.Current.User.Identity.Name.ToString(); //pobranie aktualnie zalogowanego Usera
                db.Auctions.Add(auction);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return Create();
        }
        
        [HttpGet]
        public ActionResult Edit(long id)
        {
           
                var db = new AuctionsDataContext();
                try
                {
                    var auction = db.Auctions.Find(id);
                   
                    return View(auction);
                  
                }
                catch (DbEntityValidationException e)
                {
                    Console.WriteLine(e.StackTrace);
                    throw;
               }

        }
       [HttpPost] 
       public ActionResult Edit(Auction auction)
       {
           var db = new AuctionsDataContext();
           try
           {
               var auctionOriginal = db.Auctions.Find(auction.Id);
               //how to replace existing lement with current element ?
               if (auctionOriginal != null)
               {
                   db.Entry(auctionOriginal).CurrentValues.SetValues(auction);
                   db.SaveChanges();
               }
              

               return RedirectToAction("Index");

           }
           catch (DbEntityValidationException e)
           {
               Console.WriteLine(e.StackTrace);
               throw;
           }
            
       }
        public ActionResult Delete(long id)
        {
            var db = new AuctionsDataContext();
            var auction = db.Auctions.Find(id);

            db.Auctions.Remove(auction);
            db.SaveChanges();

            var auctions = db.Auctions.ToArray();

            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult StopBinding(Bid bid) //method which set record in database ('cancelled' for 'true')
        {
            var db = new AuctionsDataContext();
            long id = bid.AuctionId;
            string CurrentWinner = bid.Username;
            try
            { 
               var auction = db.Auctions.Find(id);
               auction.IsFinished = "Yes";
               auction.LastBindingPerson =  System.Web.HttpContext.Current.User.Identity.Name.ToString();;
                db.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                    Console.WriteLine(e.StackTrace);
                throw;
            }
            return RedirectToAction("Index");
        }
        
       /*
        public String ChooseAuctionWinner(Bid bid)
        {
            String AuctionWinner = "";
            //jeżeli wylicytujesz wiekszą lub równą kwotę maksymalnej : wygrywasz
            //Jeżeli inny koleś użytkownika - wygrywasz
            //Jeżeli ty przerwiesz - przegrywasz

            return AuctionWinner;
        }
       */
    }
}
