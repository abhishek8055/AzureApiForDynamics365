using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AzureAPI.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string JobTitle { get; set; }
        public string AccountName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public virtual Account Account { get; set; }
    }
}