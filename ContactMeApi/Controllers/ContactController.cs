using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using ContactMeApi.Models;

namespace ContactMeApi.Controllers
{
    [Route("api/contacts")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly ContactContext _context;

        public ContactController(ContactContext context)
        {
            _context = context;

            if (_context.Contacts.Count() == 0)
            {
                _context.Contacts.Add(new Contact 
                { 
                    First_name = "Dro", 
                    Last_name = "Tavarez",
                    Job_title = "Software Developer",
                    Phone_number = "978-239-9784",
                    Email = "pe@dro.com"
                });
                _context.SaveChanges();
            }
        }
        [HttpGet]
        public ActionResult<List<Contact>> GetAll()
        {
            return _context.Contacts.ToList();
        }
        [HttpGet("{id}", Name = "GetContact")]
        public ActionResult<Contact> GetById(long id)
        {
            var contact = _context.Contacts.Find(id);
            if (contact == null)
            {
                return NotFound();
            }
            return contact;
        }
        [HttpPost]
        public IActionResult Create(Contact contact)
        {
            _context.Contacts.Add(contact);
            _context.SaveChanges();

            return CreatedAtRoute("GetContact", new { id = contact.Id }, contact);
        }
    }
}