﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClientInfo.Model
{
    public class Client
    {
        public int ClientId { get; set; }
        [MaxLength(100)]
        public string FullName { get; set; }
        public ICollection<Address> Addresses { get; set; }
    }
}