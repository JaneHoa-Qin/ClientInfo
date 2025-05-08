using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClientInfo.DTO
{
    public class ClientWithAddresses
    {
        public int ClientId { get; set; }
        public string FullName { get; set; }
        public List<AddressDto> Addresses { get; set; }
    }
}