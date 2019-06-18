using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuotesApi.Models
{
    public class Supplier
    {
        public int Id { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public DateTime Dob { get; set; }
    }
}
