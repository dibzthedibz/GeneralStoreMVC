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
        // Get: Delete
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = _db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Transaction transaction = _db.Transactions.Find(id);
            _db.Transactions.Remove(transaction);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        //Get : Transaction/Edit/{Id}
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = _db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            var pList = _db.Products.ToList();
            var cList = _db.Customers.ToList();
            ViewBag.ProductId = new SelectList(pList, "ProductId", "Name");
            ViewBag.CustomerId = new SelectList(cList, "CustomerId", "FullName");
            return View(transaction);
        }
        //Post: Transaction/Edit {id}
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(transaction).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(transaction);
        }

        //Get: Details Transaction/Details/{Id}
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = _db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }
    }
}