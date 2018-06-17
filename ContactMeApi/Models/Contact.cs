﻿namespace ContactMeApi.Models
{
    public class Contact
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string JobTitle { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
