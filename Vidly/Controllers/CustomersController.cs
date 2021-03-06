﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
// For using Include()
using System.Data.Entity;
using System.Runtime.Caching;
using System.Diagnostics;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            // To access the database
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Index()
        {
            // Store genres in the cache if it is the first time executing this page
            if (MemoryCache.Default["Genres"] == null) {
                MemoryCache.Default.Add(new CacheItem("Genres", _context.Genres.ToList()), new CacheItemPolicy());
            }
            // Get genres from the cache
            var genres = MemoryCache.Default["Genres"] as IEnumerable<Genre>;


            // Without api and with html markup being generated on the server use this:
            /*  // Query is executed by the ToList() method, because it iterates
              var customers = _context.Customers.Include(c => c.MembershipType).ToList();
              return View(customers);
             */
            return View();
        }     

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult New()
        {
            var membershipTypes = _context.MembershipTypes.ToList();
            var viewModel = new CustomerFormViewModel() {
                MembershipTypes = membershipTypes,
                // So that ID is not null but initialised as 0, this is because ID is 
                // not a nullable property and so the customer won't be valid if the ID is null
                // Another solution is to get all the customer props in the customer viewmodel and not a reference to customer
                Customer = new Customer()
            };
            return View("CustomerForm", viewModel);
        }
    
        // The action is only accessable by a 'Post' and not by 'Get'
        [HttpPost]
        [ValidateAntiForgeryToken]
        // De Customer parameter wordt meegegeven door het form te posten
        // Het object in de view is een viewmodel, maar omdat alles geprefixed is met Customer in de inputs (met lambda expressies) 
        // Werkt alleen customer hier ook, dit heet 'model binding'
        public ActionResult Save(Customer customer)
        {
            // If the form values are not valid, user will be send back to the form with validation error messages
            if (!ModelState.IsValid) {
                var viewModel = new CustomerFormViewModel() {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList(),
                };
                return View("CustomerForm", viewModel);
            }
            // A new customer has an ID of 0
            if (customer.Id == 0) {
                _context.Customers.Add(customer);
            }
            // An existing customer, so updating the database
            else {
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);
                customerInDb.Name = customer.Name;
                customerInDb.BirthDate = customer.BirthDate;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Customers");
        }

        // When a customer is clicked the edit action gets executed and a form will show where the customer can be edited.
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null) {
                return HttpNotFound();
            }

            var viewModel = new CustomerFormViewModel() {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList(),
            };

            return View("CustomerForm", viewModel);
        }

        public ActionResult Details(int id)
        {
            // Query will be executed because of singleordefault
            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);
            
            if (customer == null) {
                return HttpNotFound();
            }

            return View(customer);
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Delete(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null) {
                return HttpNotFound();
            }

            _context.Customers.Remove(customer);
            _context.SaveChanges();
            return RedirectToAction("Index", "Customers");
        }
    }
}