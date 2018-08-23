using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
// For using Include()
using System.Data.Entity;

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
            // Query is executed by the ToList() method, because it iterates
            var customers = _context.Customers.Include(c => c.MembershipType).ToList();

            return View(customers);
        }

        public ActionResult New()
        {
            var membershipTypes = _context.MembershipTypes.ToList();
            var viewModel = new CustomerFormViewModel() {
                MembershipTypes = membershipTypes,
            };
            return View("CustomerForm", viewModel);
        }

        // The action is only accessable by a 'Post' and not by 'Get'
        [HttpPost]
        // De Customer parameter wordt meegegeven door het form te posten
        // Het object in de view is een viewmodel, maar omdat alles geprefixed is met Customer in de inputs (met lambda expressies) 
        // Werkt alleen customer hier ook, dit heet 'model binding'
        public ActionResult Save(Customer customer)
        {
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
    }
}