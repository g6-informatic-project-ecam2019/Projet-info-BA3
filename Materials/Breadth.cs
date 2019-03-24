using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Materials
{
    class Breadth : Piece  /* Class that groups all breadths*/
    {
        private string pos;
        private string determiningDimension;
        public Breadth(float price, int length, string pos)/*builder*/
        {
            this.price = price;
            this.length = length;
            this.pos = pos;
            this.name = String.Format("Traverse {0}", pos);
            this.determiningDimension = ((this.pos == "Av") || (this.pos == "Ar")) ? "width" : "depth";
        }
        public override Dictionary<string, Object> GetDescription()/*Returns a dictionary with all panel information*/
        {
            Dictionary<string, Object> Description = new Dictionary<string, Object>();
            Description.Add("price", this.price);
            Description.Add("length", this.length);
            Description.Add("pos", this.pos);
            Description.Add("ref", this.name);
            Description.Add("dim", this.determiningDimension);
            return Description;
        }
    }
}
