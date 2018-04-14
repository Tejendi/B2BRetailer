using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerApi.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string StreetAddress { get; set; }
        public short ZipCode { get; set; }
        public string City { get; set; }
    }
}
