using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Materials
{
    /* Class that groups all breadths*/
    class Breadth : Part
    {
        private string position;
        private string determiningDimension;

        /*Builder*/
        public Breadth(float price, int length, string pos)
        {
            this.price = price;
            this.length = length;
            this.position = position;
            this.name = String.Format("Traverse {0}", position);
            this.determiningDimension = ((this.position == "Av") || (this.position == "Ar")) ? "width" : "depth";
        }

        /*Returns a dictionary with all panel information*/
        public override Dictionary<string, Object> GetDescription()
        {
            Dictionary<string, Object> Description = new Dictionary<string, Object>
            {
                { "price", this.price },
                { "length", this.length },
                { "position", this.position },
                { "ref", this.name },
                { "dim", this.determiningDimension },
                { "code", this.code }
            };
            return Description;
        }
    }
}
