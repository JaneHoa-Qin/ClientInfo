using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClientInfo.Model
{
    public class Address
    {
        public int AddressId { get; set; }
        public string Street { get; set; }
        public string Email { get; set; }
        //Foreign key
        public int ClientId { get; set; }
        // Navigation property
        public Client Client { get; set; }
    }
}