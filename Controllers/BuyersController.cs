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
    //Buyers controller using Authorisation.
    public class BuyersController : Controller
    {
        private readonly Buy_And_Sell_House_MVCContext _context;

        public BuyersController(Buy_And_Sell_House_MVCContext context)
        {
            _context = context;
        }

        //Get all buyers using a linq query.
        // GET: Buyers
        public IActionResult Index()
        {
            return View((from buyers in _context.Buyer select buyers).ToList());
        }

        // GET: Buyers/Details/5
        //Gets buyers details using a lamda query.
        public IActionResult Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buyer =  _context.Buyer
                .FirstOrDefault(m => m.Id == id);
            if (buyer == null)
            {
                return NotFound();
            }

            return View(buyer);
        }

        // GET: Buyers/Create
        //Get create form. 
        public IActionResult Create()
        {
            return View();
        }

        // POST: Buyers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //Adds  the buyer to the databse
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,BuyerName,BuyerPhoneNumber")] Buyer buyer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(buyer);
               _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(buyer);
        }

        // GET: Buyers/Edit/5
        //Gets the buyer for update using a linq query.
        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buyer = (from buyers in _context.Buyer
                         where buyers.Id == id
                         select buyers).FirstOrDefault();
            if (buyer == null)
            {
                return NotFound();
            }
            return View(buyer);
        }

        // POST: Buyers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //Updates the buyer .
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, [Bind("Id,BuyerName,BuyerPhoneNumber")] Buyer buyer)
        {
            if (id != buyer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(buyer);
                     _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BuyerExists(buyer.Id))
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
            return View(buyer);
        }

        // GET: Buyers/Delete/5
        //Removes a  buyer uses a lamda query to select the buyer.
        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buyer =_context.Buyer
                .FirstOrDefault(m => m.Id == id);
            if (buyer == null)
            {
                return NotFound();
            }

            return View(buyer);
        }

        // POST: Buyers/Delete/5
        //Removes the buyer uses a linq query to select the buyer.
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
            var buyer = (from buyers in _context.Buyer
                         where buyers.Id == id
                         select buyers).FirstOrDefault();
            _context.Buyer.Remove(buyer);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        //Checks the buyer using a lamda query.
        private bool BuyerExists(string id)
        {
            return _context.Buyer.Any(e => e.Id == id);
        }
    }
}
