using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AzureAPI.Models
{
    public class Account
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string WebsiteUrl { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
    }
}