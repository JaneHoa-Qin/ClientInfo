using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClientInfo.DTO
{
    public class AddressDto
    {
        public int AddressId { get; set; }
        public string Street { get; set; }
        public string Email { get; set; }
    }
}