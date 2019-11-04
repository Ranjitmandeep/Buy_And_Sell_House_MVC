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
    //Seller controller authorisation.
    public class SellersController : Controller
    {
        private readonly Buy_And_Sell_House_MVCContext _context;

        public SellersController(Buy_And_Sell_House_MVCContext context)
        {
            _context = context;
        }

        // GET: Sellers
        //Gets all sellers using a lamda query.
        public IActionResult Index()
        {
            var buy_And_Sell_House_MVCContext = _context.Seller.Include(s => s.House);
            return View(buy_And_Sell_House_MVCContext.ToList());
        }

        // GET: Sellers/Details/5
        //Get seller details using a lamda query.
        public IActionResult Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seller =  _context.Seller
                .Include(s => s.House)
                .FirstOrDefault(m => m.Id == id);
            if (seller == null)
            {
                return NotFound();
            }

            return View(seller);
        }

        // GET: Sellers/Create
        //Gets the create form.
        public IActionResult Create()
        {
            ViewData["HouseId"] = new SelectList(_context.House, "Id", "HouseAddress");
            return View();
        }

        // POST: Sellers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //Adds a seller to the databse.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,HouseId,SellerName,SellerPhoneNumber")] Seller seller)
        {
            if (ModelState.IsValid)
            {
                _context.Add(seller);
                 _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HouseId"] = new SelectList(_context.House, "Id", "Id", seller.HouseId);
            return View(seller);
        }

        // GET: Sellers/Edit/5
        //Gets the seller for edit using  a linq query.
        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seller = (from sellers in _context.Seller
                          where sellers.Id == id
                          select sellers).FirstOrDefault();
            if (seller == null)
            {
                return NotFound();
            }
            ViewData["HouseId"] = new SelectList(_context.House, "Id", "HouseAddress", seller.HouseId);
            return View(seller);
        }

        // POST: Sellers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //Updates the seller
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, [Bind("Id,HouseId,SellerName,SellerPhoneNumber")] Seller seller)
        {
            if (id != seller.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(seller);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SellerExists(seller.Id))
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
            ViewData["HouseId"] = new SelectList(_context.House, "Id", "Id", seller.HouseId);
            return View(seller);
        }

        // GET: Sellers/Delete/5
        //Get the seller for delete. Uses  a lamda query to get the seller.
        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seller =  _context.Seller
                .Include(s => s.House)
                .FirstOrDefault(m => m.Id == id);
            if (seller == null)
            {
                return NotFound();
            }

            return View(seller);
        }

        // POST: Sellers/Delete/5
        //Deletes the selelr uses  a linq query to  get the seller.
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
            var seller = (from sellers in _context.Seller
                          where sellers.Id == id
                          select sellers).FirstOrDefault();
            _context.Seller.Remove(seller);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        //Checks the seller for existance using  a lamda query.
        private bool SellerExists(string id)
        {
            return _context.Seller.Any(e => e.Id == id);
        }
    }
}
