using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectTemplate2
{
    class MemberInfo
    {
        //property
        public string Status { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CreditCardType { get; set; }
        public string CreditCardNumber { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string PhoneType { get; set; }
        public string MembershipType { get; set; }
        public string AdditonalFeature { get; set; }
        public string StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Age { get; set; }
        public string Weight { get; set; }
        public string FitnessGoal { get; set; }
       
        //constructor
        public MemberInfo()
        {

        }

        //constructor
        public MemberInfo( string f, string l, string cc, string cn, string p, string e, string g, string pt, string mt, string af, string sd, DateTime ed, string a, string w, string fg)
        {
            FirstName = f;
            LastName = l;
            CreditCardType = cc;
            CreditCardNumber = cn;
            Phone = p;
            Email = e;
            Gender = g;
            PhoneType = pt;
            MembershipType = mt;
            AdditonalFeature = af;
            StartDate = sd;
            EndDate = ed;
            Age = a;
            Weight = w;
            FitnessGoal = fg;
            Status = "Active";
        }

        public void UpdateStatus()
        {
            DateTime n = DateTime.Now;
            if (EndDate > n)
            {
                Status = "Active";
            }
            else if (EndDate < n)
            {
                Status = "Expired";
            }
        }



    }
}
