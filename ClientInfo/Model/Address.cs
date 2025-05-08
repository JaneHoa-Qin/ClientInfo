using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClientInfo.Model
{
    public class Address
    {
        public int AddressId { get; set; }
        [MaxLength(100)]
        public string Street { get; set; }
        [MaxLength(30)]
        public string Email { get; set; }
        //Foreign key
        public int ClientId { get; set; }
        // Navigation property
        public Client Client { get; set; }
    }
}