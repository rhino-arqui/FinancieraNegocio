using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InsFinanciera.Models
{
    public class Peticion
    {
        public string TipoDoc { get; set; }
        public decimal Doc { get; set; }
        public string Pass { get; set; }
        public decimal Value { get; set; }
        public decimal getDoc()
        {
            return this.Doc;
        }
        public string getPass()
        {
            return this.Pass;
        }
    }
}