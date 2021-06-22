using GeneralStore.MVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace GeneralStore.MVC.Controllers
{
    public class TransactionController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();
        // GET: Transaction
        public ActionResult Index()
        {
            List<Transaction> transactionList = _db.Transactions.ToList();
            List<Transaction> orderedList = transactionList.OrderBy(tran => tran.DateOfTransaction).ToList();
            return View(orderedList);
        }

        // Get: Transaction
        public ActionResult Create()
        {
            var pList = _db.Products.ToList();
            var cList = _db.Customers.ToList();
            ViewBag.ProductId = new SelectList(pList, "ProductId", "Name");
            ViewBag.CustomerId = new SelectList(cList, "CustomerId", "FullName");

            return View();
        }
        //Post: Transaction
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                transaction.DateOfTransaction = DateTime.Now;
                _db.Transactions.Add(transaction);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(transaction);
        }
    }
}