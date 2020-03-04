using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectTemplate2
{
    class Discount
    {
        //Define Properties
        public string DiscountName { get; set; }
        public double SpecifiedDiscount { get; set; }


        //Default Constructor
        public Discount() { }

        //Another Constructor
        public Discount(string discountname, double specifieddiscount)
        {
            DiscountName = discountname;
            SpecifiedDiscount = specifieddiscount;
        }
    }
}