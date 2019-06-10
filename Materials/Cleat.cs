using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Materials
{
    /* Class that groups all cleats*/
    class Cleat : Part
    {
        private string determiningDimension = "height";

        /*Builder*/
        public Cleat(float price, int length)
        {
            this.price = price;
            this.length =  length;
            this.name = "Tasseau";
        }

        /*Returns a dictionary with all panel information*/
        public override Dictionary<string, Object> GetDescription()
        {
            Dictionary<string, Object> Description = new Dictionary<string, Object>
            {
                { "price", this.price },
                { "length", this.length },
                { "ref", this.name },
                { "dim", this.determiningDimension },
                { "code", this.code }
            };
            return Description;
        }
    }
}
