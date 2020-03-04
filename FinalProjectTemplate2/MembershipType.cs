using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectTemplate2
{
    class MembershipType
    {
        //Define Properties
        public string Type { get; set; }
        public double Price { get; set; }
        public string Availability { get; set; }

        //Default Constructor
        public MembershipType() { }

        //Another Constructor
        public MembershipType(string type, double price, string availability)
        {
            Type = type;
            Price = price;
            Availability = availability;
        }

    }
}