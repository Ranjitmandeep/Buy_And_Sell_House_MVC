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
    //House controller with authorization.
    public class HousesController : Controller
    {
        private readonly Buy_And_Sell_House_MVCContext _context;

        public HousesController(Buy_And_Sell_House_MVCContext context)
        {
            _context = context;
        }

        // GET: Houses
        //Gets all houses using a linq query.
        public IActionResult Index()
        {
            return View((from house in _context.House select house).ToList());
        }

        // GET: Houses/Details/5
        //Gets the details of the house using a lamda query.
        public IActionResult Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var house = _context.House
                .FirstOrDefault(m => m.Id == id);
            if (house == null)
            {
                return NotFound();
            }

            return View(house);
        }

        // GET: Houses/Create
        //Gets the create form 
        public IActionResult Create()
        {
            return View();
        }

        // POST: Houses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //Adds the house to the database.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,HouseAddress,HousePrice")] House house)
        {
            if (ModelState.IsValid)
            {
                _context.Add(house);
                 _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(house);
        }

        // GET: Houses/Edit/5
        //Gets the house details for edit using a linq query.
        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var house = (from houses in _context.House
                         where houses.Id == id
                         select houses).FirstOrDefault();
            if (house == null)
            {
                return NotFound();
            }
            return View(house);
        }

        // POST: Houses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //Updates the house details.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, [Bind("Id,HouseAddress,HousePrice")] House house)
        {
            if (id != house.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(house);
                     _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HouseExists(house.Id))
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
            return View(house);
        }

        // GET: Houses/Delete/5
        //Gets the house for delete  uses a lamda query to select the house.
        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var house =  _context.House
                .FirstOrDefault(m => m.Id == id);
            if (house == null)
            {
                return NotFound();
            }

            return View(house);
        }

        // POST: Houses/Delete/5
        //Delete  the house uses a linq query to select the house.
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
            var house = (from houses in _context.House
                         where houses.Id == id
                         select houses).FirstOrDefault();
            _context.House.Remove(house);
             _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        //Checks the house existacen using a lamda query.
        private bool HouseExists(string id)
        {
            return _context.House.Any(e => e.Id == id);
        }
    }
}
