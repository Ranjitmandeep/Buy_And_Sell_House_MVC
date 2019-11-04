using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Buy_And_Sell_House_Core_Webapp.Business;
using Buy_And_Sell_House_MVC.Models;
using Microsoft.AspNetCore.Authorization;

namespace Buy_And_Sell_House_MVC.Controllers
{
    [Authorize]
    //Transactions controller with authorization.
    public class TransactionsController : Controller
    {
        private readonly Buy_And_Sell_House_MVCContext _context;

        public TransactionsController(Buy_And_Sell_House_MVCContext context)
        {
            _context = context;
        }

        // GET: Transactions
        //Gets all transactions using a lamda query.
        public IActionResult Index()
        {
            var buy_And_Sell_House_MVCContext = _context.Transaction.Include(t => t.Buyer).Include(t => t.House);
            return View(buy_And_Sell_House_MVCContext.ToList());
        }

        // GET: Transactions/Details/5
        //Get details if the transaction using a lamda query.
        public IActionResult Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction =  _context.Transaction
                .Include(t => t.Buyer)
                .Include(t => t.House)
                .FirstOrDefault(m => m.TransactionId == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // GET: Transactions/Create
        //Gets the create form.
        public IActionResult Create()
        {
            ViewData["BuyerId"] = new SelectList(_context.Buyer, "Id", "BuyerName");
            ViewData["HouseId"] = new SelectList(_context.House, "Id", "HouseAddress");
            return View();
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //Adds a transaction to the databse.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("TransactionId,BuyerId,HouseId")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                _context.Add(transaction);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BuyerId"] = new SelectList(_context.Buyer, "Id", "Id", transaction.BuyerId);
            ViewData["HouseId"] = new SelectList(_context.House, "Id", "Id", transaction.HouseId);
            return View(transaction);
        }

        // GET: Transactions/Edit/5
        //Gets the  transaction for update using a linq query.
        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = (from transactions in _context.Transaction
                               where transactions.TransactionId == id
                               select transactions).FirstOrDefault();
            if (transaction == null)
            {
                return NotFound();
            }
            ViewData["BuyerId"] = new SelectList(_context.Buyer, "Id", "BuyerName", transaction.BuyerId);
            ViewData["HouseId"] = new SelectList(_context.House, "Id", "HouseAddress", transaction.HouseId);
            return View(transaction);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //Updates the transaction 
        public IActionResult Edit(string id, [Bind("TransactionId,BuyerId,HouseId")] Transaction transaction)
        {
            if (id != transaction.TransactionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transaction);
                     _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransactionExists(transaction.TransactionId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BuyerId"] = new SelectList(_context.Buyer, "Id", "Id", transaction.BuyerId);
            ViewData["HouseId"] = new SelectList(_context.House, "Id", "Id", transaction.HouseId);
            return View(transaction);
        }

        // GET: Transactions/Delete/5
        //Gets the transacion for delete using a lamda query.
        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction =  _context.Transaction
                .Include(t => t.Buyer)
                .Include(t => t.House)
                .FirstOrDefault(m => m.TransactionId == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // POST: Transactions/Delete/5
        //Deletes the transaction  usesa linq query to get the transaction.
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
            var transaction = (from transactions in _context.Transaction
                               where transactions.TransactionId == id
                               select transactions).FirstOrDefault();
            _context.Transaction.Remove(transaction);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        //Checks the transaction using a lamda query.
        private bool TransactionExists(string id)
        {
            return _context.Transaction.Any(e => e.TransactionId == id);
        }
    }
}
