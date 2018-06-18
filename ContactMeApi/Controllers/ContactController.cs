using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using ContactMeApi.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

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

        [HttpGet, Authorize]
        public ActionResult<List<Contact>> GetAll()
        {
            var currentUser = HttpContext.User;
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

        [HttpPut("{id}")]
        public IActionResult Update(long id, Contact contact)
        {
            var con = _context.Contacts.Find(id);
            if (con == null)
            {
                return NotFound();
            }

            con.First_name = contact.First_name;
            con.Last_name = contact.Last_name;
            con.Job_title = contact.Job_title;
            con.Phone_number = contact.Phone_number;
            con.Email = contact.Email;

            _context.Contacts.Update(contact);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var con = _context.Contacts.Find(id);
            if (con == null)
            {
                return NotFound();
            }

            _context.Contacts.Remove(con);
            _context.SaveChanges();
            return NoContent();
        }
    }
}